namespace fias
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ComboBox addressComboBox;
		private System.Windows.Forms.DateTimePicker plandate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button AcceptBtn;
		private System.Windows.Forms.Button CloseBtn;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox commentTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox deliveryType;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.addressComboBox = new System.Windows.Forms.ComboBox();
			this.plandate = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.AcceptBtn = new System.Windows.Forms.Button();
			this.CloseBtn = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.commentTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.deliveryType = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// addressComboBox
			// 
			this.addressComboBox.CausesValidation = false;
			this.addressComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.addressComboBox.ItemHeight = 13;
			this.addressComboBox.Location = new System.Drawing.Point(12, 72);
			this.addressComboBox.MaxDropDownItems = 10;
			this.addressComboBox.Name = "addressComboBox";
			this.addressComboBox.Size = new System.Drawing.Size(432, 21);
			this.addressComboBox.TabIndex = 1;
			this.addressComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComboBox1KeyUp);
			// 
			// plandate
			// 
			this.plandate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.plandate.CustomFormat = "dd.MM.yyyy";
			this.plandate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.plandate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.plandate.Location = new System.Drawing.Point(57, 18);
			this.plandate.Name = "plandate";
			this.plandate.Size = new System.Drawing.Size(102, 20);
			this.plandate.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 14);
			this.label1.TabIndex = 2;
			this.label1.Text = "Населенный пункт";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 14);
			this.label2.TabIndex = 3;
			this.label2.Text = "Дата";
			// 
			// AcceptBtn
			// 
			this.AcceptBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.AcceptBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.AcceptBtn.Location = new System.Drawing.Point(288, 184);
			this.AcceptBtn.Name = "AcceptBtn";
			this.AcceptBtn.Size = new System.Drawing.Size(75, 23);
			this.AcceptBtn.TabIndex = 4;
			this.AcceptBtn.Text = "Сохранить";
			this.AcceptBtn.UseVisualStyleBackColor = true;
			this.AcceptBtn.Click += new System.EventHandler(this.AcceptBtnClick);
			// 
			// CloseBtn
			// 
			this.CloseBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CloseBtn.Location = new System.Drawing.Point(369, 184);
			this.CloseBtn.Name = "CloseBtn";
			this.CloseBtn.Size = new System.Drawing.Size(75, 23);
			this.CloseBtn.TabIndex = 5;
			this.CloseBtn.Text = "Закрыть";
			this.CloseBtn.UseVisualStyleBackColor = true;
			this.CloseBtn.Click += new System.EventHandler(this.CloseBtnClick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 108);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 14);
			this.label3.TabIndex = 6;
			this.label3.Text = "Комментарий";
			// 
			// commentTextBox
			// 
			this.commentTextBox.Location = new System.Drawing.Point(12, 125);
			this.commentTextBox.Name = "commentTextBox";
			this.commentTextBox.Size = new System.Drawing.Size(432, 20);
			this.commentTextBox.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(219, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(26, 14);
			this.label4.TabIndex = 8;
			this.label4.Text = "Тип";
			// 
			// deliveryType
			// 
			this.deliveryType.BackColor = System.Drawing.Color.White;
			this.deliveryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.deliveryType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deliveryType.FormattingEnabled = true;
			this.deliveryType.Items.AddRange(new object[] {
			"Самовывоз",
			"Доставка"});
			this.deliveryType.Location = new System.Drawing.Point(251, 17);
			this.deliveryType.Name = "deliveryType";
			this.deliveryType.Size = new System.Drawing.Size(193, 21);
			this.deliveryType.TabIndex = 9;
			this.deliveryType.SelectedIndexChanged += new System.EventHandler(this.DeliveryTypeSelectedIndexChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(456, 215);
			this.ControlBox = false;
			this.Controls.Add(this.deliveryType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.commentTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.AcceptBtn);
			this.Controls.Add(this.CloseBtn);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.plandate);
			this.Controls.Add(this.addressComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Доставка";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
