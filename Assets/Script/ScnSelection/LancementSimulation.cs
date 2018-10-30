using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LancementSimulation : MonoBehaviour {

    public const string FICHIER_TEXTE_ENTREE = "DonneeEntre.txt";

    [SerializeField]
    List<GameObject> panelsInputField;

    List<InputField> allInputFields;

    Color couleurBase;
	// Use this for initialization
	void Start () {

        allInputFields = new List<InputField>();
        foreach(GameObject p in panelsInputField)
        {
            List<InputField> inputs = new List<InputField>(p.GetComponentsInChildren<InputField>());
            foreach(InputField i in inputs)
            {
                allInputFields.Add(i);
            }
        }

        couleurBase = allInputFields[0].GetComponentsInChildren<Text>()[1].color;

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //Verifie que toute les entree sont bonnes
    bool Verification()
    {
        bool bonneCouleur = true;
        bool nonNul = true;
        foreach(InputField input in allInputFields)
        {
            if (input.GetComponentsInChildren<Text>()[1].color != couleurBase)
            {
                //Not gouuchh
                bonneCouleur = false;
            }
            if(input.GetComponentsInChildren<Text>()[1].text == "")
            {
                //Not gouch either
                nonNul = false;
            }

        }
        return bonneCouleur || nonNul;
    }


    //Fichier texte de pour garder les info pour la prochaine scene
    void EcrireFichier()
    {
        using (StreamWriter sw = new StreamWriter(FICHIER_TEXTE_ENTREE))
        {
            foreach(InputField i in allInputFields)
            {
                string texteEntre = i.GetComponentsInChildren<Text>()[1].text;
                if (texteEntre != "")
                {
                    sw.WriteLine(/*i.name +": " +*/ texteEntre);
                }
            }
        }
    }

    public void Lancer()
    {
        bool pouvoirLancer = Verification();
        if(pouvoirLancer)
        {
            EcrireFichier();
            SceneManager.LoadScene("ScnPrincipale");
        }
        else
        {
            //TODO Mettre une erreur
        }
    }
}
