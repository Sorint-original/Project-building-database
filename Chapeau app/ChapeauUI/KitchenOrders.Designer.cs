namespace ChapeauUI
{
    partial class KitchenOrders
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
            lblName = new Label();
            lblDate = new Label();
            lblTime = new Label();
            OrderTreeView = new TreeView();
            ShowUn = new Button();
            ShowTo = new Button();
            LShow = new Label();
            SuspendLayout();
            // 
            // btnLogoff
            // 
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
            LogoPanel.BackgroundImageLayout = ImageLayout.Stretch;
            LogoPanel.Location = new Point(669, 10);
            LogoPanel.Margin = new Padding(2, 1, 2, 1);
            LogoPanel.Name = "LogoPanel";
            LogoPanel.Size = new Size(120, 52);
            LogoPanel.TabIndex = 6;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(195, 32);
            lblName.Margin = new Padding(2, 0, 2, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(37, 15);
            lblName.TabIndex = 7;
            lblName.Text = "name";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(379, 32);
            lblDate.Margin = new Padding(2, 0, 2, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(30, 15);
            lblDate.TabIndex = 8;
            lblDate.Text = "date";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(548, 32);
            lblTime.Margin = new Padding(2, 0, 2, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(31, 15);
            lblTime.TabIndex = 9;
            lblTime.Text = "time";
            // 
            // OrderTreeView
            // 
            OrderTreeView.Location = new Point(33, 139);
            OrderTreeView.Name = "OrderTreeView";
            OrderTreeView.Size = new Size(590, 299);
            OrderTreeView.TabIndex = 10;
            // 
            // ShowUn
            // 
            ShowUn.Location = new Point(367, 97);
            ShowUn.Name = "ShowUn";
            ShowUn.Size = new Size(125, 36);
            ShowUn.TabIndex = 11;
            ShowUn.Text = "Show unprepared";
            ShowUn.UseVisualStyleBackColor = true;
            ShowUn.Click += ShowUn_Click;
            // 
            // ShowTo
            // 
            ShowTo.Location = new Point(498, 97);
            ShowTo.Name = "ShowTo";
            ShowTo.Size = new Size(125, 36);
            ShowTo.TabIndex = 12;
            ShowTo.Text = "Show today's orders";
            ShowTo.UseVisualStyleBackColor = true;
            ShowTo.Click += ShowTo_Click;
            // 
            // LShow
            // 
            LShow.AutoSize = true;
            LShow.Location = new Point(33, 108);
            LShow.Name = "LShow";
            LShow.Size = new Size(153, 15);
            LShow.TabIndex = 13;
            LShow.Text = "Showing unprepared orders";
            // 
            // OrderView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LShow);
            Controls.Add(ShowTo);
            Controls.Add(ShowUn);
            Controls.Add(OrderTreeView);
            Controls.Add(lblTime);
            Controls.Add(lblDate);
            Controls.Add(lblName);
            Controls.Add(LogoPanel);
            Controls.Add(btnLogoff);
            Name = "OrderView";
            Text = "OrderView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogoff;
        private Panel LogoPanel;
        private Label lblName;
        private Label lblDate;
        private Label lblTime;
        private TreeView OrderTreeView;
        private Button ShowUn;
        private Button ShowTo;
        private Label LShow;
    }
}