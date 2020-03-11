#if (!DEBUG)
using System.Reflection;
#endif
using System.Threading.Tasks;
using Octokit;

namespace PostScriptumMortarCalculator.Services
{
    public class UpdateService
    {
        private const string c_OWNER_NAME = "sirdoombox";
        private const string c_REPO_NAME = "PostScriptumMortarCalculator";
        private const string c_RELEASE_PATH = "https://github.com/sirdoombox/PostScriptumMortarCalculator/releases/latest";
        
        private readonly GitHubClient m_client;
        
        public UpdateService()
        {
            m_client = new GitHubClient(new ProductHeaderValue("PostScriptumMortarCalculator"));
        }
        
        public async Task<UpdateInfo> CheckForUpdate()
        {
#if (!DEBUG)
            var release = await m_client.Repository.Release.GetLatest(c_OWNER_NAME, c_REPO_NAME);
            var releaseVersion = decimal.Parse(release.TagName);
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            var currentVersionString = $"{ver.Major}.{ver.Minor}";
            var currentVersion = decimal.Parse(currentVersionString);
            return new UpdateInfo(currentVersion, releaseVersion, c_RELEASE_PATH);
#endif
            return new UpdateInfo(1.0m,1.1m,c_RELEASE_PATH);
        }

        public struct UpdateInfo
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