namespace BoltActionFix
{
    using FistVR;
    using HarmonyLib;
    using UnityEngine;

    public class BoltPatch : MonoBehaviour
    {
        [HarmonyPatch(typeof(LeverActionFirearm), "UpdateLever")]
        [HarmonyPrefix]
        public static void Patch(LeverActionFirearm __instance, 
            FVRViveHand ___m_hand, bool ___m_isHeld, ref bool ___m_isLeverReleasePressed,
            ref float ___curDistanceBetweenGrips, ref float ___lastDistanceBetweenGrips,
            ref bool ___m_isSpinning, ref bool ___m_isHammerCocked, ref bool ___m_isHammerCocked2,
            ref bool ___useLinearRacking, ref LeverActionFirearm.ZPos ___m_curLeverPos, 
            ref float ___m_tarLeverRot, ref Vector3 ___m_baseSpinPosition, 
            ref float ___m_rackingDisplacement)
        {
            if (!___m_hand.IsInStreamlinedMode) return;
            if (! ___m_isHeld) return;
            
            var flag = ___m_hand.OtherHand.Input.BYButtonPressed;
            var flag2 = ___m_hand.OtherHand.Input.BYButtonUp;
            
            
            if (!__instance.IsAltHeld && __instance.ForeGrip.m_hand != null)
            {
                ___m_isLeverReleasePressed = true;
                ___curDistanceBetweenGrips = Vector3.Distance(___m_hand.PalmTransform.position,
                    __instance.AltGrip.m_hand.PalmTransform.position);
                if (___lastDistanceBetweenGrips < 0f)
                    ___lastDistanceBetweenGrips = ___curDistanceBetweenGrips;
            }
            else ___lastDistanceBetweenGrips = -1f;
            
            // Checks it isn't spinning
            ___m_isSpinning = !__instance.IsAltHeld && __instance.CanSpin && flag;

            var flag4 = (___m_isHammerCocked || ___m_isHammerCocked2) &&
                        !___m_isSpinning &&
                        ___m_curLeverPos == LeverActionFirearm.ZPos.Rear;

            if (flag4 == false) flag4 = __instance.AltGrip == null && !__instance.IsAltHeld;

            if (flag4 && ___useLinearRacking)
               // __instance.SetBaseHandAngle(___m_hand);
            
            ___useLinearRacking = flag4;
            
            if (flag2)
            {
                ___m_tarLeverRot = 0f;
                __instance.PoseSpinHolder.localPosition = ___m_baseSpinPosition;
                ___lastDistanceBetweenGrips = ___curDistanceBetweenGrips;
                ___m_rackingDisplacement = 0f;
            }
        }
        
        [HarmonyPatch(typeof(FVRFireArm), "BeginInteraction")]
        [HarmonyPrefix]
        public static void Patch5(FVRFireArm __instance)
        {
            Debug.Log("This is a " + __instance.GetType());
        }
    }
}