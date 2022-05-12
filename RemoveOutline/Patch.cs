using Kingmaker;
using Kingmaker.PubSubSystem;
using Kingmaker.View;
using Kingmaker.Visual;
using HarmonyLib;
using UnityEngine;

namespace RemoveOutline
{
    [HarmonyPatch(typeof(UnitEntityView), nameof(UnitEntityView.UpdateHighlight))]
    static class UnitEntityView_UpdateHighlight_Patch
    {
        static bool Prefix(UnitEntityView __instance, ref UnitMultiHighlight ___m_Highlighter, bool raiseEvent = true)
        {
            if(!__instance.MouseHighlighted && !__instance.DragBoxHighlighted && !__instance.EntityData.Descriptor.State.IsDead && !__instance.EntityData.IsPlayersEnemy &&
               Game.Instance.Player.PartyAndPets.Contains(__instance.EntityData))
            {
                ___m_Highlighter.BaseColor = Color.clear;

                if (raiseEvent)
                {
                    EventBus.RaiseEvent<IUnitHighlightUIHandler>(delegate (IUnitHighlightUIHandler h)
                    {
                        h.HandleHighlightChange(__instance);
                    }, true);
                }
                return false;
            }
            return true;
        }
    }
}
