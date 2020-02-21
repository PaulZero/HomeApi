using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi.Web.Services.GoogleCast
{
    public class GoogleCastService
    {
        public async Task CastBunny()
        {
            await Task.Delay(TimeSpan.Zero);
            //// Use the DeviceLocator to find a Chromecast
            //var receiver = (await new DeviceLocator().FindReceiversAsync()).First();

            //var sender = new Sender();
            //// Connect to the Chromecast
            //await sender.ConnectAsync(receiver);
            //// Launch the default media receiver application
            //var mediaChannel = sender.GetChannel<IMediaChannel>();
            //await sender.LaunchAsync(mediaChannel);
            //// Load and play Big Buck Bunny video
            //var mediaStatus = await mediaChannel.LoadAsync(
            //    new MediaInformation() { ContentId = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4" });
        }

        public Task<string[]> GetReceiverNames()
        {
            return Task.Run(() => new string[0]);
            //return await new DeviceLocator().FindReceiversAsync();
        }
    }
}
