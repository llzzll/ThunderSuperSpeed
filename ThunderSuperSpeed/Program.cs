using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ThunderSuperSpeed
{
    static class Program
    {
        /// <summary>
        /// 加载程序集加载失败事件
        /// </summary>
        static Program()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                string temp = Environment.GetEnvironmentVariable("TEMP");
                string filePath = temp + "\\System.Data.SQLite.dll";
                if (!File.Exists(filePath))
                {
                    ExtractDll(filePath);
                }
                Assembly ass = Assembly.LoadFrom(filePath);
                return ass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从资源文件里释放DLL
        /// </summary>
        /// <param name="filePath"></param>
        private static void ExtractDll(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            byte[] buffer = ThunderSuperSpeed.Properties.Resources.System_Data_SQLite;
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
        }


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!CheckThunderExist()) return;
            Application.Run(new FrmMain());
        }

        /// <summary>
        /// 检查迅雷是否开启
        /// </summary>
        /// <returns></returns>
        private static bool CheckThunderExist()
        {
            Process[] temp = Process.GetProcessesByName("Thunder");
            if (temp != null && temp.Length > 0)
            {
                if (MessageBox.Show("请关闭迅雷后点击确定！", "检测到迅雷正在运行", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    return CheckThunderExist();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


    }
}
