using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LevelFileIO))]
public class LevelTemplateMaker : MonoBehaviour {

    public string filename = "";
    public string fileExt = ".lvl";
    private string filePath = "Assets/Resources/Levels/";
    public bool checkThisToCreateFile = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (checkThisToCreateFile) {
            checkThisToCreateFile = false;
            if (filename.Length == 0) {
                Debug.LogError("You might wanna name your level");
                return;
            }
            LevelFileIO io = GetComponent<LevelFileIO>();
            if (io != null) {
                Debug.Log("Creating level template");
                string fn = filePath + filename + fileExt;
                Debug.Log("file is " + fn);
                io.createNewTemplate(fn);
                Debug.Log("finished writing file (may take a second to appear)");
            }
        }

		
	}
}
