using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCam : MonoBehaviour {

    const int ANGLE_TOUR_DEGREE = 360;
    [Range(0,80)]
    public int ANGLE_MAX_DEGREE = 50;

    public GameObject obj;
    Camera cam;
    GameObject pivot;
    Vector3 positionIniCam;



    [Range(0,100)]
    public float distIni;
    [Range(1,90)]
    public float vitesseAngle;
    [Range(0,10)]
    public float additionDistance;
    [Range(1, 100)]
    public int nbIterationAvantRecommencer;

    float distance;

    //angle additionnee tout le long du programme
    float angleHorizontal = 0;
    float angleVertical = 0;
    float nbTour = 1;

    bool terminer = false;

	// Use this for initialization
	void Start () {
        distance = -distIni;

        //Trouver la camera
        cam = Camera.main;
        positionIniCam = cam.transform.position;
        //Trouver le pivot
        pivot = GameObject.Find("PivotCamera");

        //Trouver Lumiere


        //Position Initiale de la camera
        float profondeurCam = Mathf.Cos(ANGLE_MAX_DEGREE * Mathf.Deg2Rad) * distance;
        float hauteurCam = Mathf.Sin(-ANGLE_MAX_DEGREE * Mathf.Deg2Rad) * distance;

        cam.transform.position = new Vector3(positionIniCam.x, hauteurCam, profondeurCam);
        cam.transform.Rotate(ANGLE_MAX_DEGREE, 0, 0);


    }
	
	// Update is called once per frame
	void Update () {
        if(!terminer)
        {
            TournerCam();
            HauteurCam();
            //AugmenteDistance();
        }
    }

    void TournerCam()
    {
        pivot.transform.Rotate(0, vitesseAngle, 0);
        angleHorizontal += vitesseAngle;
    }

    void HauteurCam()
    {
        //monter la camera pour avoir dautre angle de vue apres un tour
        if (angleHorizontal >= nbTour * ANGLE_TOUR_DEGREE)
        {
            angleVertical += vitesseAngle;
            //angle reduit a chaque tour
            float profondeurCam = Mathf.Cos((ANGLE_MAX_DEGREE - angleVertical) * Mathf.Deg2Rad) * distance;
            //angle negatif au depart
            float hauteurCam = Mathf.Sin((angleVertical - ANGLE_MAX_DEGREE) * Mathf.Deg2Rad) * distance;

            cam.transform.position = new Vector3(cam.transform.position.x, hauteurCam, profondeurCam);
            cam.transform.Rotate(-vitesseAngle, 0, 0);

            ++nbTour;
        }

        
    }

    void AugmenteDistance()
    {
        //Restart hauteur
        //angleVertical augmente jusquau double de langle max
        //puisquil y a une partie positive et une partie negative
        if (angleVertical > 2 * ANGLE_MAX_DEGREE)
        {
            distance -= additionDistance;
            float profondeurCam = Mathf.Cos(ANGLE_MAX_DEGREE * Mathf.Deg2Rad) * distance;
            float hauteurCam = Mathf.Sin(-ANGLE_MAX_DEGREE * Mathf.Deg2Rad) * distance;

            cam.transform.position = new Vector3(positionIniCam.x, hauteurCam, profondeurCam);
            cam.transform.rotation = Quaternion.identity;
            cam.transform.Rotate(ANGLE_MAX_DEGREE, 0, 0);

            //reinitialiser donne
            angleVertical = 0;

            
            --nbIterationAvantRecommencer;
            //recommencer rotation et changer la luminosite
            if (nbIterationAvantRecommencer == 0)
                ChangementLuminosite();
        }
    }

    void ChangementLuminosite()
    {
        
    }
}
