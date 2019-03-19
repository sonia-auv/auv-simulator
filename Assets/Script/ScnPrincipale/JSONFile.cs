using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONFile : MonoBehaviour {

    string path;
    string fileName = "dataImages.json";
    string fullPath;

    string infoJSON = "";

	// Use this for initialization
	void Start () {
        ScreenShot scriptScreenShot = GameObject.Find("Control").GetComponent<ScreenShot>();

        path = scriptScreenShot.FullPath;

        fullPath = path + "/" + fileName;

          
		
	}
	

    /// <summary>
    /// Ecris dans un seul fichier JSON grace a l'objet JSONData
    /// Tout les autres objets y sont aussi ecris
    /// </summary>
    /// <param name="dataJSON"></param>
	public void WriteInJSONFile(JSONData dataJSON)
    {
        //permet davoir le string de lancien JSON file
        if (File.Exists(fullPath))
        {
            using (StreamReader sr = new StreamReader(fullPath))
            {
                infoJSON = sr.ReadToEnd();
            }
        }
        //true permet de ne pas overwrite le fichier
        using (StreamWriter sw = new StreamWriter(fullPath))
        {
            if(infoJSON == "")
            {
                sw.Write("[\n" + dataJSON.AllInfo + "\n]");

            }
            else
            {
                
                infoJSON = infoJSON.Remove(infoJSON.Length - 1);
                infoJSON += ",";
                sw.Write(infoJSON + "\n" + dataJSON.AllInfo + "\n]");
            }
                
        }
    }
}
