namespace ChapeauUI
{
    partial class PaymentForm
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
            TableNumberBox = new ComboBox();
            AllBillItemsList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            CurentItemsPayableList = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            AddItemButton = new Button();
            RemoveItemButton = new Button();
            FinalizyePaymentButton = new Button();
            PaymentTypeBox = new ComboBox();
            TipAmountField = new TextBox();
            FeedbackField = new RichTextBox();
            VatLowLable = new Label();
            VatHighLable = new Label();
            TotalLable = new Label();
            FeedbackLable = new Label();
            TipAmountLable = new Label();
            PaymentTypeLable = new Label();
            TableNumberLable = new Label();
            PaymentLable = new Label();
            VatLowValue = new Label();
            VatHighValue = new Label();
            TotalValue = new Label();
            SuspendLayout();
            // 
            // TableNumberBox
            // 
            TableNumberBox.FormattingEnabled = true;
            TableNumberBox.Location = new Point(179, 61);
            TableNumberBox.Name = "TableNumberBox";
            TableNumberBox.Size = new Size(59, 23);
            TableNumberBox.TabIndex = 0;
            TableNumberBox.SelectedIndexChanged += TableNumberBox_SelectedIndexChanged;
            // 
            // AllBillItemsList
            // 
            AllBillItemsList.AutoArrange = false;
            AllBillItemsList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            AllBillItemsList.Location = new Point(33, 118);
            AllBillItemsList.MultiSelect = false;
            AllBillItemsList.Name = "AllBillItemsList";
            AllBillItemsList.Size = new Size(205, 284);
            AllBillItemsList.TabIndex = 1;
            AllBillItemsList.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Quantity";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Price";
            // 
            // CurentItemsPayableList
            // 
            CurentItemsPayableList.AutoArrange = false;
            CurentItemsPayableList.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6 });
            CurentItemsPayableList.Location = new Point(279, 118);
            CurentItemsPayableList.MultiSelect = false;
            CurentItemsPayableList.Name = "CurentItemsPayableList";
            CurentItemsPayableList.Size = new Size(205, 284);
            CurentItemsPayableList.TabIndex = 2;
            CurentItemsPayableList.UseCompatibleStateImageBehavior = false;
            CurentItemsPayableList.SelectedIndexChanged += CurentItemsPayableList_SelectedIndexChanged;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Name";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Quantity";
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Price";
            // 
            // AddItemButton
            // 
            AddItemButton.Location = new Point(92, 415);
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(81, 23);
            AddItemButton.TabIndex = 3;
            AddItemButton.Text = "Add Item";
            AddItemButton.UseVisualStyleBackColor = true;
            AddItemButton.Click += AddItemButton_Click;
            // 
            // RemoveItemButton
            // 
            RemoveItemButton.Location = new Point(337, 415);
            RemoveItemButton.Name = "RemoveItemButton";
            RemoveItemButton.Size = new Size(88, 23);
            RemoveItemButton.TabIndex = 4;
            RemoveItemButton.Text = "Remove Item";
            RemoveItemButton.UseVisualStyleBackColor = true;
            RemoveItemButton.Click += RemoveItemButton_Click;
            // 
            // FinalizyePaymentButton
            // 
            FinalizyePaymentButton.Location = new Point(603, 415);
            FinalizyePaymentButton.Name = "FinalizyePaymentButton";
            FinalizyePaymentButton.Size = new Size(108, 23);
            FinalizyePaymentButton.TabIndex = 5;
            FinalizyePaymentButton.Text = "Finalize Payment";
            FinalizyePaymentButton.UseVisualStyleBackColor = true;
            FinalizyePaymentButton.Click += FinalizyePaymentButton_Click;
            // 
            // PaymentTypeBox
            // 
            PaymentTypeBox.FormattingEnabled = true;
            PaymentTypeBox.Location = new Point(654, 61);
            PaymentTypeBox.Name = "PaymentTypeBox";
            PaymentTypeBox.Size = new Size(121, 23);
            PaymentTypeBox.TabIndex = 6;
            // 
            // TipAmountField
            // 
            TipAmountField.Location = new Point(675, 118);
            TipAmountField.Name = "TipAmountField";
            TipAmountField.Size = new Size(100, 23);
            TipAmountField.TabIndex = 7;
            // 
            // FeedbackField
            // 
            FeedbackField.Location = new Point(521, 175);
            FeedbackField.Name = "FeedbackField";
            FeedbackField.Size = new Size(254, 38);
            FeedbackField.TabIndex = 8;
            FeedbackField.Text = "";
            // 
            // VatLowLable
            // 
            VatLowLable.AutoSize = true;
            VatLowLable.Location = new Point(521, 257);
            VatLowLable.Name = "VatLowLable";
            VatLowLable.Size = new Size(53, 15);
            VatLowLable.TabIndex = 9;
            VatLowLable.Text = "VAT (6%)";
            // 
            // VatHighLable
            // 
            VatHighLable.AutoSize = true;
            VatHighLable.Location = new Point(521, 289);
            VatHighLable.Name = "VatHighLable";
            VatHighLable.Size = new Size(59, 15);
            VatHighLable.TabIndex = 10;
            VatHighLable.Text = "VAT (21%)";
            // 
            // TotalLable
            // 
            TotalLable.AutoSize = true;
            TotalLable.Location = new Point(521, 387);
            TotalLable.Name = "TotalLable";
            TotalLable.Size = new Size(32, 15);
            TotalLable.TabIndex = 11;
            TotalLable.Text = "Total";
            // 
            // FeedbackLable
            // 
            FeedbackLable.AutoSize = true;
            FeedbackLable.Location = new Point(521, 148);
            FeedbackLable.Name = "FeedbackLable";
            FeedbackLable.Size = new Size(57, 15);
            FeedbackLable.TabIndex = 12;
            FeedbackLable.Text = "Feedback";
            // 
            // TipAmountLable
            // 
            TipAmountLable.AutoSize = true;
            TipAmountLable.Location = new Point(521, 126);
            TipAmountLable.Name = "TipAmountLable";
            TipAmountLable.Size = new Size(68, 15);
            TipAmountLable.TabIndex = 13;
            TipAmountLable.Text = "Tip amount";
            // 
            // PaymentTypeLable
            // 
            PaymentTypeLable.AutoSize = true;
            PaymentTypeLable.Location = new Point(521, 69);
            PaymentTypeLable.Name = "PaymentTypeLable";
            PaymentTypeLable.Size = new Size(99, 15);
            PaymentTypeLable.TabIndex = 14;
            PaymentTypeLable.Text = "Payment method";
            // 
            // TableNumberLable
            // 
            TableNumberLable.AutoSize = true;
            TableNumberLable.Location = new Point(33, 69);
            TableNumberLable.Name = "TableNumberLable";
            TableNumberLable.Size = new Size(81, 15);
            TableNumberLable.TabIndex = 15;
            TableNumberLable.Text = "Table Number";
            // 
            // PaymentLable
            // 
            PaymentLable.AutoSize = true;
            PaymentLable.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            PaymentLable.Location = new Point(295, 9);
            PaymentLable.Name = "PaymentLable";
            PaymentLable.Size = new Size(174, 50);
            PaymentLable.TabIndex = 16;
            PaymentLable.Text = "Payment";
            // 
            // VatLowValue
            // 
            VatLowValue.AutoSize = true;
            VatLowValue.Location = new Point(747, 257);
            VatLowValue.Name = "VatLowValue";
            VatLowValue.Size = new Size(28, 15);
            VatLowValue.TabIndex = 17;
            VatLowValue.Text = "0.00";
            // 
            // VatHighValue
            // 
            VatHighValue.AutoSize = true;
            VatHighValue.Location = new Point(747, 289);
            VatHighValue.Name = "VatHighValue";
            VatHighValue.Size = new Size(28, 15);
            VatHighValue.TabIndex = 18;
            VatHighValue.Text = "0.00";
            // 
            // TotalValue
            // 
            TotalValue.AutoSize = true;
            TotalValue.Location = new Point(747, 387);
            TotalValue.Name = "TotalValue";
            TotalValue.Size = new Size(28, 15);
            TotalValue.TabIndex = 19;
            TotalValue.Text = "0.00";
            // 
            // PaymentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TotalValue);
            Controls.Add(VatHighValue);
            Controls.Add(VatLowValue);
            Controls.Add(PaymentLable);
            Controls.Add(TableNumberLable);
            Controls.Add(PaymentTypeLable);
            Controls.Add(TipAmountLable);
            Controls.Add(FeedbackLable);
            Controls.Add(TotalLable);
            Controls.Add(VatHighLable);
            Controls.Add(VatLowLable);
            Controls.Add(FeedbackField);
            Controls.Add(TipAmountField);
            Controls.Add(PaymentTypeBox);
            Controls.Add(FinalizyePaymentButton);
            Controls.Add(RemoveItemButton);
            Controls.Add(AddItemButton);
            Controls.Add(CurentItemsPayableList);
            Controls.Add(AllBillItemsList);
            Controls.Add(TableNumberBox);
            Name = "PaymentForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox TableNumberBox;
        private ListView AllBillItemsList;
        private ListView CurentItemsPayableList;
        private Button AddItemButton;
        private Button RemoveItemButton;
        private Button FinalizyePaymentButton;
        private ComboBox PaymentTypeBox;
        private TextBox TipAmountField;
        private RichTextBox FeedbackField;
        private Label VatLowLable;
        private Label VatHighLable;
        private Label TotalLable;
        private Label FeedbackLable;
        private Label TipAmountLable;
        private Label PaymentTypeLable;
        private Label TableNumberLable;
        private Label PaymentLable;
        private Label VatLowValue;
        private Label VatHighValue;
        private Label TotalValue;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
    }
}