using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONData : MonoBehaviour {

    string id_;
    string labeledData_;
    string labelName_;
    string[,,] boxPoints_;
    string[] label_;
    string allLabel_;
    string createBy_;
    string projectName_;
    string createAt_;
    string secondsToLabel_;
    string externalId_;
    string agreement_;
    string dataSetName_;
    string review_;
    string viewLabel_;


    public string ID
    {
        get { return id_; }
        private set
        {
            id_ = "\"ID\": " + '\"' + value + '\"';
        }
    }

    public string LabeledData
    {
        get { return labeledData_; }
        private set
        {
            labeledData_ = "\"Labeled Data\": " + '\"' + value + '\"';
        }
    }

    public string LabelName
    {
        get { return labelName_; }
        private set
        {
            labelName_ = value;
        }
    }


    //TODO mettre les points dans le bon orde: hautGauche basgauche basdroit hautdroit
    const int NB_COTES = 4;
    const int NB_COODONNEES = 3; // dans ce cas nous utilisons juste les coordonnes x,y et nom de lobjet
    public string[,,] BoxPoints
    {
        get { return boxPoints_; }
        private set
        {
            boxPoints_ = new string[NB_COTES, NB_COODONNEES, value.GetLength(2)];
            for (int j = 0; j < value.GetLength(2); ++j) //j = nombre d'objet
            {
                for (int i = 0; i < NB_COTES; ++i)
                {
                    boxPoints_[i, 0, j] = value[i, 0, j];
                    boxPoints_[i, 1, j] = value[i, 1, j];
                    boxPoints_[i, 2, j] = value[i, 2, j];
                }
            }
        }
    }

    //Avoir l'information et l'indentation du label au complet
    public string[] Label()
    {
        label_ = new string[BoxPoints.GetLength(2)];
        for (int l = 0; l < label_.Length; ++l)
        {
            label_[l] =
                "\"" + BoxPoints[0, 2, l] + "\": [\n";

            for (int i = 0; i < NB_COTES; ++i)
            {
                if (i< NB_COTES - 1)
                {
                    label_[l] +=
                    "{\n" +
                    "\"x\": " + BoxPoints[i, 0, l] + ",\n" +
                    "\"y\": " + BoxPoints[i, 1, l] + "\n" +
                    "},\n";
                }
                else
                {
                    label_[l] +=
                    "{\n" +
                    "\"x\": " + BoxPoints[i, 0, l] + ",\n" +
                    "\"y\": " + BoxPoints[i, 1, l] + "\n" +
                    "}\n";
                }
            }

            if (l < label_.Length - 1)
            {
                label_[l] +=
                    "],\n";
            }
            else
            {
                label_[l] +=
                    "]\n";
            }
        }

        return label_;
    }

    public string AllLabel
    {
        get { return allLabel_; }
        private set
        {
            string[] l = Label();
            allLabel_ =
                "\"Label\": { \n ";
            for (int i = 0; i < l.Length; ++i)
            {
                allLabel_ += l[i];
            }

            allLabel_ +=
                "}";

        }
    }

    public string CreateBy
    {
        get { return createBy_; }
        private set
        {
            createBy_ = "\"Created By\": " + "\"" + value + "\"";
        }
    }

    public string ProjectName
    {
        get { return projectName_; }
        private set
        {
            projectName_ = "\"Project Name\": " + value;

        }

    }

    string allInfo_;
    public string AllInfo
    {
        get { return allInfo_; }
        private set
        {
            allInfo_ =                
                "{\n" +
                //ID + ",\n" +
                labeledData_ + ",\n" +
                AllLabel + "\n" +
                //CreateBy + ",\n" +
                //ProjectName + ",\n" +
                //CreateAt + ",\n" +
                //SecondsToLabel + ",\n" +
                //ExternalID + ",\n" +
                //Agreement + ",\n" +
                //DataSetName + ",\n" +
                //Review + ",\n" +
                //ViewLabel + ",\n" +
                "},"; //TODO enlever la virgule si cest le dernier et le remettre si on en rajoute

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="googleDrivePath"></param>
    /// <param name="labelName"></param>
    /// <param name="boxPoints">x, y, j = label</param>
    /// <param name="label"></param>
    /// <param name="createBy"></param>
    /// <param name="projectName"></param>
	public JSONData(string googleDrivePath, string labelName, string[,,] boxPoints//, 
        /*string createBy, string projectName*/)
    {        
        LabeledData = googleDrivePath;
        LabelName = labelName;
        BoxPoints = boxPoints;
        AllLabel = "";//il na pas besoin de valeur pcq va chercher ses valeurs internes
        AllInfo = ""; //il na pas besoin de valeur pcq va chercher ses valeurs internes
        //CreateBy = createBy;
        //ProjectName = projectName;

    }

}
