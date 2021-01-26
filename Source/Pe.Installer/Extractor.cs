using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public class Extractor
    {
        public Extractor(IProgressLogger progressLogger, ILoggerFactory loggerFactory)
        {
            ProgressLogger = progressLogger;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        IProgressLogger ProgressLogger { get; }
        ILogger Logger { get; }

        #endregion

        #region function

        public Task ExtractAsync(Stream stream, DirectoryInfo directory, string archive)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
