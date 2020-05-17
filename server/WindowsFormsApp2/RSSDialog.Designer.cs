namespace WindowsFormsApp2
{
    partial class RSSDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartRSS_button = new System.Windows.Forms.Button();
            this.RSSwebBrowser = new System.Windows.Forms.WebBrowser();
            this.EnableCheckboxes_button = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartRSS_button
            // 
            this.StartRSS_button.Location = new System.Drawing.Point(12, 390);
            this.StartRSS_button.Name = "StartRSS_button";
            this.StartRSS_button.Size = new System.Drawing.Size(102, 23);
            this.StartRSS_button.TabIndex = 0;
            this.StartRSS_button.Text = "Start";
            this.StartRSS_button.UseVisualStyleBackColor = true;
            this.StartRSS_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // RSSwebBrowser
            // 
            this.RSSwebBrowser.Location = new System.Drawing.Point(12, 12);
            this.RSSwebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.RSSwebBrowser.Name = "RSSwebBrowser";
            this.RSSwebBrowser.ScriptErrorsSuppressed = true;
            this.RSSwebBrowser.Size = new System.Drawing.Size(776, 335);
            this.RSSwebBrowser.TabIndex = 1;
            // 
            // EnableCheckboxes_button
            // 
            this.EnableCheckboxes_button.Location = new System.Drawing.Point(150, 386);
            this.EnableCheckboxes_button.Name = "EnableCheckboxes_button";
            this.EnableCheckboxes_button.Size = new System.Drawing.Size(150, 27);
            this.EnableCheckboxes_button.TabIndex = 2;
            this.EnableCheckboxes_button.Text = "EnableCheckboxes";
            this.EnableCheckboxes_button.UseVisualStyleBackColor = true;
            this.EnableCheckboxes_button.Click += new System.EventHandler(this.DowonloadChoosePage_button_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(351, 386);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(102, 24);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // RSSDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.EnableCheckboxes_button);
            this.Controls.Add(this.RSSwebBrowser);
            this.Controls.Add(this.StartRSS_button);
            this.Name = "RSSDialog";
            this.Text = "Form2";
            this.ResizeEnd += new System.EventHandler(this.ResizeBrowoser);
            this.SizeChanged += new System.EventHandler(this.ResizeBrowoser);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartRSS_button;
        private System.Windows.Forms.WebBrowser RSSwebBrowser;
        private System.Windows.Forms.Button EnableCheckboxes_button;
        private System.Windows.Forms.Button DownloadButton;
    }
}