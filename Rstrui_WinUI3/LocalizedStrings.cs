using Microsoft.Windows.ApplicationModel.Resources;

namespace Rstrui_WinUI3
{
	public class LocalizedStrings
	{
		private static readonly ResourceManager resourceManager = new();
		public string MainTitle => resourceManager.MainResourceMap.GetValue("Resources/MainTitle").ValueAsString;
		public string SubTitle => resourceManager.MainResourceMap.GetValue("Resources/SubTitle").ValueAsString;
		public string Cancel => resourceManager.MainResourceMap.GetValue("Resources/Cancel").ValueAsString;
		public string Back => resourceManager.MainResourceMap.GetValue("Resources/Back").ValueAsString;
		public string Next => resourceManager.MainResourceMap.GetValue("Resources/Next").ValueAsString;
	}
}
