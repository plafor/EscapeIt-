using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour {

	public void back() {
		if (GameObject.Find ("Chest") != null)
		{ 
			if (GameObject.Find ("Chest").GetComponent <ChestManager>().exit == false)
				return;
		}
		SceneManager.LoadScene("SceneReinforcerMario");
	}

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuScreen());
    }
    IEnumerator LoadMainMenuScreen()
    {
        yield return true; //new WaitForSeconds(1.0f); // wait time
        SceneManager.LoadScene("Menu");
    }
}
