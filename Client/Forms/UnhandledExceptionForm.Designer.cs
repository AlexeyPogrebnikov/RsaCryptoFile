namespace CryptoFile.Client.Forms
{
	partial class UnhandledExceptionForm
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
			this.messageLabel = new System.Windows.Forms.Label();
			this.messageRichTextBox = new System.Windows.Forms.RichTextBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.sendButton = new System.Windows.Forms.Button();
			this.emailLabel = new System.Windows.Forms.Label();
			this.emailTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// messageLabel
			// 
			this.messageLabel.AutoSize = true;
			this.messageLabel.Location = new System.Drawing.Point(12, 44);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.Size = new System.Drawing.Size(53, 13);
			this.messageLabel.TabIndex = 0;
			this.messageLabel.Text = "Message:";
			// 
			// messageRichTextBox
			// 
			this.messageRichTextBox.Location = new System.Drawing.Point(12, 60);
			this.messageRichTextBox.Name = "messageRichTextBox";
			this.messageRichTextBox.ReadOnly = true;
			this.messageRichTextBox.Size = new System.Drawing.Size(446, 185);
			this.messageRichTextBox.TabIndex = 1;
			this.messageRichTextBox.Text = "";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(383, 251);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// sendButton
			// 
			this.sendButton.Location = new System.Drawing.Point(290, 251);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(87, 23);
			this.sendButton.TabIndex = 3;
			this.sendButton.Text = "Send";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// emailLabel
			// 
			this.emailLabel.AutoSize = true;
			this.emailLabel.Location = new System.Drawing.Point(12, 15);
			this.emailLabel.Name = "emailLabel";
			this.emailLabel.Size = new System.Drawing.Size(37, 13);
			this.emailLabel.TabIndex = 4;
			this.emailLabel.Text = "e-mail:";
			// 
			// emailTextBox
			// 
			this.emailTextBox.Location = new System.Drawing.Point(55, 12);
			this.emailTextBox.Name = "emailTextBox";
			this.emailTextBox.ReadOnly = true;
			this.emailTextBox.Size = new System.Drawing.Size(254, 20);
			this.emailTextBox.TabIndex = 5;
			// 
			// UnhandledExceptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(470, 284);
			this.Controls.Add(this.emailTextBox);
			this.Controls.Add(this.emailLabel);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.messageRichTextBox);
			this.Controls.Add(this.messageLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "UnhandledExceptionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Unhandled Exception";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label messageLabel;
		private System.Windows.Forms.RichTextBox messageRichTextBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Label emailLabel;
		private System.Windows.Forms.TextBox emailTextBox;
	}
}