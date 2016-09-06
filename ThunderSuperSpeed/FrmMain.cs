using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ThunderSuperSpeed
{
    public partial class FrmMain : Form
    {

        /// <summary>
        /// 注册表项位置
        /// </summary>
        private const string REGISTY = @"SOFTWARE\Thunder Network\Thunder";
        /// <summary>
        /// db数据库文件位置
        /// </summary>
        private string dbPath = "";

        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取注册表获取迅雷数据库文件位置
        /// </summary>
        /// <returns></returns>
        private string GetThunderPath()
        {
            string path = "";
            try
            {
                RegistryKey curUser = Registry.CurrentUser.OpenSubKey(REGISTY);
                if (curUser != null)
                {
                    path = curUser.GetValue("Path").ToString();
                }
                if (path.Equals(""))
                {
                    RegistryKey locMachine = Registry.LocalMachine.OpenSubKey(REGISTY);
                    if (locMachine != null)
                    {
                        path = curUser.GetValue("Path").ToString();
                    }
                }
                if (!path.Equals(""))
                {
                    if (path.Contains(@"Program\Thunder.exe"))
                    {
                        path = path.Substring(0, path.LastIndexOf(@"Program\Thunder.exe"));
                    }
                    path += @"Profiles\TaskDb.dat";
                }
            }catch
            {
                return "";
            }
            return path;
        }

        private void btnPathSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "请选择迅雷安装目录下的TaskDb.dat文件";
            openFile.Filter = "迅雷数据库文件|TaskDb.dat";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.dbPath = openFile.FileName;
                this.txtPath.Text = dbPath;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (dbPath == null || dbPath.Equals(""))
            {
                MessageBox.Show("请选择文件路径。");
            }
            else
            {
                try
                {
                    string count = SQLiteDAL.DoCrack(dbPath);
                    MessageBox.Show("破解完成，" + count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void txtPath_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.txtPath, this.txtPath.Text);
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            dbPath = GetThunderPath();

            if (dbPath.Equals(""))
            {
                MessageBox.Show("未找到迅雷高速通道数据文件，请手动选择。");
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Title = "请选择迅雷安装目录下的TaskDb.dat文件";
                openFile.Filter = "迅雷数据库文件|TaskDb.dat";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    this.dbPath = openFile.FileName;
                    this.txtPath.Text = dbPath;
                }
            }
            else
            {
                this.txtPath.Text = dbPath;
            }
        }
    }
}
