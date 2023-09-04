namespace NxIEHelperNS
{
	partial class LogForm
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
			this.tb_log = new System.Windows.Forms.TextBox();
			this.bt_clear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tb_log
			// 
			this.tb_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_log.BackColor = System.Drawing.Color.White;
			this.tb_log.Location = new System.Drawing.Point(13, 13);
			this.tb_log.Multiline = true;
			this.tb_log.Name = "tb_log";
			this.tb_log.ReadOnly = true;
			this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_log.Size = new System.Drawing.Size(447, 240);
			this.tb_log.TabIndex = 0;
			// 
			// bt_clear
			// 
			this.bt_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bt_clear.Location = new System.Drawing.Point(385, 259);
			this.bt_clear.Name = "bt_clear";
			this.bt_clear.Size = new System.Drawing.Size(75, 23);
			this.bt_clear.TabIndex = 1;
			this.bt_clear.Text = "Clear";
			this.bt_clear.UseVisualStyleBackColor = true;
			this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 291);
			this.Controls.Add(this.bt_clear);
			this.Controls.Add(this.tb_log);
			this.Name = "LogForm";
			this.Text = "Logger Form";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tb_log;
		private System.Windows.Forms.Button bt_clear;
	}
}