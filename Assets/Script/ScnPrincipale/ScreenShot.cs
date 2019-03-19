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


    public string FullPath
    {
        get { return fullPath; }
        private set { fullPath = value; }
    }
    // Use this for initialization
    void Start () {

        path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/auv-data"; 
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);


        }

        FullPath = path + "/" + folderName;
        //si le folder nexiste pas, en creer un
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
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
