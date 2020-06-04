#if (!DEBUG)
using System.Reflection;
using Octokit;
#endif
using System.Net;
using System.Threading.Tasks;

namespace PostScriptumMortarCalculator.Services
{
    public class UpdateService
    {
#if (!DEBUG)
        // Strip out all the version checks for debugging purposes
        // The versioning is handled by AppVeyor. 
        private const string c_OWNER_NAME = "sirdoombox";
        private const string c_REPO_NAME = "PostScriptumMortarCalculator";

        private readonly GitHubClient m_client;
#endif
        private const string c_RELEASE_PATH =
            "https://github.com/sirdoombox/PostScriptumMortarCalculator/releases/latest";


        public UpdateService()
        {
#if(!DEBUG)
            m_client = new GitHubClient(new ProductHeaderValue(c_REPO_NAME));
#endif
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
        }

        public async Task<UpdateInfo> CheckForUpdate()
        {
#if (!DEBUG)
            try
            {
                var release = await m_client.Repository.Release.GetLatest(c_OWNER_NAME, c_REPO_NAME);
                var releaseVersion = decimal.Parse(release.TagName);
                var ver = Assembly.GetExecutingAssembly().GetName().Version;
                var currentVersionString = $"{ver.Major}.{ver.Minor}";
                var currentVersion = decimal.Parse(currentVersionString);
                return new UpdateInfo(currentVersion, releaseVersion, c_RELEASE_PATH);
            }
            catch
            {
                return new UpdateInfo(1,0,string.Empty);
            }
#endif
            return new UpdateInfo(1.0m, 1.1m, c_RELEASE_PATH);
        }

        public readonly struct UpdateInfo
        {
            public decimal CurrentVersion { get; }
            public decimal NewVersion { get; }
            public string ReleasePath { get; }

            public bool IsUpdateAvailable => NewVersion > CurrentVersion;

            public UpdateInfo(decimal currentVersion, decimal newVersion, string releasePath)
            {
                CurrentVersion = currentVersion;
                NewVersion = newVersion;
                ReleasePath = releasePath;
            }
        }
    }
}