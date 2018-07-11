using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour {


	public void action() {
		//print ("button pressed");
		SoundManagerScript.PlaySound (SoundManagerScript.STARTING);
		StartCoroutine(LoadKittID());


	}




	IEnumerator LoadKittID(){
		yield return new WaitForSeconds(1.0f); // wait time
		//SceneManager.LoadScene("ScenariosChoice");
        SceneManager.LoadScene("ScenarioQuantityChoice");

    }
}
