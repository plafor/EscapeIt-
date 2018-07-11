using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonProfile : MonoBehaviour {

	public void LoadProfileScene() {
		StartCoroutine(LoadProfileScreen());
	}

	IEnumerator LoadProfileScreen(){
		yield return true; //new WaitForSeconds(1.0f); // wait time
		//SceneManager.LoadScene("ProfileScreen");
        SceneManager.LoadScene("ProfileSettings");
    }

	public void LoadMenu() {
		StartCoroutine(LoadMenuScreen());
	}

	IEnumerator LoadMenuScreen(){
		yield return true; //new WaitForSeconds(1.0f); // wait time
		SceneManager.LoadScene("Menu");
	}

}
