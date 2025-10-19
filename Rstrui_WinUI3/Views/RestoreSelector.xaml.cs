using System;
using System.Collections.ObjectModel;
using System.Management;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System.Security.Principal;
using System.Linq;
using Microsoft.Windows.ApplicationModel.Resources;

namespace Rstrui_WinUI3.Views
{
	public class RestorePointInfo
	{
		public string Name { get; set; }
		public DateTime DateTime { get; set; }
		public string DateTimeFormatted { get; set; }
		public string Type { get; set; }
		public uint SequenceNumber { get; set; }
		public string Description { get; set; }
		public uint RestorePointType { get; set; }

		public RestorePointInfo()
		{
			Name = string.Empty;
			DateTime = System.DateTime.MinValue;
			DateTimeFormatted = string.Empty;
			Type = string.Empty;
			Description = string.Empty;
		}
	}

	public sealed partial class RestoreSelector : Page
	{
		public LocalizedStrings LocalizedStrings { get; } = new();
		public string CurrentTimeZone { get; set; }
		private ObservableCollection<RestorePointInfo> RestorePoints { get; set; }

		public RestoreSelector()
		{
			InitializeComponent();
			CurrentTimeZone = GetCurrentTimeZone();
			RestorePoints = new ObservableCollection<RestorePointInfo>();
			this.DataContext = this;

			Loaded += RestoreSelector_Loaded;
		}

		private void RestoreSelector_Loaded(object sender, RoutedEventArgs e)
		{
			// Load restore points (app already requires admin via manifest)
			LoadRestorePoints();

			// Setup selection changed event
			RestorePointsListView.SelectionChanged += RestorePointsListView_SelectionChanged;
		}

		private bool IsAdministrator()
		{
			try
			{
				var identity = WindowsIdentity.GetCurrent();
				var principal = new WindowsPrincipal(identity);
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			catch
			{
				return false;
			}
		}

		private void LoadRestorePoints()
		{
			try
			{
				RestorePoints.Clear();

				var scope = new ManagementScope("\\\\localhost\\root\\default");
				scope.Connect();

				var query = new ObjectQuery("SELECT * FROM SystemRestore");
				var searcher = new ManagementObjectSearcher(scope, query);
				var results = searcher.Get();

				foreach (ManagementObject result in results)
				{
					try
					{
						var restorePoint = new RestorePointInfo();

						// Get sequence number
						if (result["SequenceNumber"] != null)
							restorePoint.SequenceNumber = Convert.ToUInt32(result["SequenceNumber"]);

						// Get description/name
						if (result["Description"] != null)
						{
							restorePoint.Name = result["Description"].ToString() ?? string.Empty;
							restorePoint.Description = result["Description"].ToString() ?? string.Empty;
						}

						// Get creation time
						if (result["CreationTime"] != null)
						{
							string? creationTime = result["CreationTime"].ToString();
							if (!string.IsNullOrEmpty(creationTime))
							{
								restorePoint.DateTime = ManagementDateTimeConverter.ToDateTime(creationTime);
								restorePoint.DateTimeFormatted = restorePoint.DateTime.ToString("M/d/yyyy hh:mm:ss tt");
							}
						}

						// Get restore point type
						if (result["RestorePointType"] != null)
						{
							restorePoint.RestorePointType = Convert.ToUInt32(result["RestorePointType"]);
							restorePoint.Type = GetRestorePointTypeName(restorePoint.RestorePointType);
						}

						RestorePoints.Add(restorePoint);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine($"Error processing restore point: {ex.Message}");
					}
				}

				// Sort by date descending (newest first)
				var sortedPoints = RestorePoints.OrderByDescending(rp => rp.DateTime).ToList();
				RestorePoints.Clear();
				foreach (var point in sortedPoints)
				{
					RestorePoints.Add(point);
				}

				RestorePointsListView.ItemsSource = RestorePoints;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error loading restore points: {ex.Message}");
				ShowErrorDialog($"Failed to load restore points: {ex.Message}");
			}
		}

		private string GetRestorePointTypeName(uint typeCode)
		{
			return typeCode switch
			{
				0 => LocalizedStrings.RestorePointType_ApplicationInstall,
				1 => LocalizedStrings.RestorePointType_ApplicationUninstall,
				10 => LocalizedStrings.RestorePointType_DeviceDriverInstall,
				12 => LocalizedStrings.RestorePointType_ModifySettings,
				13 => LocalizedStrings.RestorePointType_CancelledOperation,
				_ => LocalizedStrings.RestorePointType_Manual
			};
		}

		private void RestorePointsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			NextButton.IsEnabled = RestorePointsListView.SelectedItem != null;
		}

		private async void ScanAffectedApps_Click(object sender, RoutedEventArgs e)
		{
			// 因為微軟沒開放 API 讓我們去掃描還原點會影響哪些應用程式 or 驅動...
			// 所以這邊就只能讓使用者自己去檢查囉
			var dialog = new ContentDialog
			{
				Title = LocalizedStrings.AffectedAppsTitle,
				Content = LocalizedStrings.AffectedAppsContent,
				CloseButtonText = LocalizedStrings.OK,
				XamlRoot = this.XamlRoot
			};
			await dialog.ShowAsync();
		}

		private async void ShowErrorDialog(string message)
		{
			var dialog = new ContentDialog
			{
				Title = LocalizedStrings.RestoreSelectorErrorTitle,
				Content = message,
				CloseButtonText = LocalizedStrings.OK,
				XamlRoot = this.XamlRoot
			};
			await dialog.ShowAsync();
		}

		private string GetCurrentTimeZone()
		{
			try
			{
				var tz = TimeZoneInfo.Local;
				return $"{tz.Id}";
			}
			catch
			{
				return LocalizedStrings.RestoreSelectorTimeZoneUnknown;
			}
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			if (Frame != null && Frame.CanGoBack)
			{
				Frame.GoBack(new SlideNavigationTransitionInfo());
			}
		}

		private async void Next_Click(object sender, RoutedEventArgs e)
		{
			if (RestorePointsListView.SelectedItem is RestorePointInfo selectedPoint)
			{
				Frame.Navigate(typeof(RestoreResult), selectedPoint, new SlideNavigationTransitionInfo());
			}
		}
	}
}