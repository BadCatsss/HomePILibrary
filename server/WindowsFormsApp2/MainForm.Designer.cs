namespace WindowsFormsApp2
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DownloadButton = new System.Windows.Forms.Button();
            this.TitleURL_TextB = new System.Windows.Forms.TextBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.Deamon_CheckB = new System.Windows.Forms.CheckBox();
            this.DownloadList = new System.Windows.Forms.Button();
            this.DownloadListFile = new System.Windows.Forms.Button();
            this.LinksList = new System.Windows.Forms.TextBox();
            this.radioB_AllPages = new System.Windows.Forms.RadioButton();
            this.radioB_UniqPages = new System.Windows.Forms.RadioButton();
            this.UrlLog_CheckB = new System.Windows.Forms.CheckBox();
            this.RSSButton = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.LocalD_Rb = new System.Windows.Forms.RadioButton();
            this.RemoteD_Rb = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IP_Tb = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(9, 67);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(134, 39);
            this.DownloadButton.TabIndex = 0;
            this.DownloadButton.Text = "Скачать статью";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.Download_Click);
            // 
            // TitleURL_TextB
            // 
            this.TitleURL_TextB.Location = new System.Drawing.Point(112, 10);
            this.TitleURL_TextB.Margin = new System.Windows.Forms.Padding(2);
            this.TitleURL_TextB.Name = "TitleURL_TextB";
            this.TitleURL_TextB.Size = new System.Drawing.Size(545, 20);
            this.TitleURL_TextB.TabIndex = 1;
            this.TitleURL_TextB.TextChanged += new System.EventHandler(this.URLChanged);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(7, 39);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TitleLabel.MinimumSize = new System.Drawing.Size(8, 8);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(27, 13);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "Title";
            this.TitleLabel.Visible = false;
            // 
            // Deamon_CheckB
            // 
            this.Deamon_CheckB.AutoSize = true;
            this.Deamon_CheckB.Checked = true;
            this.Deamon_CheckB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Deamon_CheckB.Location = new System.Drawing.Point(9, 236);
            this.Deamon_CheckB.Margin = new System.Windows.Forms.Padding(2);
            this.Deamon_CheckB.Name = "Deamon_CheckB";
            this.Deamon_CheckB.Size = new System.Drawing.Size(174, 17);
            this.Deamon_CheckB.TabIndex = 4;
            this.Deamon_CheckB.Text = "Работать в фоновом режиме";
            this.Deamon_CheckB.UseVisualStyleBackColor = true;
            // 
            // DownloadList
            // 
            this.DownloadList.Location = new System.Drawing.Point(9, 111);
            this.DownloadList.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadList.Name = "DownloadList";
            this.DownloadList.Size = new System.Drawing.Size(134, 44);
            this.DownloadList.TabIndex = 5;
            this.DownloadList.Text = "Скачать все из списка";
            this.DownloadList.UseVisualStyleBackColor = true;
            this.DownloadList.Click += new System.EventHandler(this.DownloadList_Click);
            // 
            // DownloadListFile
            // 
            this.DownloadListFile.Location = new System.Drawing.Point(9, 160);
            this.DownloadListFile.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadListFile.Name = "DownloadListFile";
            this.DownloadListFile.Size = new System.Drawing.Size(134, 55);
            this.DownloadListFile.TabIndex = 6;
            this.DownloadListFile.Text = "Найти ссылки в файле";
            this.DownloadListFile.UseVisualStyleBackColor = true;
            this.DownloadListFile.Click += new System.EventHandler(this.DownloadListFile_Click);
            // 
            // LinksList
            // 
            this.LinksList.Location = new System.Drawing.Point(417, 67);
            this.LinksList.Margin = new System.Windows.Forms.Padding(2);
            this.LinksList.Multiline = true;
            this.LinksList.Name = "LinksList";
            this.LinksList.Size = new System.Drawing.Size(306, 361);
            this.LinksList.TabIndex = 7;
            // 
            // radioB_AllPages
            // 
            this.radioB_AllPages.AutoSize = true;
            this.radioB_AllPages.Checked = true;
            this.radioB_AllPages.Location = new System.Drawing.Point(0, 17);
            this.radioB_AllPages.Margin = new System.Windows.Forms.Padding(2);
            this.radioB_AllPages.Name = "radioB_AllPages";
            this.radioB_AllPages.Size = new System.Drawing.Size(266, 17);
            this.radioB_AllPages.TabIndex = 8;
            this.radioB_AllPages.TabStop = true;
            this.radioB_AllPages.Text = "Скачать все статьи(перезаписать одинаковые)";
            this.radioB_AllPages.UseVisualStyleBackColor = true;
            // 
            // radioB_UniqPages
            // 
            this.radioB_UniqPages.AutoSize = true;
            this.radioB_UniqPages.Location = new System.Drawing.Point(4, 39);
            this.radioB_UniqPages.Margin = new System.Windows.Forms.Padding(2);
            this.radioB_UniqPages.Name = "radioB_UniqPages";
            this.radioB_UniqPages.Size = new System.Drawing.Size(386, 17);
            this.radioB_UniqPages.TabIndex = 9;
            this.radioB_UniqPages.Text = "Скачать последние добавленые/уникальные стати(исключая повторы)";
            this.radioB_UniqPages.UseVisualStyleBackColor = true;
            // 
            // UrlLog_CheckB
            // 
            this.UrlLog_CheckB.AutoSize = true;
            this.UrlLog_CheckB.Checked = true;
            this.UrlLog_CheckB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UrlLog_CheckB.Location = new System.Drawing.Point(9, 258);
            this.UrlLog_CheckB.Margin = new System.Windows.Forms.Padding(2);
            this.UrlLog_CheckB.Name = "UrlLog_CheckB";
            this.UrlLog_CheckB.Size = new System.Drawing.Size(286, 17);
            this.UrlLog_CheckB.TabIndex = 10;
            this.UrlLog_CheckB.Text = "Вести лог скаченных статтей (запоминать ссылки)";
            this.UrlLog_CheckB.UseVisualStyleBackColor = true;
            // 
            // RSSButton
            // 
            this.RSSButton.Location = new System.Drawing.Point(9, 353);
            this.RSSButton.Margin = new System.Windows.Forms.Padding(2);
            this.RSSButton.Name = "RSSButton";
            this.RSSButton.Size = new System.Drawing.Size(165, 27);
            this.RSSButton.TabIndex = 11;
            this.RSSButton.Text = "Открыть RSS Feader";
            this.RSSButton.UseVisualStyleBackColor = true;
            this.RSSButton.Click += new System.EventHandler(this.RSSButton_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // LocalD_Rb
            // 
            this.LocalD_Rb.AutoSize = true;
            this.LocalD_Rb.Location = new System.Drawing.Point(0, 16);
            this.LocalD_Rb.Margin = new System.Windows.Forms.Padding(2);
            this.LocalD_Rb.Name = "LocalD_Rb";
            this.LocalD_Rb.Size = new System.Drawing.Size(75, 17);
            this.LocalD_Rb.TabIndex = 12;
            this.LocalD_Rb.TabStop = true;
            this.LocalD_Rb.Text = "Локально";
            this.LocalD_Rb.UseVisualStyleBackColor = true;
            // 
            // RemoteD_Rb
            // 
            this.RemoteD_Rb.AutoSize = true;
            this.RemoteD_Rb.Checked = true;
            this.RemoteD_Rb.Location = new System.Drawing.Point(0, 38);
            this.RemoteD_Rb.Margin = new System.Windows.Forms.Padding(2);
            this.RemoteD_Rb.Name = "RemoteD_Rb";
            this.RemoteD_Rb.Size = new System.Drawing.Size(136, 17);
            this.RemoteD_Rb.TabIndex = 13;
            this.RemoteD_Rb.TabStop = true;
            this.RemoteD_Rb.Text = "На удаленный сервер";
            this.RemoteD_Rb.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioB_AllPages);
            this.groupBox1.Controls.Add(this.radioB_UniqPages);
            this.groupBox1.Location = new System.Drawing.Point(9, 280);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(381, 67);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки сохранения";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.IP_Tb);
            this.groupBox2.Controls.Add(this.LocalD_Rb);
            this.groupBox2.Controls.Add(this.RemoteD_Rb);
            this.groupBox2.Location = new System.Drawing.Point(188, 67);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(150, 81);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки загрузки";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "IP:";
            // 
            // IP_Tb
            // 
            this.IP_Tb.Location = new System.Drawing.Point(27, 58);
            this.IP_Tb.Margin = new System.Windows.Forms.Padding(2);
            this.IP_Tb.Name = "IP_Tb";
            this.IP_Tb.Size = new System.Drawing.Size(76, 20);
            this.IP_Tb.TabIndex = 14;
            this.IP_Tb.Text = "192.168.0.102";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 437);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RSSButton);
            this.Controls.Add(this.UrlLog_CheckB);
            this.Controls.Add(this.LinksList);
            this.Controls.Add(this.DownloadListFile);
            this.Controls.Add(this.DownloadList);
            this.Controls.Add(this.Deamon_CheckB);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.TitleURL_TextB);
            this.Controls.Add(this.DownloadButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.TextBox TitleURL_TextB;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.CheckBox Deamon_CheckB;
        private System.Windows.Forms.Button DownloadList;
        private System.Windows.Forms.Button DownloadListFile;
        private System.Windows.Forms.TextBox LinksList;
        private System.Windows.Forms.RadioButton radioB_AllPages;
        private System.Windows.Forms.RadioButton radioB_UniqPages;
        private System.Windows.Forms.CheckBox UrlLog_CheckB;
        private System.Windows.Forms.Button RSSButton;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.RadioButton LocalD_Rb;
        private System.Windows.Forms.RadioButton RemoteD_Rb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IP_Tb;
    }
}

