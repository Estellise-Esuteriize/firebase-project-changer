namespace Packages.FirebaseProjectManager.Project_Manager.Editor.Processing.Extensions
{
    internal static class ProcessChangerExtensions
    {
        internal static string GetGoogleServicesJsonFile(bool isRelease)
        {
            return isRelease ? "google-services_release.json" : "google-services_debug.json";
        }

        internal static string GetGoogleServicesPlistFile(bool isRelease)
        {
            return isRelease ? "GoogleService-Info_release.plist" : "GoogleService-Info_debug.plist";
        }
    }
}