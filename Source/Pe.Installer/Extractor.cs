using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZipExtractor;

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

        private void Extract7z(Stream stream, DirectoryInfo extractDirectory)
        {
            using(var archive = new ArchiveFile(stream)) {
                var totalExtractItemCount = archive.Entries.Count;
                var extractedItemCount = 0;
                archive.Extract(e => {
                    var expandPath = Path.Combine(extractDirectory.FullName, e.FileName);

                    Logger.LogTrace("展開: {0}", expandPath);
                    extractedItemCount += 1;
                    ProgressLogger.Set((int)(extractedItemCount / (double)totalExtractItemCount));

                    if(extractedItemCount == totalExtractItemCount) {
                        ProgressLogger.Wait();
                    }

                    return expandPath;
                });
            }

        }

        public Task ExtractAsync(Stream stream, DirectoryInfo directory, string archive)
        {
            ProgressLogger.Reset(1);
            return Task.Run(() => {
                switch(archive) {
                    //case "zip":
                    //    ExtractZip(stream, directory);
                    //    break;

                    case "7z":
                        Extract7z(stream, directory);
                        break;

                    default:
                        throw new NotSupportedException($"kind: {archive}");
                }
            });
        }

        #endregion
    }
}
