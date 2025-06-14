using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace BuyAll;

internal static class ModInfo
{
    internal const string Guid = "me.pocke.buy-all";
    internal const string Name = "Buy all";
    internal const string Version = "1.0.0";
}

[BepInPlugin(ModInfo.Guid, ModInfo.Name, ModInfo.Version)]
internal class BuyAll : BaseUnityPlugin
{
    internal static BuyAll Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        new Harmony("BuyAll").PatchAll();
    }

    public static void Log(object message)
    {
        Instance.Logger.LogInfo(message);
    }
}
