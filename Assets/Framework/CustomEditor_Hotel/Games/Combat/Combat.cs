using System.Collections;
using System.Text;
using UnityEngine;
using UnityEditor;

/*
1. a character's own charStatus is filtered by their own current statusEffectFilter
2. If the character can perform attack, then 
   filter their attack ability with their current statusEffectFilter
3. filter their attack ability with the target's statusEffect filter
4. if target can receive attack, then apply the ability on the target
 */
namespace HiryuTK.GameRoomService
{
    public class Combat : EditorWindow
    {
        //Rendering pos - Stats
        const int Width = 300;
        const int HalfWidth = (int)(Width * .5f);
        const int HealthBarWidth = HalfWidth - 20;
        const int buttonWidth = (int)(Width / 3f) - 5;

        //Rendering pos - Combat
        const int BulletDamage = 20;
        const int P1PosX = HalfWidth / 2 - 15;
        const int CharPosY = 30;
        const int CharWidth = 40;
        const int CharHeight = 45;

        const int shootZoneLeft = P1PosX + CharWidth + 5;
        const int shootZoneWidth = (HalfWidth - (P1PosX + CharWidth)) * 2;
        const int shootZoneRight = shootZoneLeft + shootZoneWidth;

        const int P2PosX = HalfWidth + P1PosX;

        //Status
        bool initialized;
        string logText = "You met an enemy! Select an action!";
        CombatStance p1Stance = CombatStance.Stand;
        CombatStance p2Stance = CombatStance.Stand;

        //Bullet status
        bool P1HasShotBullet;
        bool P2HasShotBullet;
        int p1BulletSpeed = 1;
        int p2BulletSpeed = 1;
        Vector2Int p1Bullet = Vector2Int.zero;
        Vector2Int p2Bullet = Vector2Int.zero;
        CombatStance p1BulletShotStance = CombatStance.Stand;
        CombatStance p2BulletShotStance = CombatStance.Stand;

        //Portrait status
        string p1Portrait;
        string p2Portrait;

        //Timers
        int p2RespawnTimer;
        int p2MovementCD = 4;
        int p2ShootCD = 8;

        //Characters
        CharacterStatus player;
        CharacterStatus enemy;

        //Cache
        GUIStyle centeredStyle;

        //Properties
        bool p1Alive => p1Stance != CombatStance.Dead;
        bool p2Alive => p2Stance != CombatStance.Dead;

        public void Initialize()
        {
            initialized = true;

            player = new CharacterStatus();
            p1Stance = CombatStance.Stand;
            enemy = new CharacterStatus();
            p2Stance = CombatStance.Stand;

            logText = "You met an enemy! Select an action!";

            centeredStyle = new GUIStyle(GUI.skin.label)
            { alignment = TextAnchor.MiddleCenter };
        }

        private void OnGUI()
        {
            if (!initialized)
                Initialize();

            DisplayP1Stats();
            DisplayP2Stats();
            DisplayLog();
            if (p1Alive)
                DisplayAbilityButtons();
            else
                DisplayRespawnButton();
            DrawBullets();
        }

        #region Draw GUI
        void DisplayP1Stats()
        {
            GUI.Label(new Rect(P1PosX - 5, 5, 100, 20), "Player");
            //Portrait
            {
                UpdateP1Portrait();
                GUILayout.BeginArea(new Rect(P1PosX, CharPosY, CharWidth, CharHeight));
                GUILayout.Label(p1Portrait);
                GUILayout.EndArea();
            }

            EditorGUI.ProgressBar(new Rect(15, 90, HealthBarWidth, 15), (float)player.Hp / 100, "HP");
        }

        void DisplayP2Stats()
        {
            if (!initialized)
                Initialize();

            GUI.Label(new Rect(P2PosX - 5, 5, 100, 20), "Enemy");
            //Portrait
            {
                UpdateP2Portrait();
                GUILayout.BeginArea(new Rect(P2PosX, CharPosY, CharWidth, CharHeight));
                GUILayout.Label(p2Portrait);
                GUILayout.EndArea();
            }
            EditorGUI.ProgressBar(new Rect(HalfWidth + 5, 90, HealthBarWidth, 15), (float)enemy.Hp / 100, "HP");
        }
        #endregion

        #region Log
        void DisplayLog()
        {
            EditorGUILayout.Space(60);
            GUI.Label(new Rect(0, 120, Width, 20), logText, centeredStyle);
        }
        #endregion

        #region Ability buttons
        void DisplayAbilityButtons()
        {
            //Jump
            if (GUI.Button(new Rect(5, 150, buttonWidth, 30), "Jump"))
            {
                p1Stance = CombatStance.Jump;
                    LogTextWhenPlayersAlive("Player jumps");
            }
            if (GUI.Button(new Rect(5 + buttonWidth, 150, buttonWidth, 30), "Duck"))
            {
                p1Stance = CombatStance.Duck;
                LogTextWhenPlayersAlive("Player ducks");
            }
            if (GUI.Button(new Rect(5 + buttonWidth * 2, 150, buttonWidth, 30), "Stand"))
            {
                p1Stance = CombatStance.Stand;
                LogTextWhenPlayersAlive("Player stands up");
            }

            if (GUI.Button(new Rect(5, 180, Width - 15, 30), "Shoot"))
            {
                LogTextWhenPlayersAlive("Player shoots");
                Shoot(true);
            }
        }

        void DisplayRespawnButton()
        {
            if (GUI.Button(new Rect(5, 150, Width - 5, 60), "Respawn"))
            {
                RespawnPlayer();
            }
        }

        void Shoot(bool isP1)
        {
            if (isP1)
            {
                p1BulletSpeed = p1Stance == CombatStance.Stand ? 21 : 7;
                p1Bullet = GetBulletFiringPosition(isP1);
                p1BulletShotStance = p1Stance;
                P1HasShotBullet = true;
            }
            else
            {
                p2BulletSpeed = p2Stance == CombatStance.Stand ? 21 : 7;
                p2Bullet = GetBulletFiringPosition(isP1);
                p2BulletShotStance = p2Stance;
                P2HasShotBullet = true;
            }
        }

        void DrawBullets()
        {
            GUI.Box(new Rect(shootZoneLeft, CharPosY, shootZoneWidth, CharHeight), "");
            if (P1HasShotBullet)
            {
                GUI.Label(new Rect(p1Bullet.x, p1Bullet.y, 10, 10), ">");
            }
            if (P2HasShotBullet)
            {
                GUI.Label(new Rect(p2Bullet.x, p2Bullet.y, 10, 10), "<");
            }
        }

        Vector2Int GetBulletFiringPosition(bool isP1) =>
            new Vector2Int(
                isP1 ? shootZoneLeft : shootZoneLeft + shootZoneWidth,
                GetMuzzlePosY(isP1 ? p1Stance : p2Stance));

        int GetMuzzlePosY(CombatStance stance) => stance switch
        {
            CombatStance.Duck => CharPosY + 30,
            CombatStance.Stand => CharPosY + 17,
            CombatStance.Jump => CharPosY + 5,
            _ => -100
        };
        #endregion

        #region Spawn character status
        void RespawnPlayer()
        {
            logText = "Player respawns";
            if (player == null)
                player = new CharacterStatus();
            else
                player.Reset();
            p1Stance = CombatStance.Stand;
        }

        void RespawnEnemy()
        {
            if (enemy == null)
                enemy = new CharacterStatus();
            else
                enemy.Reset();
            p2Stance = CombatStance.Stand;
        }
        #endregion

        // ======= Updates ======= 
        void OnInspectorUpdate()
        {
            if (!initialized)
                Initialize();

            EnemyAI();
            UpdateBulletPosition();
            UpdateRespawnTimer();
            Repaint();
        }

        #region Bullet combat update
        void UpdateBulletPosition()
        {
            if (P1HasShotBullet)
            {
                p1Bullet.x += p1BulletSpeed;
                if (p1Bullet.x >= shootZoneRight + 2)
                {
                    P1HasShotBullet = false;
                    if (p2Alive)
                    {
                        BulletReachedP2();
                    }
                }
            }

            if (P2HasShotBullet)
            {
                p2Bullet.x -= p2BulletSpeed;
                if (p2Bullet.x <= shootZoneLeft - 2)
                {
                    P2HasShotBullet = false;
                    if (p1Alive)
                    {
                        BulletReachedP1();
                    }
                }
            }
        }

        void BulletReachedP1()
        {
            if (CombatUtil.CanTargetTakeDamage(p1Stance, p2BulletShotStance))
            {
                LogTextWhenPlayersAlive("Player got shot!");
                DealP1Dmg();
            }
            else
                LogTextWhenPlayersAlive("Player dodged a bullet!");
        }

        void BulletReachedP2()
        {
            if (CombatUtil.CanTargetTakeDamage(p2Stance, p1BulletShotStance))
            {
                LogTextWhenPlayersAlive( "Enemy got shot!");
                DealP2Dmg();
            }
            else
                LogTextWhenPlayersAlive("Enemy dodged a bullet!");
        }

        void DealP1Dmg()
        {
            player.ModifyHealth(-BulletDamage);
            if (player.Hp <= 0)
            {
                p1Stance = CombatStance.Dead;
                logText = "Player dead";
            }

        }
        void DealP2Dmg()
        {
            enemy.ModifyHealth(-BulletDamage);
            if (enemy.Hp <= 0)
            {
                p2Stance = CombatStance.Dead;
                int reward = Random.Range(40, 100);
                logText = "Enemy fainted! Received money: " + reward.ToString();
                GameData.Money += reward;
                GameData.Achivement_CombatLegend = true;
                GameData.SaveData();
                FrontDesk.RepaintWindow();
                p2RespawnTimer = 20;
            }
        }

        void UpdateRespawnTimer()
        {
            if (p2RespawnTimer > 0)
            {
                p2RespawnTimer--;
                if (p2RespawnTimer <= 0)
                    RespawnEnemy();
            }
        }
        #endregion

        #region EnemyAI
        void EnemyAI()
        {
            if (!p2Alive)
                return;

            if (p2MovementCD <= 0)
            {
                p2MovementCD = Random.Range(2, 8);
                p2Stance = Random.Range(0, 3) switch
                {
                    0 => CombatStance.Jump,
                    1 => CombatStance.Duck,
                    _ => CombatStance.Stand,
                };
            }
            else
            {
                p2MovementCD--;
            }

            if (p2ShootCD <= 0)
            {
                if (!P2HasShotBullet && p2Alive && p1Alive)
                {
                    p2ShootCD = Random.Range(1, 4);
                    Shoot(false);
                }
            }
            else
            {
                p2ShootCD--;
            }
        }
        #endregion

        #region Minor methods
        void UpdateP1Portrait () => p1Portrait = CombatUtil.GetP1Portrait(p1Stance);
        void UpdateP2Portrait () => p2Portrait = CombatUtil.GetP2ortrait(p2Stance);
        private void OnDestroy()
        {
            GameData.SaveData();
        }

        void LogTextWhenPlayersAlive (string text)
        {
            if (p1Alive && p2Alive)
            {
                logText = text;
            }
        }
        #endregion
    }
}

/*
 
        void DrawBattleGround()
        {
            

            StringBuilder battlefield = new StringBuilder();
            //Render BG
            for (int y = 0; y < BattlegrondRenderHeight; y++)
            {
                for (int x = 0; x < BattlegrondRenderWidth; x++)
                {
                    battlefield.Append((x%10).ToString());
                    //battlefield.Append(x % 2 == 0 ? "," : ".");
                }

                if (y < BattlegrondRenderHeight - 1)
                    battlefield.Append("\n");
            }

            //Render P1
            int p1Length = p1Portrait.Length;
            for (int i = 0; i < p1Length; i++)
            {
                //Current p1 portrait string(char).
                string p1Str = p1Portrait.Substring(i, 1);
                if (p1Str == " ")
                    break;
                Debug.Log("=============");

                int bRow = (int)((float)i / CharacterRenderWidth); //The battlefield row that current char is positioned on.
                int pCol = i % CharacterRenderWidth;  //The portrait-relative y-pos that current char is positiong 

                try
                {
                    //It's corresponding position on the battlefield.
                    int battlefieldIndex = bRow * BattlegrondRenderWidth + P1RenderPoxXStart + pCol;
                    Debug.Log("A: " + battlefield);
                    battlefield.Remove(battlefieldIndex, 1);
                    Debug.Log("B: " + battlefield);
                    battlefield.Insert(battlefieldIndex, p1Str);
                    Debug.Log("C: " + battlefield);
                }
                catch (System.Exception e)
                {
                    Debug.Log("Failed to render char. P1 char: " + p1Str + ", bRow: " + bRow + ", pCol: " + pCol);
                }
            }

            //Render bullet

            GUI.Label(new Rect(10, 0, Width, 100), battlefield.ToString());
        }
 */