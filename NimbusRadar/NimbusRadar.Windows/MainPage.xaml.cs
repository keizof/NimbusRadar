using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NimbusRadar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double _receivedLatitude;
        private double _receivedLongitude;
        private int _receivedZoom;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LiveTiles.AddLiveTiles();

            YahooJSMapWebView.Source = new Uri("ms-appx-web:///Assets/YahooJSMap.html");
        }

        private async void ScriptNotify(object sender, NotifyEventArgs e)
        {
            if (e.Value.StartsWith("latitude"))
            {
                char[] separators = { ':' };
                this._receivedLatitude = Double.Parse(e.Value.Split(separators)[1]);
            }

            if (e.Value.StartsWith("longtitude"))
            {
                char[] separators = { ':' };
                this._receivedLongitude = Double.Parse(e.Value.Split(separators)[1]);
            }

            if (e.Value.StartsWith("zoom"))
            {
                char[] separators = { ':' };
                this._receivedZoom = Int32.Parse(e.Value.Split(separators)[1]);
            }

            if (e.Value == "updateTilePositionButton")
            {
                MessageDialog dialog = new MessageDialog("地図の中心部分をタイルに表示します");
                dialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(DialogCommandhandler)));
                dialog.Commands.Add(new UICommand("キャンセル", new UICommandInvokedHandler(DialogCommandhandler)));

                dialog.DefaultCommandIndex = 1;
                dialog.CancelCommandIndex = 2;

                await dialog.ShowAsync();
            }
        }

        private void DialogCommandhandler(IUICommand command)
        {
            if (command.Label == "OK")
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["TileLatitude"] = this._receivedLatitude;
                localSettings.Values["TileLongtitude"] = this._receivedLongitude;
                localSettings.Values["TileZoom"] = this._receivedZoom;
            }
        }
    }
}
