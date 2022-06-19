using HarmonyLib;
using Verse;

namespace DropSome
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        static Main()
        {
            var harmony = new Harmony(Strings.ModId);
            harmony.PatchAll();
        }
    }
}
