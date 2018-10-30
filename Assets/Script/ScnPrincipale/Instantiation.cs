using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Instantiation : MonoBehaviour {

    GameObject[] models;
    List<string> donneFichier;

    // Use this for initialization
    void Start () {

        GetDonneesFichier();

        InstantierModels();


		
	}

    void GetDonneesFichier()
    {
        //string fichierTexte = Directory.GetFiles()

        donneFichier = new List<string>();

        using (StreamReader sr = new StreamReader("DonneeEntre.txt"))
        {
            while (sr.ReadLine() != "")
            {
                donneFichier.Add(sr.ReadLine());
            }
        }
    }

    void InstantierModels()
    {
        
    }

}
