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
		public string RestoreSelectorTimeZone => resourceManager.MainResourceMap.GetValue("Resources/RestoreSelectorTimeZone").ValueAsString;
		public string AffectedAppsBtn => resourceManager.MainResourceMap.GetValue("Resources/AffectedAppsBtn").ValueAsString;
		public string AffectedAppsTitle => resourceManager.MainResourceMap.GetValue("Resources/AffectedAppsTitle").ValueAsString;
		public string AffectedAppsContent => resourceManager.MainResourceMap.GetValue("Resources/AffectedAppsContent").ValueAsString;
		public string RestoreSelectorErrorTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreSelectorErrorTitle").ValueAsString;
		public string RestoreSelectorTimeZoneUnknown => resourceManager.MainResourceMap.GetValue("Resources/RestoreSelectorTimeZoneUnknown").ValueAsString;
		public string RestorePointType_ApplicationInstall => resourceManager.MainResourceMap.GetValue("Resources/RestorePointType_ApplicationInstall").ValueAsString;
		public string RestorePointType_ApplicationUninstall => resourceManager.MainResourceMap.GetValue("Resources/RestorePointType_ApplicationUninstall").ValueAsString;
		public string RestorePointType_DeviceDriverInstall => resourceManager.MainResourceMap.GetValue("Resources/RestorePointType_DeviceDriverInstall").ValueAsString;
		public string RestorePointType_ModifySettings => resourceManager.MainResourceMap.GetValue("Resources/RestorePointType_ModifySettings").ValueAsString;
		public string RestorePointType_CancelledOperation => resourceManager.MainResourceMap.GetValue("Resources/RestorePointType_CancelledOperation").ValueAsString;
		public string RestorePointType_Manual => resourceManager.MainResourceMap.GetValue("Resources/RestorePointType_Manual").ValueAsString;

		// RestoreResult
		public string RestoreResultTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultTitle").ValueAsString;
		public string RestoreResultSubtitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultSubtitle").ValueAsString;
		public string RestoreResultAttentionTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultAttentionTitle").ValueAsString;
		public string RestoreResultAttention => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultAttention").ValueAsString;
		public string RestoreResultName => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultName").ValueAsString;
		public string RestoreResultDate => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultDate").ValueAsString;
		public string RestoreResultType => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultType").ValueAsString;
		public string RestoreResultRestore => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultRestore").ValueAsString;
		public string RestoreResultErrorTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultErrorTitle").ValueAsString;
		public string RestoreResultErrorNoPoint => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultErrorNoPoint").ValueAsString;
		public string RestoreResultConfirmTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultConfirmTitle").ValueAsString;
		public string RestoreResultConfirmContent => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultConfirmContent").ValueAsString;
		public string RestoreResultConfirmBtn => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultConfirmBtn").ValueAsString;
		public string RestoreResultCancelBtn => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultCancelBtn").ValueAsString;
		public string RestoreResultStartedTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultStartedTitle").ValueAsString;
		public string RestoreResultStartedContent => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultStartedContent").ValueAsString;
		public string RestoreResultRestartNow => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultRestartNow").ValueAsString;
		public string RestoreResultRestartLater => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultRestartLater").ValueAsString;
		public string RestoreResultFailedTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultFailedTitle").ValueAsString;
		public string RestoreResultPermissionTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultPermissionTitle").ValueAsString;
		public string RestoreResultPermissionContent => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultPermissionContent").ValueAsString;
		public string RestoreResultUnknownErrorTitle => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultUnknownErrorTitle").ValueAsString;
		public string RestoreResultUnknownErrorContent => resourceManager.MainResourceMap.GetValue("Resources/RestoreResultUnknownErrorContent").ValueAsString;

		public string Cancel => resourceManager.MainResourceMap.GetValue("Resources/Cancel").ValueAsString;
		public string Back => resourceManager.MainResourceMap.GetValue("Resources/Back").ValueAsString;
		public string Next => resourceManager.MainResourceMap.GetValue("Resources/Next").ValueAsString;
		public string OK => resourceManager.MainResourceMap.GetValue("Resources/OK").ValueAsString;
	}
}