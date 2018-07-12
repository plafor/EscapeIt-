using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWithListScenarios : MonoBehaviour {

	public void LoadSceneWithScenarios() {
		StartCoroutine(LoadScreen());

	}

	IEnumerator LoadScreen()
	{
		yield return true; //new WaitForSeconds(1.0f); // wait time
		SceneManager.LoadScene("ScenariosChoice");
	}
}
