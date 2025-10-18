using Microsoft.Windows.ApplicationModel.Resources;

namespace Rstrui_WinUI3
{
	public class LocalizedStrings
	{
		private static readonly ResourceManager resourceManager = new();
		// MainPage
		public string MainTitle => resourceManager.MainResourceMap.GetValue("Resources/MainTitle").ValueAsString;
		public string SubTitle => resourceManager.MainResourceMap.GetValue("Resources/SubTitle").ValueAsString;
		public string LearnMore => resourceManager.MainResourceMap.GetValue("Resources/LearnMore").ValueAsString;
		// Error Info Messages
		public string DisabledByOrgTitle => resourceManager.MainResourceMap.GetValue("Resources/DisabledByOrgTitle").ValueAsString;
		public string DisabledByOrgSubTitle => resourceManager.MainResourceMap.GetValue("Resources/DisabledByOrgSubTitle").ValueAsString;
		public string SystemProtectBtn => resourceManager.MainResourceMap.GetValue("Resources/SystemProtectBtn").ValueAsString;

		// RestoreSelector
		public string RestoreSelectorTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreSelectorTitle").ValueAsString;
		public string RestoreSelectorTimeZome => resourceManager.MainResourceMap.GetValue("Resources/RestoreSelectorTimeZome").ValueAsString;
		public string AffectedAppsBtn => resourceManager.MainResourceMap.GetValue("Resources/AffectedAppsBtn").ValueAsString;
		public string AffectedAppsTitle => resourceManager.MainResourceMap.GetValue("Resources/AffectedAppsTitle").ValueAsString;
		public string AffectedAppsContent => resourceManager.MainResourceMap.GetValue("Resources/AffectedAppsContent").ValueAsString;

		public string Cancel => resourceManager.MainResourceMap.GetValue("Resources/Cancel").ValueAsString;
		public string Back => resourceManager.MainResourceMap.GetValue("Resources/Back").ValueAsString;
		public string Next => resourceManager.MainResourceMap.GetValue("Resources/Next").ValueAsString;
		public string OK => resourceManager.MainResourceMap.GetValue("Resources/OK").ValueAsString;
	}
}
