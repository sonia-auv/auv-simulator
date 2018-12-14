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

    bool voirLigne = true;

	// Use this for initialization
	void Start () {
        model3D = gameObject;
        
        //Line renderer pour creer le bounding box
        line = GetComponentInChildren<LineRenderer>();
        if (line == null)
            voirLigne = false;

        cam = Camera.main;
        camTransform = cam.transform;
        
	}
	
	// LateUpdate on veut creer le bounding box apres que le robot ai bouge
	void Update () {

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

        AssignerCoinBox2();

        //Si le line renderer est assignee on affiche le bouding box
        if(voirLigne)
        {
            //assigne les position pour creer le bounding box grace a un line renderer
            Vector3[] tableauLigne = new Vector3[5];
            tableauLigne[0] = CoinHautGauche;
            tableauLigne[1] = CoinHautDroit;
            tableauLigne[2] = CoinBasDroit;
            tableauLigne[3] = CoinBasGauche;
            tableauLigne[4] = CoinHautGauche;
            line.SetPositions(tableauLigne);
        }
        

    }

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

    /// <summary>
    /// Ajuster la box selon la perspective
    /// Quand l'objet est a droite, on veut prendre en consideration sa profondeur de gauche pour que la bounding box soit plus precise
    /// </summary>
    void AssignerCoinBox2()
    {

        float xGauche = (bounds.center.x - bounds.extents.x) - cam.transform.position.x; 
        float xDroit = (bounds.center.x +bounds.extents.x) -cam.transform.position.x; 
        float yBas = (bounds.center.y - bounds.extents.y) - cam.transform.position.y; 
        float yHaut = (bounds.center.y + bounds.extents.y) - cam.transform.position.y; 
        
        if (model3D.transform.position.x >= 1)
        {
            //Xgauche

            //Prendre le point en x le plus pres de la camera et le plus loin en z
            float xG = (bounds.center.x - bounds.extents.x) - cam.transform.position.x;
            float zG = (bounds.center.z + bounds.extents.z) - cam.transform.position.z;

            //Trouver le point selon ce vecteur quand le x est au bounds avant de lobjet
            float angleMin = Mathf.Atan(zG / xG);
            //xMin : tan() = opp/adj --> tan() = opp/xmin
            float oppMin = (bounds.center.z - bounds.extents.z) - cam.transform.position.z;
            xGauche = oppMin / Mathf.Tan(angleMin);

            //----------------
            //Xdroit
            //prendre le point en x le plus eloigner de la cam et le plus pres en z
            xDroit = (bounds.center.x + bounds.extents.x) - cam.transform.position.x;
            




        }
        else if (model3D.transform.position.x < 1)
        {
            //Xdroit

            //Prendre le point en x le plus pres de la camera et le plus loin en z
            float xD = (bounds.center.x + bounds.extents.x) - cam.transform.position.x;
            float zD = (bounds.center.z + bounds.extents.z) - cam.transform.position.z;

            //Trouver le point selon ce vecteur quand le x est au bounds avant de lobjet
            float angleMin = Mathf.Atan(zD / xD);
            //xMin : tan() = opp/adj --> tan() = opp/xmin
            float oppMin = (bounds.center.z - bounds.extents.z) - cam.transform.position.z;
            xDroit = oppMin / Mathf.Tan(angleMin);

            //----------------
            //Xmax
            //prendre le point en x le plus eloigner de la cam et le plus pres en z
            xGauche = (bounds.center.x - bounds.extents.x) - cam.transform.position.x;
        }
        if(model3D.transform.position.y >= 1)
        {
            //YBas

           //Quand lobjet est plus haut que la cam prendre le point en bas arriere
           float yB = (bounds.center.y - bounds.extents.y) - cam.transform.position.y;
           float zB = (bounds.center.z + bounds.extents.z) - cam.transform.position.z;

            //Trouver le point selon ce vecteur quand le y est au bounds avant de lobjet
            float angleMin = Mathf.Atan(zB / yB);
            //xMin : tan() = opp/adj --> tan() = opp/xmin
            float oppMin = (bounds.center.z - bounds.extents.z) - cam.transform.position.z;
            yBas = oppMin / Mathf.Tan(angleMin);

            //--------------
            //YHaut
            yHaut = (bounds.center.y + bounds.extents.y) - cam.transform.position.y;
        }
        else if(model3D.transform.position.y < -1)
        {
            //YHaut
            //Quand lobjet est plus bas que la cam prendre le point en Haut arriere
            float yH = (bounds.center.y + bounds.extents.y) - cam.transform.position.y;
            float zH = (bounds.center.z + bounds.extents.z) - cam.transform.position.z;

            //Trouver le point selon ce vecteur quand le y est au bounds avant de lobjet
            float angleMin = Mathf.Atan(zH / yH);
            //xMin : tan() = opp/adj --> tan() = opp/xmin
            float oppMin = (bounds.center.z - bounds.extents.z) - cam.transform.position.z;
            yHaut = oppMin / Mathf.Tan(angleMin);

            //--------------
            //YBas
            yBas = (bounds.center.y - bounds.extents.y) - cam.transform.position.y;
        }


        //Assigner les coins
        //coin superieur gauche
        CoinHautGauche = new Vector3(xGauche - espacement, yHaut /*+ espacement*/, bounds.min.z);
        //coin superieur droit
        CoinHautDroit = new Vector3(xDroit + espacement, yHaut /*+ espacement*/, bounds.min.z);
        //coin inferieur droit
        CoinBasDroit = new Vector3(xDroit + espacement, yBas /*- espacement*/, bounds.min.z);
        //coin inferieur gauche
        CoinBasGauche = new Vector3(xGauche - espacement, yBas /*- espacement*/, bounds.min.z);
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
