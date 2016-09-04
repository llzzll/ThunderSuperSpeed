namespace ThunderSuperSpeed
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPathSelect = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(218, 55);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(510, 35);
            this.txtPath.TabIndex = 0;
            this.txtPath.MouseEnter += new System.EventHandler(this.txtPath_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "迅雷文件目录：";
            // 
            // btnPathSelect
            // 
            this.btnPathSelect.Location = new System.Drawing.Point(756, 54);
            this.btnPathSelect.Name = "btnPathSelect";
            this.btnPathSelect.Size = new System.Drawing.Size(75, 41);
            this.btnPathSelect.TabIndex = 2;
            this.btnPathSelect.Text = "...";
            this.btnPathSelect.UseVisualStyleBackColor = true;
            this.btnPathSelect.Click += new System.EventHandler(this.btnPathSelect_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("宋体", 13F);
            this.btn_OK.Location = new System.Drawing.Point(331, 190);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(241, 48);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "破解";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(45, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(670, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "*请选择迅雷安装目录下Profiles文件夹中的TaskDb.bat文件。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(45, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(790, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "*本软件只是重新打开被举报文件的高速通道功能，并不是破解高速通道。";
            // 
            // FrmMain
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 252);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnPathSelect);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "高速通道一键破解";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPathSelect;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

