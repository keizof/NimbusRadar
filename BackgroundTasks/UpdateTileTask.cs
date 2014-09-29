using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace BackgroundTasks
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updateTile();
            deferral.Complete();
        }

        private void updateTile()
        {
            var tileUpdater = Windows.UI.Notifications.TileUpdateManager
                  .CreateTileUpdaterForApplication();

            double lat = 35.681093831866455;
            double lon = 139.76716278230535;
            int zoom = 8;

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("TileLatitude") == true
                && localSettings.Values.ContainsKey("TileLongtitude") == true
                && localSettings.Values.ContainsKey("TileZoom") == true)
            {
                lat = (double)localSettings.Values["TileLatitude"];
                lon = (double)localSettings.Values["TileLongtitude"];
                zoom = (int)localSettings.Values["TileZoom"];
            }

            var tile310x310 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Image);
            tile310x310.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = YahooMapAPI.GenerateStaticMapURL(310, 310, lat, lon, zoom);
            var tile310x150 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Image);
            tile310x150.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = YahooMapAPI.GenerateStaticMapURL(310, 150, lat, lon, zoom);
            var tile150x150 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
            tile150x150.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = YahooMapAPI.GenerateStaticMapURL(150, 150, lat, lon, zoom);

            // Clear previously set tiles
            tileUpdater.Clear();

            tileUpdater.Update(new TileNotification(tile310x310));
            tileUpdater.Update(new TileNotification(tile310x150));
            tileUpdater.Update(new TileNotification(tile150x150));
        }
    }
}
