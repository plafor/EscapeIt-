using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelQuantitySelector : MonoBehaviour{
    private static int nbLvl;



    // Use this for initialization
    void Start () {
        nbLvl = 3;
	}
	
    public void SetNbLevel(int i)
    {
        nbLvl = i;
    }

    public void Validate()
    {
        GeneratorXMLCreator.Create(Authentication.currentProfile, nbLvl);
        LoadGeneratorWaiting();
        
    }

    public void LoadGeneratorWaiting()
    {
        StartCoroutine(LoadGeneratorWaitingScreen());
    }




    IEnumerator LoadGeneratorWaitingScreen()
    {
        yield return true; //new WaitForSeconds(1.0f); // wait time
        SceneManager.LoadScene("GeneratorWaiting");
    }

}
