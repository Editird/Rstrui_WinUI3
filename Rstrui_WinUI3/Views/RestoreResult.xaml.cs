using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Rstrui_WinUI3.Views
{
    /// <summary>
    /// 系統還原結果頁面
    /// </summary>
    public sealed partial class RestoreResult : Page
    {
        public LocalizedStrings LocalizedStrings { get; } = new();
        public RestorePointInfo? RestorePoint { get; set; }

        public RestoreResult()
        {
            InitializeComponent();
            this.DataContext = this;
            RestoreResultAttention.IsOpen = true;
		}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is RestorePointInfo restorePoint)
            {
                RestorePoint = restorePoint;
			}
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame != null && Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }


        private async void Restore_Click(object sender, RoutedEventArgs e)
        {
            if (RestorePoint == null)
            {
                await ShowErrorDialog(LocalizedStrings.RestoreResultErrorTitle, LocalizedStrings.RestoreResultErrorNoPoint);
                return;
            }

            ContentDialog confirmDialog = new ContentDialog
            {
                Title = LocalizedStrings.RestoreResultConfirmTitle,
                Content = string.Format(LocalizedStrings.RestoreResultConfirmContent),
                PrimaryButtonText = LocalizedStrings.RestoreResultConfirmBtn,
                CloseButtonText = LocalizedStrings.RestoreResultCancelBtn,
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            // 顯示進度環並禁用按鈕
            RestoreProgressRing.Visibility = Visibility.Visible;
            NextButton.IsEnabled = false;
			BackButton.IsEnabled = false;
            App.IsRestoreInProgress = true;

            // 執行還原
            try
            {
                var restoreResult = await SystemRestoreHelper.RestoreSystemAsync(RestorePoint.SequenceNumber);

                if (restoreResult.Success)
                {
                    ContentDialog successDialog = new ContentDialog
                    {
                        Title = LocalizedStrings.RestoreResultStartedTitle,
                        Content = LocalizedStrings.RestoreResultStartedContent,
                        PrimaryButtonText = LocalizedStrings.RestoreResultRestartNow,
                        CloseButtonText = LocalizedStrings.RestoreResultRestartLater,
                        DefaultButton = ContentDialogButton.Primary,
                        XamlRoot = this.XamlRoot
                    };

                    var dialogResult = await successDialog.ShowAsync();
                    
                    if (dialogResult == ContentDialogResult.Primary)
                    {
                        SystemRestoreHelper.RebootSystem();
                    }
                    else
                    {
                        Application.Current.Exit();
                    }
                }
                else
                {
                    await ShowErrorDialog(LocalizedStrings.RestoreResultFailedTitle, restoreResult.ErrorMessage);
                }
            }
            catch (UnauthorizedAccessException)
            {
                await ShowErrorDialog(LocalizedStrings.RestoreResultPermissionTitle, LocalizedStrings.RestoreResultPermissionContent);
            }
            catch (Exception ex)
            {
                await ShowErrorDialog(LocalizedStrings.RestoreResultUnknownErrorTitle, string.Format(LocalizedStrings.RestoreResultUnknownErrorContent, ex.Message));
                Application.Current.Exit();
            }
            finally
            {
                // 隱藏進度環並啟用按鈕
                RestoreProgressRing.Visibility = Visibility.Collapsed;
                NextButton.IsEnabled = true;
                BackButton.IsEnabled = true;
                App.IsRestoreInProgress = false;
            }
        }

        private async Task ShowErrorDialog(string title, string message)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = LocalizedStrings.OK,
                XamlRoot = this.XamlRoot
            };
            await errorDialog.ShowAsync();
        }
    }

    /// <summary>
    /// 系統還原輔助類別 - 依照微軟官方 System Restore API 實作
    /// </summary>
    public static class SystemRestoreHelper
    {
        // 事件類型常數（根據 Microsoft 官方文檔定義）
        public const int BEGIN_SYSTEM_CHANGE = 100;
        public const int END_SYSTEM_CHANGE = 101;

        // 還原點類型常數
        public const int APPLICATION_INSTALL = 0;
        public const int APPLICATION_UNINSTALL = 1;
        public const int DEVICE_DRIVER_INSTALL = 10;
        public const int MODIFY_SETTINGS = 12;
        public const int CANCELLED_OPERATION = 13;

        /// <summary>
        /// RESTOREPOINTINFO 結構（根據 Microsoft 官方文檔定義）
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RESTOREPOINTINFO
        {
            public int dwEventType;           // BEGIN_SYSTEM_CHANGE 或 END_SYSTEM_CHANGE
            public int dwRestorePtType;       // 還原點類型
            public long llSequenceNumber;     // 序號（建立時設為 0）
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDescription;      // 描述文字
        }

        /// <summary>
        /// STATEMGRSTATUS 結構（根據 Microsoft 官方文檔定義）
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STATEMGRSTATUS
        {
            public int nStatus;               // 狀態碼
            public long llSequenceNumber;     // 還原點序號
        }

        /// <summary>
        /// SRSetRestorePoint 函式（根據 Microsoft 官方文檔定義）
        /// </summary>
        [DllImport("srclient.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SRSetRestorePointW(
            ref RESTOREPOINTINFO pRestorePtSpec,
            out STATEMGRSTATUS pSMgrStatus);

        /// <summary>
        /// 系統還原結果
        /// </summary>
        public class RestoreResult
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; } = string.Empty;
        }

        /// <summary>
        /// 使用 WMI 執行系統還原（根據 Microsoft 官方文檔建議）
        /// </summary>
        public static async Task<RestoreResult> RestoreSystemAsync(long sequenceNumber)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // 根據 Microsoft 官方文檔，使用 WMI SystemRestore 類別執行還原
                    ManagementScope scope = new ManagementScope("\\\\localhost\\root\\default");
                    scope.Connect();

                    ManagementPath path = new ManagementPath("SystemRestore");
                    ManagementClass systemRestore = new ManagementClass(scope, path, null);

                    // 準備參數
                    ManagementBaseObject inParams = systemRestore.GetMethodParameters("Restore");
                    inParams["SequenceNumber"] = (uint)sequenceNumber;

                    // 執行還原方法
                    ManagementBaseObject outParams = systemRestore.InvokeMethod("Restore", inParams, null);

                    // 檢查返回值
                    uint returnValue = (uint)outParams["ReturnValue"];

                    if (returnValue == 0)
                    {
                        return new RestoreResult { Success = true };
                    }
                    else
                    {
                        string errorMessage = returnValue switch
                        {
                            1 => "System Restore service is disabled",
                            2 => "The specified restore point was not found",
                            3 => "System Restore initialization failed",
                            _ => $"System Restore failed, error code: {returnValue}"
                        };

                        return new RestoreResult 
                        { 
                            Success = false, 
                            ErrorMessage = errorMessage 
                        };
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    return new RestoreResult
                    {
                        Success = false,
                        ErrorMessage = "Administrator permission is required to run System Restore"
                    };
                }
                catch (ManagementException ex)
                {
                    return new RestoreResult
                    {
                        Success = false,
                        ErrorMessage = $"WMI Error：{ex.Message}"
                    };
                }
                catch (Exception ex)
                {
                    return new RestoreResult
                    {
                        Success = false,
                        ErrorMessage = $"Unexpected error occurred: {ex.Message}"
                    };
                }
            });
        }

        /// <summary>
        /// 建立系統還原點（根據 Microsoft 官方文檔實作）
        /// </summary>
        public static bool CreateRestorePoint(string description, int restorePointType = APPLICATION_INSTALL)
        {
            try
            {
                // 初始化 COM 安全性（根據官方文檔要求）
                // 注意：在實際應用中，可能需要在應用程式啟動時就進行 COM 初始化
                RESTOREPOINTINFO restorePointInfo = new RESTOREPOINTINFO
                {
                    dwEventType = BEGIN_SYSTEM_CHANGE,
                    dwRestorePtType = restorePointType,
                    llSequenceNumber = 0,  // 建立時必須設為 0
                    szDescription = description
                };

                bool result = SRSetRestorePointW(ref restorePointInfo, out STATEMGRSTATUS status);

                if (!result)
                {
                    // 檢查錯誤狀態
                    const int ERROR_SERVICE_DISABLED = 1058;
                    if (status.nStatus == ERROR_SERVICE_DISABLED)
                    {
                        throw new InvalidOperationException("System Restore Service is disabled");
                    }
                    return false;
                }

                // 結束系統變更
                restorePointInfo.dwEventType = END_SYSTEM_CHANGE;
                restorePointInfo.llSequenceNumber = status.llSequenceNumber;
                
                result = SRSetRestorePointW(ref restorePointInfo, out status);
                
                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 重新啟動系統（根據 Microsoft 官方文檔建議使用 WMI）
        /// </summary>
        public static void RebootSystem()
        {
            try
            {
                // 使用 WMI Win32_OperatingSystem 類別重新啟動
                ManagementScope scope = new ManagementScope("\\\\localhost\\root\\cimv2");
                scope.Connect();

                SelectQuery query = new SelectQuery("SELECT * FROM Win32_OperatingSystem WHERE Primary=true");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                foreach (ManagementObject os in searcher.Get())
                {
                    // 取得重新啟動方法的參數
                    ManagementBaseObject inParams = os.GetMethodParameters("Reboot");
                    os.InvokeMethod("Reboot", inParams, null);
                }
            }
            catch
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/r /t 0",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            }
        }

        /// <summary>
        /// 檢查系統還原服務是否可用
        /// </summary>
        public static bool IsSystemRestoreAvailable()
        {
            try
            {
                // 嘗試載入 srclient.dll
                IntPtr hModule = LoadLibrary("srclient.dll");
                if (hModule == IntPtr.Zero)
                {
                    return false;
                }
                FreeLibrary(hModule);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);
    }
}