using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace NxIEHelperNS
{
	class ConfigManager
	{
		private static Configuration configuration=null;

		private static KeyValueConfigurationElement GetConfigElement( string elementName)
		{
			try
			{
				if (configuration == null)
				{
					ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
					fileMap.ExeConfigFilename = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";
					configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
				}
				return configuration.AppSettings.Settings[elementName];
			}
			catch
			{
				return null;
			}
		}

		public static bool DisableLogging
		{
			get
			{
				KeyValueConfigurationElement disableLog = GetConfigElement("disableLogging");
				return (disableLog == null || disableLog.Value.ToLower() == "true");
			}
		}

		public static bool DisableHtmlStatus
		{
			get
			{
				KeyValueConfigurationElement disableLog = GetConfigElement("disableHtmlStatus");
				return (disableLog == null || disableLog.Value.ToLower() == "true");
			}
		}





	}
}
