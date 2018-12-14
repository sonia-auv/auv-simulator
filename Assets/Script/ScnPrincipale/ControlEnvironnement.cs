using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControlEnvironnement : MonoBehaviour {

    //const int ANGLE_UN_TOUR = 360;
    const string NB_FRAME_FILE = "DonneeEntre.txt";

    //int angleModeleInitial = 50;
    List<Transform> modeles;
    GameObject[] obj;
    float[] coefficientRotation;

    public int vitesseRotation = 1;

    int additionAngleLacet = 0;
    int additionAngleTangage = 0;


	// Use this for initialization
	void Start () {
        modeles = new List<Transform>();
        obj = GameObject.FindGameObjectsWithTag("BoundingObject");
        coefficientRotation = new float[obj.Length];

        for(int i =0; i < obj.Length; ++i)
        {
            modeles.Add(obj[i].GetComponentInChildren<Transform>());
            coefficientRotation[i] = Random.Range(1, 10);
            //modeles[i].GetComponentInParent<Transform>().Rotate(50, 0, 0);

        }

        

       
    }
	
	// Update is called once per frame
	void Update () {

        TournerModele();
       
	}

    void TournerModele()
    {
        //foreach(Transform t in modeles)
        //{
        //    t.transform.Rotate(0, vitesseRotation, 0);
            //additionAngleLacet += vitesseRotation;
            //if (additionAngleLacet > ANGLE_UN_TOUR)
            //{
            //    t.transform.Rotate(-vitesseRotation, 0, 0);
            //    additionAngleLacet -= ANGLE_UN_TOUR;
            //}
        //}

        for(int i = 0; i< modeles.Count; ++i)
        {
            float rotation = vitesseRotation * coefficientRotation[i];
            modeles[i].Rotate(0, rotation, 0);

        }
        
    }
}
