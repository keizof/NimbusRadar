using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundTasks
{
    public sealed class YahooMapAPI
    {
        public static string GetApplicationID()
        {
            return <Replace with AppID>;
        }

        public static string GenerateStaticMapURL(int width, int height, double lat, double lon, int zoom)
        {
            return string.Format("http://map.olp.yahooapis.jp/OpenLocalPlatform/V1/static?appid={0}&lat={1}&lon={2}&z={3}&width={4}&height={5}&overlay=type:rainfall|datelabel:on",
                GetApplicationID(),
                lat,
                lon,
                zoom,
                width,
                height);
        }
    }
}
