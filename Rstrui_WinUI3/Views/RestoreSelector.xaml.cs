using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Rstrui_WinUI3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RestoreSelector : Page
    {
        public LocalizedStrings LocalizedStrings { get; } = new();
        public string CurrentTimeZone { get; set; }

        public RestoreSelector()
        {
            InitializeComponent();
            CurrentTimeZone = GetCurrentTimeZone();
            this.DataContext = this;
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
                return "Unknown";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame != null && Frame.CanGoBack)
            {
                Frame.GoBack(new SlideNavigationTransitionInfo());
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
			// Frame.Navigate(typeof(RestoreResult), null, new SlideNavigationTransitionInfo());
		}
    }
}
