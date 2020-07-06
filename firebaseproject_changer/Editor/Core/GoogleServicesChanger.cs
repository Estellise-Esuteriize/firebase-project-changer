
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FreCre.FirebaseProjectChanger.Extensions;
using FreCre.FirebaseProjectChanger.JsonModel;
using UnityEngine;

namespace FreCre.FirebaseProjectChanger
{
    public class GoogleServicesChanger
    {

        private readonly string _defaultGoogleServices;
        private readonly string _originGoogleServices;
        
        public GoogleServicesChanger(string defaultGoogleServices, string originGoogleServices)
        {
            _defaultGoogleServices = defaultGoogleServices;
            _originGoogleServices = originGoogleServices;
        }

        public GoogleServicesAndroidModel GetCurrentAndroidServices()
        {
            try
            {
                if (!_defaultGoogleServices.EndsWith("json"))
                {
                    throw new FileNotFoundException();
                }

                return JsonUtility.FromJson<GoogleServicesAndroidModel>(
                    File.ReadAllText(Path.Combine(Application.dataPath, _defaultGoogleServices)));
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return default;
            }
        }
        
        public GoogleServicesIosModel GetCurrentIosServices()
        {
            try
            {
                if (!_defaultGoogleServices.EndsWith("plist"))
                {
                    throw new FileNotFoundException();
                }
                
                var filePath = Path.Combine(Application.dataPath, _defaultGoogleServices);
                var document = XDocument.Load(filePath);
                var iosKeyValues = document.Descendants("dict")
                    .SelectMany(d => d.Elements("key").Zip(d.Elements().Where(e => e.Name != "key"), (k, v) => new {Key = k, Value = v}))
                    .ToDictionary(i => i.Key.Value, i => (object)i.Value.Value);

                return iosKeyValues.DictionaryToObject<GoogleServicesIosModel>();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return default;
            }
        }


        public void ReplaceGoogleServices()
        {
            var copyPathFile = Path.Combine(Application.dataPath, _originGoogleServices);
            var writePathFile = Path.Combine(Application.dataPath, _defaultGoogleServices);
            
            if (File.Exists(copyPathFile))
            {
                File.WriteAllText(writePathFile, File.ReadAllText(copyPathFile));
            }
            else
            {
                throw new FileNotFoundException($"No {_originGoogleServices} found.");
            }
        }
    }
}
