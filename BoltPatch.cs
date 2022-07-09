namespace BoltActionFix
{
    using FistVR;
    using HarmonyLib;
    using UnityEngine;
    
    public class BoltPatch : MonoBehaviour
    {
        [HarmonyPatch(typeof(FVRPhysicalObject), "Awake")]
        [HarmonyPrefix]
        public static void Patch()
        {
            Debug.Log("asd");
        }

        private void Update()
        {
            Debug.Log("asdasd");
        }

        private void Start()
        {
            Harmony.CreateAndPatchAll(typeof(BoltPatch));
        }
    }
}