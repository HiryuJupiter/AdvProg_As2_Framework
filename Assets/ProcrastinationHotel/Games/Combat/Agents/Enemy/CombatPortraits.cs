using System.Collections;
using UnityEngine;

namespace HiryuTK.GameRoomService
{
    public static class CombatPortraits
    {
        //public static string enemyBird = "" +
        //    ">`) \n" +
        //    "( \\ \n" +
        //    " ^  ";

        //public static string enemyDuck = ""+
        //    "   _    \n"+
        //    "> (.)__ \n" +
        //    "  (___ /";

        //public static string enemyBandana = "" +
        //    "\\o~ \n" +
        //    " |\\ \n" +
        //    "/ \\ ";

        //Each must have 7 x 3 size
        public static string enemyPortrait_Idle = "" +
            " @~    \n" +
            "/|\\    \n" +
            "/ \\    ";

        public static string enemyPortrait_Duck = "" +
            "     \n" +
            " @~__ \n" +
            "//  \\\\ ";

        public static string enemyPortrait_Jump = "" +
            "\\@~/_//\n" +
            "       \n" +
            "       ";

        public static string playerPortrait_Idle = "" +
            "   ~@  \n" +
            "    /|\\\n" +
            "    / \\";

        public static string playerPortrait_Duck = "" +
            "       \n" +
            " __~@  \n" +
            "//  \\\\ ";

        public static string playerPortrait_Jump = "" +
            "\\\\_\\~@/\n" +
            "       \n" +
            "       ";

        public static string GraveRIP = "" +
            ".-----.\n" +
            "|R.I.P|\n" +
            "|_____|";
    }
}