using System.Text;

namespace SimpleWebPositionApp.Controllers {

    public static class Logger {
        private static readonly string path = Environment.CurrentDirectory;

        public static string Path => path;


        public static void writeLog(string msg) {
            if (!Directory.Exists(Path + "/temp")) {
                Directory.CreateDirectory(Path + "/temp");
            }
            File.AppendAllText(Path + "/temp/logger.log", String.Format("[{0}]:{1}\n", DateTime.Today.ToString("dd-MM-yyyy"), msg),Encoding.UTF8);
        }
    }
}
