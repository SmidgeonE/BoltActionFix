namespace BoltActionFix
{
    using FistVR;
    using HarmonyLib;
    using UnityEngine;

    public class BoltPatch : MonoBehaviour
    {
        [HarmonyPatch(typeof(LeverActionFirearm), "UpdateLever")]
        [HarmonyPrefix]
        public static void Patch(LeverActionFirearm __instance)
        {
            
        }
        
        [HarmonyPatch(typeof(FVRFireArm), "BeginInteraction")]
        [HarmonyPrefix]
        public static void Patch5(FVRFireArm __instance)
        {
            Debug.Log("This is a " + __instance.GetType());
        }
    }
}