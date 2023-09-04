using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NxIEHelperNS
{
	public partial class LogForm : Form
	{
		public LogForm()
		{
			InitializeComponent();
		}

		private void bt_clear_Click(object sender, EventArgs e)
		{
			this.tb_log.Clear();
		}

		public void LogText(string textFormat, params object[] parameters)
		{
			if (!this.IsDisposed)
				this.tb_log.AppendText(string.Format(textFormat + "\r\n", parameters));
		}

		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Visible = false;
		}

	}
}