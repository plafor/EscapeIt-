using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour {

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuScreen());
        //SceneManager.LoadScene("Menu");
    }

    IEnumerator LoadMenuScreen()
    {
        yield return true; //new WaitForSeconds(1.0f); // wait time
        SceneManager.LoadScene("Menu");
    }
}
