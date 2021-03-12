using UnityEngine;
using System.Collections;


public static class _SceneEvents
{
    public static _SceneEvent GameStart { get; private set; } = new _SceneEvent("Gamestart");

    public static _SceneEvent PlayerSpawn { get; private set; } = new _SceneEvent("PlayerSpawn");
    public static _SceneEvent PlayerDead { get; private set; } = new _SceneEvent("PlayerDead");
    public static _SceneEvent GameOverBackToMain { get; private set; } = new _SceneEvent("GameOverBackToMain");
    public static _SceneEvent GameQuit { get; private set; } = new _SceneEvent("GameQuit");

    //Game wide event
    public static _SceneEvent GameSave { get; private set; } = new _SceneEvent("GameSave");
    public static _SceneEvent GameLoad { get; private set; } = new _SceneEvent("GameLoad");

    public static bool GameWideEventsInitialized { get; private set; }
    public static bool PerLevelEventsInitialized { get; private set; }

    public static void UnSubscribePerLevelEvents ()
    {
        GameStart.UnSubscribeAll();
        PlayerSpawn.UnSubscribeAll();
        PlayerDead.UnSubscribeAll();
        GameQuit.UnSubscribeAll();
    }
}