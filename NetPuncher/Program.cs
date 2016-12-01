using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NetPuncher
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        internal static int scannedCount = 0;
        internal static int totalCount = 0;
        internal static Dictionary<String, String> PortToIndex = new Dictionary<String, String>();
        [STAThread]
        static void Main()
        {
            PortToIndex.Add("445", "3");
            PortToIndex.Add("3306", "4");
            PortToIndex.Add("80", "5");
            PortToIndex.Add("443", "6");
            PortToIndex.Add("22", "7");
            PortToIndex.Add("21", "8");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
}
