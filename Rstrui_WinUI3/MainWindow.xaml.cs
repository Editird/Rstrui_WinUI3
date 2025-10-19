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
			Closed += MainWindow_Closed;
		}

		private void MainWindow_Closed(object sender, WindowEventArgs args)
		{
			if (App.IsRestoreInProgress)
			{
				// Prevent closing the window during restore
				args.Handled = true;
			}
		}
	}
}