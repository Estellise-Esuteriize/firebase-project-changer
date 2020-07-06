using FreCre.FirebaseProjectChanger;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class FirebaseChangerMenu
    {

        private const string ANDROID_JSON = "google-services.json";
        private const string IOS_PLIST = "GoogleService-Info.plist";

        private const string ANDROID_JSON_FILE_DEBUG = "google-services_debug.json";
        private const string ANDROID_JSON_FILE_RELEASE = "google-services_release.json";
        
        private const string IOS_PLIST_FILE_DEBUG = "GoogleService-Info_debug.plist";
        private const string IOS_PLIST_FILE_RELEASE = "GoogleService-Info_release.plist";
        

        [MenuItem("Assets/FirebaseProject/Debug")]
        public static void SetDebug()
        {
            var androidServices = new GoogleServicesChanger(ANDROID_JSON, ANDROID_JSON_FILE_DEBUG);
            var iosServices = new GoogleServicesChanger(IOS_PLIST, IOS_PLIST_FILE_DEBUG);
            
            androidServices.ReplaceGoogleServices();
            iosServices.ReplaceGoogleServices();
            
            AssetDatabase.Refresh();
        }


        [MenuItem("Assets/FirebaseProject/Release")]
        public static void SetRelease()
        {
            var androidServices = new GoogleServicesChanger(ANDROID_JSON, ANDROID_JSON_FILE_RELEASE);
            var iosServices = new GoogleServicesChanger(IOS_PLIST, IOS_PLIST_FILE_RELEASE);
            
            androidServices.ReplaceGoogleServices();
            iosServices.ReplaceGoogleServices();
            
            AssetDatabase.Refresh();
        }


        [MenuItem("Assets/FirebaseProject/CurrentProject")]
        public static void CurrentFirebaseProject()
        {
            
            // json
            var androidServices = new GoogleServicesChanger(ANDROID_JSON, ANDROID_JSON_FILE_RELEASE);
            var androidJson = androidServices.GetCurrentAndroidServices();
            
            Debug.Log("Android Project Id : " + androidJson.project_info.project_id);
            Debug.Log("Android Database Url : " + androidJson.project_info.firebase_url);
            Debug.Log("Android Storage Bucket : " + androidJson.project_info.storage_bucket);
           
            // ---
            Debug.Log("-----------------");
            
            
            // ios
            var iosServices = new GoogleServicesChanger(IOS_PLIST, IOS_PLIST_FILE_RELEASE);
            var iosPlist = iosServices.GetCurrentIosServices();
            
            Debug.Log("IOS Project Id : " + iosPlist.PROJECT_ID);
            Debug.Log("IOS Database Url : " + iosPlist.DATABASE_URL);
            Debug.Log("IOS Storage Bucket : " + iosPlist.STORAGE_BUCKET);
        }

    }
}