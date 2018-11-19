using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnvironnement : MonoBehaviour {

    const int ANGLE_UN_TOUR = 360;

    int angleModeleInitial = 50;
    List<Transform> modeles;
    GameObject[] obj;

    public int vitesseRotation = 1;


    int additionAngleLacet = 0;
    int additionAngleTangage = 0;

	// Use this for initialization
	void Start () {
        modeles = new List<Transform>();
        obj = GameObject.FindGameObjectsWithTag("BoundingObject");

        for(int i =0; i < obj.Length; ++i)
        {
            modeles.Add(obj[i].GetComponentInChildren<Transform>());
            modeles[i].GetComponentInParent<Transform>().Rotate(50, 0, 0);

        }
    }
	
	// Update is called once per frame
	void Update () {

        TournerModele();
	}

    void TournerModele()
    {
        foreach(Transform t in modeles)
        {
            t.transform.Rotate(0, vitesseRotation, 0);
            additionAngleLacet += vitesseRotation;
            if (additionAngleLacet > ANGLE_UN_TOUR)
            {
                t.transform.Rotate(-vitesseRotation, 0, 0);
                additionAngleLacet -= ANGLE_UN_TOUR;
            }
        }
        
    }
}
