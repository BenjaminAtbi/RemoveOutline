using UnityEngine;
using UnityModManagerNet;
using UnityEngine.UI;
using HarmonyLib;

namespace RemoveOutline
{
    static class Main
    {
        public static bool Enabled;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }
    }
}