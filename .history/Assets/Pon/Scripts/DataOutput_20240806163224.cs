using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO; 


public class DataOutput 
{
    
    public void SaveData<T>(List<T> datatosave, string path, string name)
    {
        Debug.Log("eyeDataToSave: "+ datatosave.Count);
    
        var jsonData  = JsonConvert.SerializeObject(datatosave);
        Debug.Log("json: "+jsonData);

       if (!Directory.Exists(Application.dataPath + path)){
           Directory.CreateDirectory(Application.dataPath + path);
       }
       File.WriteAllText
        (Application.dataPath + path + name + System.DateTime.Now.ToString("-MM-dd-HH-mm-ss-yyyy") + ".json", 
        jsonData);
    }

}
