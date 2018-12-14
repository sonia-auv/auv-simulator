using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LancerSimulation : MonoBehaviour {

    public const string FICHIER_TEXTE_ENTREE = "DonneeEntre.txt";

    InputField InputNbFrame;

    int valeurNbFrame;

    void Awake()
    {
        Screen.SetResolution(1280, 960, false);
    }

    // Use this for initialization
    void Start () {

        InputNbFrame = GetComponentInChildren<InputField>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(int sceneIndex)
    {

        using (StreamWriter sw = new StreamWriter(FICHIER_TEXTE_ENTREE))
        {
            sw.Write(valeurNbFrame.ToString());
        }
        SceneManager.LoadScene(sceneIndex);
    }

    int min = 30;
    int max = 10000;
    public void InputFrameMinMax()
    {
        valeurNbFrame = int.Parse(InputNbFrame.text);
        valeurNbFrame = Mathf.Clamp(valeurNbFrame, min, max);
        InputNbFrame.text = valeurNbFrame.ToString();
    }
}
