﻿using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace DropSome
{
    [HarmonyPatch(typeof(ITab_Pawn_Gear))]
    public static class ITabPawnGear_Patch
    {
        private static int initialValue = 1;
        private static bool changeTip = false;

        [HarmonyPatch("DrawThingRow")]
        [HarmonyPrefix]
        public static void DrawThingRow_Pre(Thing thing)
        {
            changeTip = !thing.def.destroyOnDrop && thing.stackCount > 1;
        }

        [HarmonyPatch(typeof(TooltipHandler), nameof(TooltipHandler.TipRegion), typeof(Rect), typeof(TipSignal))]
        [HarmonyPrefix]
        public static void TooltipHandler_TipRegion_Pre(ref TipSignal tip)
        {
            if (changeTip && tip.text == Strings.OriginalDropTooltip)
            {
                tip.text = Strings.StackDropTooltip;
            }
            changeTip = false;
        }

        [HarmonyPatch("InterfaceDrop")]
        [HarmonyPrefix]
        public static bool InterfaceDrop_Pre(Thing t, ITab_Pawn_Gear __instance)
        {
            if (!t.def.destroyOnDrop && t.stackCount > 1 && Event.current.shift)
            {
                Find.WindowStack.Add(new Dialog_Slider(Strings.DropCount.Translate(t.LabelNoCount, t), 1, t.stackCount, drop, initialValue));
                return false;

            }
            return true;

            void drop(int count)
            {
                initialValue = count; 
                Pawn pawn = Traverse.Create(__instance).Property("SelPawnForGear").GetValue<Pawn>();
                Thing toDrop = t.SplitOff(count);
                GenDrop.TryDropSpawn(toDrop, pawn.Position, pawn.Map, ThingPlaceMode.Near, out Thing _);
            }
        }
    }
}
