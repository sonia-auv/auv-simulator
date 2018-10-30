using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnvironnement : MonoBehaviour {

    const int ANGLE_UN_TOUR = 360;

    int angleModeleInitial = 50;
    Transform modele;

    public int vitesseRotation = 1;


    int additionAngleLacet = 0;
    int additionAngleTangage = 0;

	// Use this for initialization
	void Start () {
        modele = GameObject.FindGameObjectsWithTag("BoundingObject")[0].GetComponentInChildren<Transform>();
        modele.GetComponentInParent<Transform>().Rotate(50, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        TournerModele();
	}

    void TournerModele()
    {
        
        modele.transform.Rotate(0, vitesseRotation, 0);
        additionAngleLacet += vitesseRotation;
        if(additionAngleLacet > ANGLE_UN_TOUR)
        {
            modele.transform.Rotate(-vitesseRotation, 0, 0);
            additionAngleLacet -= ANGLE_UN_TOUR;
        }
    }
}
