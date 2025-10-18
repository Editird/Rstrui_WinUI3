// filepath: c:\Users\hung5\Desktop\Lab\vsproj\Rstrui_WinUI3\Rstrui_WinUI3\Views\MainPage.xaml.cs
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Win32;

namespace Rstrui_WinUI3.Views
{
    public sealed partial class MainPage : Page
    {
        public LocalizedStrings LocalizedStrings { get; } = new();

        public MainPage()
        {
            InitializeComponent();
            CheckSystemRestoreStatus();
        }

        /// <summary>
        /// 檢查系統還原是否被停用
        /// </summary>
        private void CheckSystemRestoreStatus()
        {
            try
            {
                // 檢查註冊表路徑
                using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore");
                
                if (key != null)
                {
                    var disableSR = key.GetValue("DisableSR");
                    
                    // 如果 DisableSR 值為 1，表示系統還原已被停用
                    if (disableSR != null && disableSR is int value && value == 1)
                    {
                        // 顯示 InfoBar
                        SystemRestoreDisabledInfoBar.IsOpen = true;
                        
                        // 停用 Next 按鈕
                        NextButton.IsEnabled = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                // 如果無法讀取註冊表（例如權限不足），記錄錯誤但不影響程式運作
                Debug.WriteLine($"無法檢查系統還原狀態: {ex.Message}");
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var uri = "https://support.microsoft.com/en-us/windows/system-restore-a5ae3ed9-07c4-fd56-45ee-096777ecd14e";
            var psi = new ProcessStartInfo
            {
                FileName = uri,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        /// <summary>
        /// 開啟系統保護設定
        /// </summary>
        private void SystemProtection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "SystemPropertiesProtection.exe",
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"無法啟動系統保護程式: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            // 使用 Frame 導航到 RestoreSelector 頁面，並指定動畫
            Frame.Navigate(typeof(RestoreSelector), null, new SlideNavigationTransitionInfo());
        }
    }
}