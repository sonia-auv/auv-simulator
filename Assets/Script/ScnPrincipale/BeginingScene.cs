using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginingScene : MonoBehaviour {


    public int iterationReinitialisation = 100;
    //const float HAUTEUR = 6;
    //const float LARGEUR = 10;
    const float PROFONDEUR_FACE = 0;
    const float DISTANCE_MAX = 5;
    const float Z_CAM = -10;
    const int CONTRAINTE_CADRE = 5;

    GameObject[] modeles;

    Vector3 ZoneBasGauche;
    Vector3 ZoneHautDroit;

	// Use this for initialization
	void Awake () {
        modeles = GameObject.FindGameObjectsWithTag("BoundingObject");

        //ZoneBasGauche = new Vector3(-LARGEUR/2, -HAUTEUR/2, PROFONDEUR_FACE-Z_CAM);
        //ZoneHautDroit = new Vector3(LARGEUR / 2, HAUTEUR / 2, PROFONDEUR_FACE - Z_CAM);

        AssignerPosition();
        
	}

    int nombreInstancier = 0;
    List<GameObject> modelesBoundingBox;

    void AssignerPosition()
    {
        modelesBoundingBox = new List<GameObject>();
        
        for (int i = 0; i < modeles.Length; ++i)
        {
            //2 exclusif
            int choix = Random.Range(0, 2);
            bool dernierObjetNonInstancier = (i == modeles.Length - 1 && nombreInstancier == 0);
            //si 1 positionne l'objet et sassurer quil y a toujours un objet d<instancier
            if (choix == 1 || dernierObjetNonInstancier)
            {
                float distance = Random.Range(PROFONDEUR_FACE - Z_CAM, DISTANCE_MAX);
                float frustrumHeight = distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
                float frustrumWidth = frustrumHeight * Camera.main.aspect;

                float hautMax = frustrumHeight - CONTRAINTE_CADRE;
                float coteMax = frustrumWidth - CONTRAINTE_CADRE * Camera.main.aspect;

                float x = Random.Range(-coteMax, coteMax);
                float y = Random.Range(-hautMax, hautMax);

                modeles[i].transform.position = new Vector3(x, y, distance);

                nombreInstancier += 1;

                modelesBoundingBox.Add(modeles[i]);

            }
            //enleve le tag bouding Object pour ne pas lui faire de bounding box ainsi que son script
            if (choix == 0 && !dernierObjetNonInstancier)
            {
                modeles[i].GetComponent<BoundingBox>().enabled = false;
                modeles[i].tag = "Untagged";
            }

            
        }
    }

    public void ReinitialiserObjets()
    {
        for (int i = 0; i < modeles.Length; ++i)
        {
            //Remettre tout les modeles a larriere de la camera et leurs paramettres initiaux
            modeles[i].transform.position = new Vector3(0, 0, -20);
            modeles[i].GetComponent<BoundingBox>().enabled = true;
            modeles[i].tag = "BoundingObject";
        }
    }

    int compteurIteration = 0;
	// Update is called once per frame
	void Update () {

        compteurIteration++;
        //Changer les objets qui sont pris en screen shot a un certain nombre diteration 
        if(compteurIteration >= iterationReinitialisation)
        {
            ReinitialiserObjets();
            AssignerPosition();
            compteurIteration = 0;
        }

        UpdatePosition();
		

	}

    void UpdatePosition()
    {
        foreach(GameObject g in modelesBoundingBox)
        {
            float distance = Random.Range(PROFONDEUR_FACE, DISTANCE_MAX);
            float frustrumHeight = distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustrumWidth = frustrumHeight * Camera.main.aspect;

            float hautMax = frustrumHeight - CONTRAINTE_CADRE;
            float coteMax = frustrumWidth - CONTRAINTE_CADRE * Camera.main.aspect;

            float x = Random.Range(-coteMax, coteMax);
            float y = Random.Range(-hautMax, hautMax);

            g.transform.position = new Vector3(x, y, distance);
        }
    }
}
