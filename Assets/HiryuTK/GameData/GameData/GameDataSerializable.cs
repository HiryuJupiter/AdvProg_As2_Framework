using UnityEngine;
using System.Collections;

namespace HiryuTK.GameDataManagement
{
    //A middle man for saving data, since filestream doesnt handle Vector3 and
    //quaternion well when converting to filestream
    [System.Serializable]
    public class GameDataSerializable
    {
        int playerLevel;
        int playerHealth;
        float pX, pY, pZ;
        public float rX, rY, rZ, rW;

        public GameDataSerializable(GameData gameData)
        {
            playerLevel = gameData.playerLevel;
            playerHealth = gameData.playerHealth;

            pX = gameData.playerPosition.x;
            pY = gameData.playerPosition.y;
            pZ = gameData.playerPosition.z;

            rX = gameData.playerRotation.x;
            rY = gameData.playerRotation.y;
            rZ = gameData.playerRotation.z;
            rW = gameData.playerRotation.w;
        }

        public GameData ConvertToGameData()
        {
            GameData gameData = new GameData();
            gameData.playerLevel = playerLevel;
            gameData.playerHealth = playerHealth;

            Vector3 pos = new Vector3(pX, pY, pZ);
            Quaternion rot = new Quaternion(rX, rY, rZ, rW);
            gameData.playerPosition = pos;
            gameData.playerRotation = rot;

            return gameData;
        }

    }
}