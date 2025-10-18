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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Rstrui_WinUI3
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		public LocalizedStrings LocalizedStrings { get; } = new();

		public MainWindow()
		{
			InitializeComponent();
			ExtendsContentIntoTitleBar = true;
			SetTitleBar(AppTitleBar);
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			// Close Window
			Close();
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			// 繼續邏輯
			ContentDialog dialog = new()
			{
				Title = LocalizedStrings.Next,
				Content = "Continuing...",
				CloseButtonText = "OK",
				XamlRoot = this.Content.XamlRoot
			};
			_ = dialog.ShowAsync();
		}
	}
}