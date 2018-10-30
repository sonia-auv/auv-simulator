using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour {

    public float espacement = 0.1f;

    GameObject model3D;
    Bounds bounds;
    LineRenderer line;

    Transform camTransform;

    Camera cam;

	// Use this for initialization
	void Start () {
        model3D = gameObject;

        //Line renderer pour creer le bounding box
        line = GetComponentInChildren<LineRenderer>();

        cam = Camera.main;
        camTransform = cam.transform;
        
	}
	
	// LateUpdate on veut creer le bounding box apres que le robot ai bouge
	void LateUpdate () {

        CreerBoundingBox();
        DefinirCoinPixel();
    }


    /// <summary>
    /// Permet de creer un bounding box autour du model 3D en allant chercher son mesh renderer
    /// Ce bounding box n'est exact que si la camera est aux coordonnees (0,0,z)
    /// </summary>
    void CreerBoundingBox()
    {
        //Recherche les bounds de l'objet en question a chaque frame, car ceux-ci change

        bounds = model3D.GetComponentInChildren<Renderer>().bounds;

        AssignerCoinBox();

        //assigne les position pour creer le bounding box grace a un line renderer
        Vector3[] tableauLigne = new Vector3[5];
        tableauLigne[0] = CoinHautGauche;
        tableauLigne[1] = CoinHautDroit;
        tableauLigne[2] = CoinBasDroit;
        tableauLigne[3] = CoinBasGauche;
        tableauLigne[4] = CoinHautGauche;
        line.SetPositions(tableauLigne);

    }

    //void AssignerCoinBox()
    //{
        
    //    //coin superieur gauche
    //    CoinHautGauche = new Vector3(bounds.min.x - espacement, bounds.max.y + espacement, bounds.min.z);
    //    //coin superieur droit
    //    CoinHautDroit = new Vector3(bounds.max.x + espacement, bounds.max.y + espacement, bounds.min.z);
    //    //coin inferieur droit
    //    CoinBasDroit = new Vector3(bounds.max.x + espacement, bounds.min.y - espacement, bounds.min.z);
    //    //coin inferieur gauche
    //    CoinBasGauche = new Vector3(bounds.min.x - espacement, bounds.min.y - espacement, bounds.min.z);
    //}

    void AssignerCoinBox()
    {
        Vector3 centreBound = AjusterCentreBoundingBox();
        //TODO trouver un moyen de bien amplifier les bounds selon l'angle
        float amplification = Mathf.Abs(model3D.transform.position.x) / 20;
        //coin superieur gauche
        CoinHautGauche = new Vector3(centreBound.x - bounds.extents.x-amplification - espacement, centreBound.y + bounds.extents.y + espacement, bounds.min.z);
        //coin superieur droit
        CoinHautDroit = new Vector3(centreBound.x + bounds.extents.x+amplification + espacement, centreBound.y + bounds.extents.y + espacement, bounds.min.z);
        //coin inferieur droit
        CoinBasDroit = new Vector3(centreBound.x + bounds.extents.x+amplification + espacement, centreBound.y - bounds.extents.y - espacement, bounds.min.z);
        //coin inferieur gauche
        CoinBasGauche = new Vector3(centreBound.x - bounds.extents.x-amplification - espacement, centreBound.y - bounds.extents.y - espacement, bounds.min.z);
    }

    void AssignerCoinBox2()
    {
        if (model3D.transform.position.x >= 0)
        {
            //ajuster la valeur en x minimale selon la position de l'objet
            float opposeG = cam.transform.position.x - bounds.min.x;
            float adjG = cam.transform.position.z - bounds.center.z;
            float angleXG = Mathf.Atan(opposeG / adjG);

            float xAjustementG = Mathf.Sin(angleXG * bounds.extents.z * 2);
            float xAjusterG = bounds.min.x - xAjustementG;


            CoinHautGauche = new Vector3(xAjusterG, bounds.max.y, bounds.min.z);
            CoinHautDroit = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
            CoinBasDroit = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
            CoinBasGauche = new Vector3(xAjusterG, bounds.min.y, bounds.min.z);


        }
        else if (model3D.transform.position.x < 0)
        {
            
            //ajuster la valeur en x maximale selon la position de l'objet
            float opposeD = cam.transform.position.x - bounds.max.x;
            float adjD = cam.transform.position.z - bounds.center.z;
            float angleXD = Mathf.Atan(opposeD / adjD);

            float xAjustementD = Mathf.Sin(angleXD * bounds.extents.z * 2);
            float xAjusterD = bounds.max.x - xAjustementD;

            CoinHautGauche = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
            CoinHautDroit = new Vector3(xAjusterD, bounds.max.y, bounds.min.z);
            CoinBasDroit = new Vector3(xAjusterD, bounds.min.y, bounds.min.z);
            CoinBasGauche = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        }
        else if(model3D.transform.position.y >= 0)
        {
            float opposeH = cam.transform.position.y - bounds.max.y;
            float adjH = cam.transform.position.z - bounds.center.z;
            float angleYH = Mathf.Atan(opposeH / adjH);

            float yAjustementH = Mathf.Sin(angleYH)* bounds.extents.z;
            float yAjuster = bounds.max.y + yAjustementH;

            CoinHautGauche = new Vector3(bounds.min.x, yAjuster, bounds.min.z);
            CoinHautDroit = new Vector3(bounds.max.x,yAjuster, bounds.min.z);
            CoinBasDroit = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
            CoinBasGauche = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);

        }
    }

    

    void DefinirCoinPixel()
    {
        pixelHautGauche_ = cam.WorldToScreenPoint(CoinHautGauche);
        PixelHautDroit = cam.WorldToScreenPoint(CoinHautDroit);
        PixelBasDroit = cam.WorldToScreenPoint(CoinBasDroit);
        PixelBasGauche = cam.WorldToScreenPoint(CoinBasGauche);
    }

    Vector3 AjusterCentreBoundingBox()
    {
        //vecteur entre la cam et l'objet : dir
        //Et trouver lequation lineaire de la cam
        //mettre le centre du box selon lequation lineaire en face de lobjet

        //(x,y,z) = ai + bj + ck + t(ui + vj + wk)
        Vector3 droite = new Vector3(bounds.center.x - cam.transform.position.x, bounds.center.y - cam.transform.position.y, bounds.center.z - cam.transform.position.z);

        Vector3 pente = droite.normalized;
        Vector3 point = cam.transform.position;

        //multiplicateur t bounding box --> t = (bound.z-cam.z)/pente.z
        float t = (bounds.min.z - cam.transform.position.z) / pente.z;

        Vector3 newCentreBounds = new Vector3(point.x + pente.x * t, point.y + pente.y * t, point.z + pente.z * t);

        return newCentreBounds;
    }

    //ajuster selon les angles
    void AjusterCoinBB()
    {
        //si lobjet est a droite, prend le coin bas arriere gauche et bas avant droit pour la largeur du bb
        //si lobjet est a gauche --> contraire
       
    }

    Vector3 coinHautGauche_;
    Vector3 coinHautDroit_;
    Vector3 coinBasGauche_;
    Vector3 coinBasDroit_;

    public Vector3 CoinHautGauche
    {
        get { return coinHautGauche_; }
        private set
        {
            coinHautGauche_ = value;
        }
    }
    public Vector3 CoinHautDroit
    {
        get { return coinHautDroit_; }
        private set
        {
            coinHautDroit_ = value;
        }
    }
    public Vector3 CoinBasDroit
    {
        get { return coinBasDroit_; }
        private set
        {
            coinBasDroit_ = value;
        }
    }
    public Vector3 CoinBasGauche
    {
        get { return coinBasGauche_; }
        private set
        {
            coinBasGauche_ = value;
        }
    }

    Vector3 pixelHautGauche_;
    Vector3 pixelHautDroit_;
    Vector3 pixelBasDroit_;
    Vector3 pixelBasGauche_;
    
    public Vector3 PixelHautGauche
    {
        get { return pixelHautGauche_; }
        private set
        {
            pixelHautGauche_ = value;
        }
    }
    public Vector3 PixelHautDroit
    {
        get { return pixelHautDroit_; }
        private set
        {
            pixelHautDroit_ = value;
        }
    }
    public Vector3 PixelBasDroit
    {
        get { return pixelBasDroit_; }
        private set
        {
            pixelBasDroit_ = value;
        }
    }
    public Vector3 PixelBasGauche
    {
        get { return pixelBasGauche_; }
        private set
        {
            pixelBasGauche_ = value;
        }
    }
}
