using System.Configuration;

namespace Twos
{
    public static class GameSettings
    {
        public static string LogOutputDirectory
        {
            get { return ConfigurationManager.AppSettings["LogOutputDirectory"]; }
        }

        public static string LogExtension
        {
            get { return ConfigurationManager.AppSettings["LogExtension"]; }
        }
    }
}
