using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using  SHDocVw;
using mshtml;


namespace NxIEHelperNS
{
	class Logger
	{
		private static LogForm logForm;

		static Logger()
		{
			logForm = (ConfigManager.DisableLogging) ? null: new LogForm();
		}

		public static void ShowForm()
		{
			try
			{
				if (logForm != null)
					logForm.Show();
			}
			catch { }
		}

		public static void LogText(string logText, params object[] parameters)
		{
			try
			{
				if (logForm != null)
					logForm.LogText(logText, parameters);
			}
			catch{}
		}

		
	}
}
