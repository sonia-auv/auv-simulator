using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class ModelPath : MonoBehaviour {

    Text txtPath;
    Color couleurBase;

	// Use this for initialization
	void Start () {
        //le texte entre et non le placeholder
        txtPath = GetComponentsInChildren<Text>()[1];
        couleurBase = txtPath.color;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GoodModelPath(string path)
    {
        if (txtPath.text != "")
        {

            if (File.Exists(txtPath.text))
            {
                txtPath.color = couleurBase;
                //TODO check if its a 3d model
            }
            else
            {
                txtPath.color = Color.red;
            }
        }
        else
        {
            //TODO ne pas mettre derreur
            txtPath.color = couleurBase;
        }
    }
}
