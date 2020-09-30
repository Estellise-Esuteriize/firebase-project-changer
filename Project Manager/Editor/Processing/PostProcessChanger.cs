using Packages.FirebaseProjectManager.Project_Manager.Editor.Core;
using Packages.FirebaseProjectManager.Project_Manager.Editor.Processing.Extensions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Packages.FirebaseProjectManager.Project_Manager.Editor.Processing
{
    public class PostProcessChanger : IPostprocessBuildWithReport
    {

        public int callbackOrder
        {
            get
            {
                return 1600;
            }
        }
        public void OnPostprocessBuild(BuildReport report)
        {
            const string json = "google-services.json";
            const string plist = "GoogleService-Info.plist";

            var debugJson = ProcessChangerExtensions.GetGoogleServicesJsonFile(false);
            var debugPlist =  ProcessChangerExtensions.GetGoogleServicesPlistFile(false);
            
            Debug.Log("Start Operation Firebase Debug");

            var androidServices = new GoogleServicesChanger(json, debugJson);
            var iosServices = new GoogleServicesChanger(plist, debugPlist);
            
            androidServices.ReplaceGoogleServices();
            iosServices.ReplaceGoogleServices();
            
            AssetDatabase.Refresh();
            
            Debug.Log("End Operation Firebase Debug");
        }
    }
}
