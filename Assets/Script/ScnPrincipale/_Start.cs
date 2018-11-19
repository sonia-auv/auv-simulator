using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class _Start : MonoBehaviour {

    string path;
    string donnes;
    List<string> listDonnes;
    List<string> listObjet;

    // Use this for initialization
    void Start () {
        path = LancementSimulation.FICHIER_TEXTE_ENTREE;
        GetDonneStart();
        InstantiateModeles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Prend les donnees du fichier texte
    void GetDonneStart()
    {
        listDonnes = new List<string>();
        using (StreamReader sr = new StreamReader(path))
        {
            string ligne;
            while ((ligne = sr.ReadLine()) != null)
            {
                listDonnes.Add(ligne);
            }
            
        }
    }

    //Ajoute les objet dans la liste d'objet selon leur type de fichier
    void InstantiateModeles()
    {
        listObjet = new List<string>();
        foreach(string s in listDonnes)
        {
            //TODO s'assurer davoir tout les fichier de modele possible
            if(s.EndsWith(".sldprt") || s.EndsWith(".FBX") || s.EndsWith(".DAE") || s.EndsWith(".3DS") ||s.EndsWith(".OBJ"))
            {
                listObjet.Add(s);
                //File.Copy(s, Directory.GetCurrentDirectory() + "/" )
            }
        }


    }
}
