namespace CryptoFile.Client.Forms {
	partial class GenerateKeysForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.generateButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.publicKeyLabel = new System.Windows.Forms.Label();
			this.publicKeyTextBox = new System.Windows.Forms.TextBox();
			this.privateKeyLabel = new System.Windows.Forms.Label();
			this.privateKeyTextBox = new System.Windows.Forms.TextBox();
			this.lengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.lengthLabel = new System.Windows.Forms.Label();
			this.publicExponentLabel = new System.Windows.Forms.Label();
			this.publicExponentTextBox = new System.Windows.Forms.TextBox();
			this.publicExponentButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// generateButton
			// 
			this.generateButton.Location = new System.Drawing.Point(205, 203);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(100, 23);
			this.generateButton.TabIndex = 1;
			this.generateButton.Text = "Generate";
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(311, 203);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// publicKeyLabel
			// 
			this.publicKeyLabel.AutoSize = true;
			this.publicKeyLabel.Location = new System.Drawing.Point(12, 112);
			this.publicKeyLabel.Name = "publicKeyLabel";
			this.publicKeyLabel.Size = new System.Drawing.Size(59, 13);
			this.publicKeyLabel.TabIndex = 5;
			this.publicKeyLabel.Text = "Public key:";
			// 
			// publicKeyTextBox
			// 
			this.publicKeyTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.publicKeyTextBox.Location = new System.Drawing.Point(12, 128);
			this.publicKeyTextBox.Name = "publicKeyTextBox";
			this.publicKeyTextBox.ReadOnly = true;
			this.publicKeyTextBox.Size = new System.Drawing.Size(374, 20);
			this.publicKeyTextBox.TabIndex = 6;
			// 
			// privateKeyLabel
			// 
			this.privateKeyLabel.AutoSize = true;
			this.privateKeyLabel.Location = new System.Drawing.Point(12, 161);
			this.privateKeyLabel.Name = "privateKeyLabel";
			this.privateKeyLabel.Size = new System.Drawing.Size(63, 13);
			this.privateKeyLabel.TabIndex = 7;
			this.privateKeyLabel.Text = "Private key:";
			// 
			// privateKeyTextBox
			// 
			this.privateKeyTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.privateKeyTextBox.Location = new System.Drawing.Point(12, 177);
			this.privateKeyTextBox.Name = "privateKeyTextBox";
			this.privateKeyTextBox.ReadOnly = true;
			this.privateKeyTextBox.Size = new System.Drawing.Size(374, 20);
			this.privateKeyTextBox.TabIndex = 8;
			// 
			// lengthNumericUpDown
			// 
			this.lengthNumericUpDown.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.lengthNumericUpDown.Location = new System.Drawing.Point(12, 30);
			this.lengthNumericUpDown.Name = "lengthNumericUpDown";
			this.lengthNumericUpDown.Size = new System.Drawing.Size(120, 20);
			this.lengthNumericUpDown.TabIndex = 9;
			// 
			// lengthLabel
			// 
			this.lengthLabel.AutoSize = true;
			this.lengthLabel.Location = new System.Drawing.Point(12, 14);
			this.lengthLabel.Name = "lengthLabel";
			this.lengthLabel.Size = new System.Drawing.Size(43, 13);
			this.lengthLabel.TabIndex = 10;
			this.lengthLabel.Text = "Length:";
			// 
			// publicExponentLabel
			// 
			this.publicExponentLabel.AutoSize = true;
			this.publicExponentLabel.Location = new System.Drawing.Point(12, 63);
			this.publicExponentLabel.Name = "publicExponentLabel";
			this.publicExponentLabel.Size = new System.Drawing.Size(86, 13);
			this.publicExponentLabel.TabIndex = 11;
			this.publicExponentLabel.Text = "Public exponent:";
			// 
			// publicExponentTextBox
			// 
			this.publicExponentTextBox.Location = new System.Drawing.Point(12, 79);
			this.publicExponentTextBox.Name = "publicExponentTextBox";
			this.publicExponentTextBox.ReadOnly = true;
			this.publicExponentTextBox.Size = new System.Drawing.Size(120, 20);
			this.publicExponentTextBox.TabIndex = 12;
			// 
			// publicExponentButton
			// 
			this.publicExponentButton.Location = new System.Drawing.Point(138, 78);
			this.publicExponentButton.Name = "publicExponentButton";
			this.publicExponentButton.Size = new System.Drawing.Size(34, 23);
			this.publicExponentButton.TabIndex = 13;
			this.publicExponentButton.Text = "...";
			this.publicExponentButton.UseVisualStyleBackColor = true;
			this.publicExponentButton.Click += new System.EventHandler(this.publicExponentButton_Click);
			// 
			// GenerateKeysForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(401, 237);
			this.Controls.Add(this.publicExponentButton);
			this.Controls.Add(this.publicExponentTextBox);
			this.Controls.Add(this.publicExponentLabel);
			this.Controls.Add(this.lengthLabel);
			this.Controls.Add(this.lengthNumericUpDown);
			this.Controls.Add(this.privateKeyTextBox);
			this.Controls.Add(this.privateKeyLabel);
			this.Controls.Add(this.publicKeyTextBox);
			this.Controls.Add(this.publicKeyLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.generateButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "GenerateKeysForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Generate Keys";
			((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label publicKeyLabel;
		private System.Windows.Forms.TextBox publicKeyTextBox;
		private System.Windows.Forms.Label privateKeyLabel;
		private System.Windows.Forms.TextBox privateKeyTextBox;
		private System.Windows.Forms.NumericUpDown lengthNumericUpDown;
		private System.Windows.Forms.Label lengthLabel;
		private System.Windows.Forms.Label publicExponentLabel;
		private System.Windows.Forms.TextBox publicExponentTextBox;
		private System.Windows.Forms.Button publicExponentButton;
	}
}