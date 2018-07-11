using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void action() {
		SoundManagerScript.PlaySound (SoundManagerScript.STARTING);
		StartCoroutine(LoadNextScene());


	}


	IEnumerator LoadNextScene(){
		yield return new WaitForSeconds(1.0f); // wait time

		GameManager manager = GameObject.FindObjectOfType<GameManager> ();

		SceneManager.LoadScene("Menu");

	}
}
