using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelImages : MonoBehaviour {

    ScreenShot scriptScreenShot;
    JSONData scriptJson;
    JSONFile scriptJsonFile;
    LabelImages scriptLabel;

    List<BoundingBox> scriptBB;
    GameObject[] objets;

    string[,,] boxPoints;

    string fileName;
    string title;

    [SerializeField]
    string googleDrivePath;

    bool firstFrame = true;
    

	// Use this for initialization
	void Start () {
        scriptScreenShot = gameObject.GetComponent<ScreenShot>();
        scriptJsonFile = gameObject.GetComponent<JSONFile>();
        scriptLabel = gameObject.GetComponent<LabelImages>();
        scriptBB = new List<BoundingBox>();
        objets = GameObject.FindGameObjectsWithTag("BoundingObject");

        for (int i = 0; i < objets.Length; ++i)
        {
            scriptBB.Add(objets[i].GetComponent<BoundingBox>());
        }

        title = scriptLabel.googleDrivePath;

    }
	
	// Update is called once per frame
	void Update () {
        if (!firstFrame)
        {
            fileName = scriptScreenShot.filename;
            GetBoxPoints();

            scriptJson = new JSONData(googleDrivePath + fileName, title, boxPoints);
            scriptJsonFile.WriteInJSONFile(scriptJson);
        }
        firstFrame = false;
	}

    /// <summary>
    /// Premier indice sont les nombres de point du bounding box soit 4
    /// Deuxieme indice est la valeur x ou y ou le nom de l'objet ( 
    /// Troisieme indice est le nombre d'objet dans la scene
    /// </summary>
    void GetBoxPoints()
    {
        boxPoints = new string[4, 3, objets.Length];
        for (int i = 0; i < objets.Length; ++i)
        {
            boxPoints[0, 0, i] = scriptBB[i].PixelHautGauche.x.ToString();
            boxPoints[0, 1, i] = scriptBB[i].PixelHautGauche.y.ToString();
            boxPoints[0, 2, i] = objets[i].name;

            boxPoints[1, 0, i] = scriptBB[i].PixelHautDroit.x.ToString();
            boxPoints[1, 1, i] = scriptBB[i].PixelHautDroit.y.ToString();
            boxPoints[1, 2, i] = objets[i].name;

            boxPoints[2, 0, i] = scriptBB[i].PixelBasDroit.x.ToString();
            boxPoints[2, 1, i] = scriptBB[i].PixelBasDroit.y.ToString();
            boxPoints[2, 2, i] = objets[i].name;

            boxPoints[3, 0, i] = scriptBB[i].PixelBasGauche.x.ToString();
            boxPoints[3, 1, i] = scriptBB[i].PixelBasGauche.y.ToString();
            boxPoints[3, 2, i] = objets[i].name;
        }
    }
}
