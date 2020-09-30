using Packages.FirebaseProjectManager.Project_Manager.Editor.Core;
using Packages.FirebaseProjectManager.Project_Manager.Editor.Processing.Extensions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Packages.FirebaseProjectManager.Project_Manager.Editor.Processing
{
    public class PreProcessChanger : IPreprocessBuildWithReport
    {

        public int callbackOrder
        {
            get
            {
                return 0;
            }
        }
    
    
        public void OnPreprocessBuild(BuildReport report)
        {
            // TODO:
            // change [json, plist] default file names to assets

            const string json = "google-services.json";
            const string plist = "GoogleService-Info.plist";

            var releaseJson = ProcessChangerExtensions.GetGoogleServicesJsonFile(!Debug.isDebugBuild);
            var releasePlist = ProcessChangerExtensions.GetGoogleServicesPlistFile(!Debug.isDebugBuild);
            
            Debug.Log("Start Operation Firebase Release");
            
            var androidServices = new GoogleServicesChanger(json, releaseJson);
            var iosServices = new GoogleServicesChanger(plist, releasePlist);
            
            androidServices.ReplaceGoogleServices();
            iosServices.ReplaceGoogleServices();
            
            AssetDatabase.Refresh();
            
            Debug.Log("End Operation Firebase Release");
        }
    }
}
