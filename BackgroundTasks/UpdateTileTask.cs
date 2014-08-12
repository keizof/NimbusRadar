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

            var tile310x310 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Image);
            tile310x310.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = YahooMapAPI.GenerateStaticMapURL(310, 310, 35.681093831866455, 139.76716278230535, 8);
            var tile310x150 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Image);
            tile310x150.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = YahooMapAPI.GenerateStaticMapURL(310, 150, 35.681093831866455, 139.76716278230535, 8);
            var tile150x150 = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
            tile150x150.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = YahooMapAPI.GenerateStaticMapURL(150, 150, 35.681093831866455, 139.76716278230535, 8);

            // Clear previously set tiles
            tileUpdater.Clear();

            tileUpdater.Update(new TileNotification(tile310x310));
            tileUpdater.Update(new TileNotification(tile310x150));
            tileUpdater.Update(new TileNotification(tile150x150));
        }
    }
}
