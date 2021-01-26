using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pe.Installer
{
    [Serializable, DataContract]
    public class UpdateItemData
    {
        #region property

        public static Uri IgnoreUri { get; } = new Uri("https://example.com/");

        [JsonProperty("version")]
        public string _Version { get; set; } = string.Empty;

        [JsonProperty("minimum_version")]
        public string _MinimumVersion { get; set; } = string.Empty;

        #endregion

        #region IReadOnlyUpdateItemData

        [JsonProperty("release")]
        public DateTime Release { get; set; }

        [JsonIgnore]
        public Version Version
        {
            get => Version.Parse(_Version);
            set => _Version = value.ToString();
        }

        [JsonProperty("revision")]
        public string Revision { get; set; } = string.Empty;

        [JsonProperty("platform")]
        public string Platform { get; set; } = string.Empty;

        [JsonIgnore]
        public Version MinimumVersion
        {
            get => Version.Parse(_MinimumVersion);
            set => _MinimumVersion = value.ToString();
        }

        [JsonProperty("note_uri")]
        public Uri NoteUri { get; set; } = IgnoreUri;


        [JsonProperty("archive_uri")]
        public Uri ArchiveUri { get; set; } = IgnoreUri;

        [JsonProperty("archive_size")]
        public long ArchiveSize { get; set; }

        [JsonProperty("archive_kind")]
        public string ArchiveKind { get; set; } = string.Empty;

        [JsonProperty("archive_hash_kind")]
        public string ArchiveHashKind { get; set; } = string.Empty;

        [JsonProperty("archive_hash_value")]
        public string ArchiveHashValue { get; set; } = string.Empty;

        #endregion
    }

    [Serializable, DataContract]
    public class UpdateData
    {
        #region property

        [JsonProperty("items")]
        public UpdateItemData[] Items { get; set; } = new UpdateItemData[0];

        #endregion
    }
}
