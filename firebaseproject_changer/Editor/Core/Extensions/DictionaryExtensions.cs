using System;
using System.Collections.Generic;
using System.Dynamic;

namespace FreCre.FirebaseProjectChanger.Extensions
{
    public static class DictionaryExtensions
    {
        
        public static T DictionaryToObject<T>(this Dictionary<string, object> source) where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType.GetField(item.Key)?.SetValue(someObject, item.Value);
            }

            return someObject;
        }

        public static T DictionaryToObjectExpando<T, TValue>(this Dictionary<string, TValue> dictionary) where T : class
        {
            return DictionaryToObjectExpando<TValue>(dictionary) as T;
        }

        public static T DictionaryToObjectExpando<T>(this Dictionary<string, object> dictionary) where T : class  
        {  
            return DictionaryToObjectExpando(dictionary) as T;  
        }
        
        private static dynamic DictionaryToObjectExpando(Dictionary<string, object> dictionary)  
        {  
            var expandoObj = new ExpandoObject();  
            var expandoObjCollection = (ICollection<KeyValuePair<string, object>>)expandoObj;  
  
            foreach (var keyValuePair in dictionary)  
            {  
                expandoObjCollection.Add(keyValuePair);  
            }  
            
            return expandoObj; 
        }  
        
        
        private static dynamic DictionaryToObjectExpando<T>(Dictionary<string, T> dictionary)  
        {  
            var expandoObj = new ExpandoObject();  
            var expandoObjCollection = (ICollection<KeyValuePair<string, object>>)expandoObj;  
  
            foreach (var keyValuePair in dictionary)  
            {  
                expandoObjCollection.Add(new KeyValuePair<string, object>(keyValuePair.Key, keyValuePair.Value));  
            }  
            
            return expandoObj;  
        } 
    }
}