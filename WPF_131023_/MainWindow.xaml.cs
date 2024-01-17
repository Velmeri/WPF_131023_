using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_131023_
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		
		private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
		{
			var newCulture = Thread.CurrentThread.CurrentUICulture.Name == "en-US" ? "uk-UA" : "en-US";
			var cultureInfo = new CultureInfo(newCulture);
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;

			ResourceDictionary newDict = new ResourceDictionary();
			switch (newCulture) {
				case "uk-UA":
					newDict.Source = new Uri("Resources/Resources.uk.xaml", UriKind.Relative);
					break;
				default:
					newDict.Source = new Uri("Resources/Resources.en.xaml", UriKind.Relative);
					break;
			}

			ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
										  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Resources.")
										  select d).First();
			if (oldDict != null) {
				Application.Current.Resources.MergedDictionaries.Remove(oldDict);
			}

			Application.Current.Resources.MergedDictionaries.Add(newDict);

			foreach (Window window in Application.Current.Windows) {
				window.DataContext = null;
				window.DataContext = this;
			}
		}
	}
}