﻿namespace ChapeauUI
{
    partial class BarOrders
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
            SetPanel = new Panel();
            BSETItem = new Button();
            RBReady = new RadioButton();
            RBPreparing = new RadioButton();
            RBPlaced = new RadioButton();
            label3 = new Label();
            CommentPanel = new Panel();
            label1 = new Label();
            CommentBox = new TextBox();
            LShow = new Label();
            ShowTo = new Button();
            ShowUn = new Button();
            OrderTreeView = new TreeView();
            lblTime = new Label();
            lblDate = new Label();
            lblName = new Label();
            LogoPanel = new Panel();
            btnLogoff = new Button();
            SetPanel.SuspendLayout();
            CommentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // SetPanel
            // 
            SetPanel.Controls.Add(BSETItem);
            SetPanel.Controls.Add(RBReady);
            SetPanel.Controls.Add(RBPreparing);
            SetPanel.Controls.Add(RBPlaced);
            SetPanel.Controls.Add(label3);
            SetPanel.Location = new Point(640, 144);
            SetPanel.Name = "SetPanel";
            SetPanel.Size = new Size(226, 134);
            SetPanel.TabIndex = 26;
            // 
            // BSETItem
            // 
            BSETItem.Location = new Point(111, 48);
            BSETItem.Name = "BSETItem";
            BSETItem.Size = new Size(88, 35);
            BSETItem.TabIndex = 7;
            BSETItem.Text = "Set";
            BSETItem.UseVisualStyleBackColor = true;
            BSETItem.Click += BSETItem_Click;
            // 
            // RBReady
            // 
            RBReady.AutoSize = true;
            RBReady.Location = new Point(12, 81);
            RBReady.Name = "RBReady";
            RBReady.Size = new Size(57, 19);
            RBReady.TabIndex = 6;
            RBReady.TabStop = true;
            RBReady.Text = "Ready";
            RBReady.UseVisualStyleBackColor = true;
            // 
            // RBPreparing
            // 
            RBPreparing.AutoSize = true;
            RBPreparing.Location = new Point(12, 56);
            RBPreparing.Name = "RBPreparing";
            RBPreparing.Size = new Size(76, 19);
            RBPreparing.TabIndex = 5;
            RBPreparing.TabStop = true;
            RBPreparing.Text = "Preparing";
            RBPreparing.UseVisualStyleBackColor = true;
            // 
            // RBPlaced
            // 
            RBPlaced.AutoSize = true;
            RBPlaced.Location = new Point(12, 31);
            RBPlaced.Name = "RBPlaced";
            RBPlaced.Size = new Size(60, 19);
            RBPlaced.TabIndex = 4;
            RBPlaced.TabStop = true;
            RBPlaced.Text = "Placed";
            RBPlaced.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 13);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 3;
            label3.Text = "Change Status:";
            // 
            // CommentPanel
            // 
            CommentPanel.Controls.Add(label1);
            CommentPanel.Controls.Add(CommentBox);
            CommentPanel.Location = new Point(639, 284);
            CommentPanel.Name = "CommentPanel";
            CommentPanel.Size = new Size(225, 154);
            CommentPanel.TabIndex = 25;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 14);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 1;
            label1.Text = "Comment:";
            // 
            // CommentBox
            // 
            CommentBox.BorderStyle = BorderStyle.FixedSingle;
            CommentBox.Location = new Point(14, 32);
            CommentBox.Multiline = true;
            CommentBox.Name = "CommentBox";
            CommentBox.ReadOnly = true;
            CommentBox.Size = new Size(199, 112);
            CommentBox.TabIndex = 0;
            // 
            // LShow
            // 
            LShow.AutoSize = true;
            LShow.Location = new Point(33, 108);
            LShow.Name = "LShow";
            LShow.Size = new Size(153, 15);
            LShow.TabIndex = 24;
            LShow.Text = "Showing unprepared orders";
            // 
            // ShowTo
            // 
            ShowTo.Location = new Point(509, 97);
            ShowTo.Name = "ShowTo";
            ShowTo.Size = new Size(125, 36);
            ShowTo.TabIndex = 23;
            ShowTo.Text = "Show finished orders";
            ShowTo.UseVisualStyleBackColor = true;
            ShowTo.Click += ShowTo_Click;
            // 
            // ShowUn
            // 
            ShowUn.Location = new Point(378, 97);
            ShowUn.Name = "ShowUn";
            ShowUn.Size = new Size(125, 36);
            ShowUn.TabIndex = 22;
            ShowUn.Text = "Show unprepared";
            ShowUn.UseVisualStyleBackColor = true;
            ShowUn.Click += ShowUn_Click;
            // 
            // OrderTreeView
            // 
            OrderTreeView.FullRowSelect = true;
            OrderTreeView.Location = new Point(33, 139);
            OrderTreeView.Name = "OrderTreeView";
            OrderTreeView.Size = new Size(601, 299);
            OrderTreeView.TabIndex = 21;
            OrderTreeView.AfterSelect += OrderTreeView_AfterSelect;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(603, 32);
            lblTime.Margin = new Padding(2, 0, 2, 0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(31, 15);
            lblTime.TabIndex = 20;
            lblTime.Text = "time";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(407, 32);
            lblDate.Margin = new Padding(2, 0, 2, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(30, 15);
            lblDate.TabIndex = 19;
            lblDate.Text = "date";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(195, 32);
            lblName.Margin = new Padding(2, 0, 2, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(37, 15);
            lblName.TabIndex = 18;
            lblName.Text = "name";
            // 
            // LogoPanel
            // 
            LogoPanel.BackgroundImage = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("Logo");
            LogoPanel.BackgroundImageLayout = ImageLayout.Stretch;
            LogoPanel.Location = new Point(744, 10);
            LogoPanel.Margin = new Padding(2, 1, 2, 1);
            LogoPanel.Name = "LogoPanel";
            LogoPanel.Size = new Size(120, 52);
            LogoPanel.TabIndex = 17;
            // 
            // btnLogoff
            // 
            btnLogoff.BackgroundImage = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("Log");
            btnLogoff.BackgroundImageLayout = ImageLayout.Stretch;
            btnLogoff.Location = new Point(11, 10);
            btnLogoff.Margin = new Padding(2, 1, 2, 1);
            btnLogoff.Name = "btnLogoff";
            btnLogoff.Size = new Size(53, 59);
            btnLogoff.TabIndex = 16;
            btnLogoff.UseVisualStyleBackColor = true;
            btnLogoff.Click += btnLogoff_Click;
            // 
            // BarOrders
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(887, 450);
            Controls.Add(SetPanel);
            Controls.Add(CommentPanel);
            Controls.Add(LShow);
            Controls.Add(ShowTo);
            Controls.Add(ShowUn);
            Controls.Add(OrderTreeView);
            Controls.Add(lblTime);
            Controls.Add(lblDate);
            Controls.Add(lblName);
            Controls.Add(LogoPanel);
            Controls.Add(btnLogoff);
            Name = "BarOrders";
            Text = "BarOrders";
            SetPanel.ResumeLayout(false);
            SetPanel.PerformLayout();
            CommentPanel.ResumeLayout(false);
            CommentPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel SetPanel;
        private Button BSETItem;
        private RadioButton RBReady;
        private RadioButton RBPreparing;
        private RadioButton RBPlaced;
        private Label label3;
        private Panel CommentPanel;
        private Label label1;
        private TextBox CommentBox;
        private Label LShow;
        private Button ShowTo;
        private Button ShowUn;
        private TreeView OrderTreeView;
        private Label lblTime;
        private Label lblDate;
        private Label lblName;
        private Panel LogoPanel;
        private Button btnLogoff;
    }
}