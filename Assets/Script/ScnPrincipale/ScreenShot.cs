using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour {

    public string folderName;
    public string path;

    string fullPath;
    public string filename;



    // Use this for initialization
    void Start () {

        path = Directory.GetCurrentDirectory();
        if (Directory.Exists(path))
        {

            fullPath = path + "/" + folderName;
            //si le folder nexiste pas, en creer un
            if (!Directory.Exists(fullPath))
            {
                Debug.Log("bonjour");

                Directory.CreateDirectory(fullPath);
            }            
        }
        else
        {
            //TODO error
        }
	}
	
	// Update is called once per frame
	void Update () {
        filename = CreerUniqueGuid();
        ScreenCapture.CaptureScreenshot(fullPath+ "/" + filename);

    }


    private string CreerUniqueGuid()
    {
        string guid = Guid.NewGuid().ToString();
        return "/" + guid + ".jpg";
    }
}
