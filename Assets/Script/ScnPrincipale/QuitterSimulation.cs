using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class QuitterSimulation : MonoBehaviour {

    public const string FICHIER_TEXTE_ENTREE = "DonneeEntre.txt";

    int compteurFrame = 0;
    int nbFrame=0;
    // Use this for initialization
    void Start () {

        using (StreamReader sr = new StreamReader(FICHIER_TEXTE_ENTREE))
        {
            nbFrame = int.Parse(sr.ReadLine());
        }
	}
	
	// Update is called once per frame
	void Update () {

        ++compteurFrame;

        if (Input.GetKeyDown(KeyCode.Escape) || compteurFrame > nbFrame)
            SceneManager.LoadScene(0);
	}
}
