using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public class Downloader
    {
        public Downloader(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        ILogger Logger { get; }
        HttpClient HttpClient { get; } = new HttpClient();

        #endregion

        #region function

        public async Task<Uri> GetArchiveUriAsync(Uri updateInfoUri, string platform, CancellationToken token)
        {
            var stream = await HttpClient.GetStreamAsync(updateInfoUri);


            throw new NotImplementedException();
        }

        #endregion
    }
}
