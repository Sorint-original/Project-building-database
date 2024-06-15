namespace ChapeauUI
{
    partial class OrderView
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
            btnLogoff = new Button();
            LogoPanel = new Panel();
            SuspendLayout();
            // 
            // btnLogoff
            // 
            btnLogoff.BackgroundImage = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("Log");
            btnLogoff.BackgroundImageLayout = ImageLayout.Stretch;
            btnLogoff.Location = new Point(11, 10);
            btnLogoff.Margin = new Padding(2, 1, 2, 1);
            btnLogoff.Name = "btnLogoff";
            btnLogoff.Size = new Size(53, 59);
            btnLogoff.TabIndex = 2;
            btnLogoff.UseVisualStyleBackColor = true;
            btnLogoff.Click += btnLogoff_Click;
            // 
            // LogoPanel
            // 
            LogoPanel.BackgroundImage = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("Logo");
            LogoPanel.BackgroundImageLayout = ImageLayout.Stretch;
            LogoPanel.Location = new Point(669, 10);
            LogoPanel.Margin = new Padding(2, 1, 2, 1);
            LogoPanel.Name = "LogoPanel";
            LogoPanel.Size = new Size(120, 52);
            LogoPanel.TabIndex = 6;
            // 
            // OrderView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LogoPanel);
            Controls.Add(btnLogoff);
            Name = "OrderView";
            Text = "OrderView";
            ResumeLayout(false);
        }

        #endregion

        private Button btnLogoff;
        private Panel LogoPanel;
    }
}