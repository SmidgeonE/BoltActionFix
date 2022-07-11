using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace BoltActionFix
{
    [BepInPlugin("asd", "adsads", "1.0.0")]
    [BepInProcess("h3vr.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Debug.Log("Beep");
            Harmony.CreateAndPatchAll(typeof(BoltPatch));
        }
    }
}