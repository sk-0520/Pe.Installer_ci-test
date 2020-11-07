using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public class Downloader
    {
        public Downloader()
        {

        }

        #region property

                ILogger Logger { get; }
        HttpClient HttpClient { get; } = new HttpClient();

        #endregion

        #region function

        public async Task<Uri> GetArchiveUriAsync(Uri updateInfoUri)
        {
            var stream = await HttpClient.GetStreamAsync(updateInfoUri);
        }

        #endregion
    }
}
