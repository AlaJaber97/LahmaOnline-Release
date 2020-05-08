using System;
using System.Collections.Generic;
using System.Text;

namespace LahmaOnline
{
    public static class AppStatics
    {
        public static int UserID { get; set; }
        public static Guid GestID { get; set; }
        public static string Token { get; set; }
        public static BLL.M.Identity.UserProfile UserProfile { get; set; }
        public static int Language { get ; set ; }
        public static string CluterLanguage { get; set; }
        public static bool IsRTL { get; set; }
    }
}