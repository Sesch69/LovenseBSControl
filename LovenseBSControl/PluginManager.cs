using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SemVer;
using Version = SemVer.Version;

namespace LovenseBSControl
{
    class PluginManager
    {
        private static readonly HttpClient client = new HttpClient();

        private Task<Release> _loadingTask = null;

        private Version _localVersion;
        
        public Version LocalVersion => _localVersion ?? IPA.Loader.PluginManager.GetPluginFromId("LovenseBSControl").Version;
             

        public async Task<Release> GetNewestReleaseAsync()
        {
            _loadingTask = _loadingTask ??  GetNewestReleaseAsyncInternal();
            return await _loadingTask;
        }

        private async Task<Release> GetNewestReleaseAsyncInternal()
        {
            try
            {
                client.DefaultRequestHeaders.Add("User-Agent", "request");
                var response = await client.GetStringAsync("https://api.github.com/repos/Sesch69/LovenseBSControl/releases");
                List<Release> resp = JsonConvert.DeserializeObject<List<Release>>(response);
                if (resp.Count > 0)
                {
                    var release = resp[0]; 
                    release.LocalVersion = LocalVersion;
                    return release;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public class Release
        {
            [JsonProperty("tag_name")] public string TagName;
            [JsonProperty("body")] public string Body;
            [JsonProperty("html_url")] public string Url;

            public Version LocalVersion;

            private Version _releaseVersion = null;

            public Version RemoteVersion => _releaseVersion ?? new Version(TagName);

            private bool? _isLocalNewest;

            public bool IsLocalNewest
            {
                get
                {
                    _isLocalNewest = _isLocalNewest ?? new Range($"<={LocalVersion}").IsSatisfied(RemoteVersion);
                    return _isLocalNewest.Value;
                }
            }
        }
    }
}