using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace DropSome
{
    public static class Strings
    {
        public const string ModId = "kathanon.DropSome";
        public const string Prefix = ModId + ".";

        public const string DropCount = Prefix + "DropCount";

        public static readonly string StackDropTooltip = (Prefix + "StackDropTooltip").Translate();

        public static readonly string OriginalDropTooltip = "DropThing".Translate();
    }
}
