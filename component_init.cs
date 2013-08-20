namespace spreadAuto
{
    partial class GUI
    {

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.autoTextBox = new System.Windows.Forms.TextBox();
            this.browserViewBox = new System.Windows.Forms.WebBrowser();
            this.jsInjectBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // autoTextBox
            // 
            this.autoTextBox.Location = new System.Drawing.Point(12, 12);
            this.autoTextBox.MinimumSize = new System.Drawing.Size(50, 50);
            this.autoTextBox.Multiline = true;
            this.autoTextBox.Name = "autoTextBox";
            this.autoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.autoTextBox.Size = new System.Drawing.Size(923, 53);
            this.autoTextBox.TabIndex = 0;
            // 
            // browserViewBox
            // 
            this.browserViewBox.Location = new System.Drawing.Point(12, 118);
            this.browserViewBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserViewBox.Name = "browserViewBox";
            this.browserViewBox.Size = new System.Drawing.Size(923, 668);
            this.browserViewBox.TabIndex = 1;
            this.browserViewBox.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.browserViewBox_Navigated);
            // 
            // jsInjectBox
            // 
            this.jsInjectBox.Location = new System.Drawing.Point(12, 71);
            this.jsInjectBox.Name = "jsInjectBox";
            this.jsInjectBox.Size = new System.Drawing.Size(922, 20);
            this.jsInjectBox.TabIndex = 2;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 798);
            this.Controls.Add(this.jsInjectBox);
            this.Controls.Add(this.browserViewBox);
            this.Controls.Add(this.autoTextBox);
            this.Name = "GUI";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox autoTextBox;
        public System.Windows.Forms.WebBrowser browserViewBox;
        public System.Windows.Forms.TextBox jsInjectBox;
    }
}

