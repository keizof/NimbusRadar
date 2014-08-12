using System;
using System.Collections.Generic;
using System.Text;

namespace NimbusRadar
{
    class LiveTiles
    {
        public static void AddLiveTiles()
        {
            var tileUpdater = Windows.UI.Notifications.TileUpdateManager
                              .CreateTileUpdaterForApplication();
            tileUpdater.EnableNotificationQueue(true);
            // Clear previously set tiles
            tileUpdater.Clear();
            var notification = CreateTileNotification();
            tileUpdater.Update(notification);
        }

        private static Windows.UI.Notifications.TileNotification CreateTileNotification()
        {
            // Assemble tiles
            var tile150x150 = Create150x150ImageTile();
            var tile310x150 = Create310x150ImageTile();
            var tile310x310 = Create310x310ImageTile();
            tile310x150.Square150x150Content = tile150x150;
            tile310x310.Wide310x150Content = tile310x150;

            return tile310x310.CreateNotification();
        }

        // 150x150 tile
        private static NotificationsExtensions.TileContent.ITileSquare150x150Image
          Create150x150ImageTile()
        {
            var tile = NotificationsExtensions.TileContent
                        .TileContentFactory.CreateTileSquare150x150Image();

            tile.Branding = NotificationsExtensions.TileContent.TileBranding.None;
            tile.Image.Src = BackgroundTasks.YahooMapAPI.GenerateStaticMapURL(150, 150, 35.681093831866455, 139.76716278230535, 8);

            return tile;
        }

        // 310x150 tile
        private static NotificationsExtensions.TileContent.ITileWide310x150Image
          Create310x150ImageTile()
        {
            var tile = NotificationsExtensions.TileContent
                        .TileContentFactory.CreateTileWide310x150Image();

            tile.Branding = NotificationsExtensions.TileContent.TileBranding.None;
            tile.Image.Src = BackgroundTasks.YahooMapAPI.GenerateStaticMapURL(310, 150, 35.681093831866455, 139.76716278230535, 8);

            return tile;
        }

        // 310x310 tile
        private static NotificationsExtensions.TileContent.ITileSquare310x310Image
          Create310x310ImageTile()
        {
            var tile = NotificationsExtensions.TileContent
                        .TileContentFactory.CreateTileSquare310x310Image();

            tile.Branding = NotificationsExtensions.TileContent.TileBranding.None;
            tile.Image.Src = BackgroundTasks.YahooMapAPI.GenerateStaticMapURL(310, 310, 35.681093831866455, 139.76716278230535, 8);

            return tile;
        }
    }
}
