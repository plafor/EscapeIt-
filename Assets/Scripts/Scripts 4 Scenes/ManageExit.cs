using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ManageExit : MonoBehaviour {


	void Start() {
		gameObject.GetComponent<Renderer>().enabled = false;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}
		
	public void Exit() {
		// disable collider to avoid several touch on the opened door
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		SoundManagerScript.PlaySound (SoundManagerScript.ENTERING);
		GameObject.FindObjectOfType<GameManager>().currentScene++;
		StartCoroutine(LoadNextScene());
	}

	IEnumerator LoadNextScene(){
		GameObject zoomManager = GameObject.Find ("ZoomManager");
		zoomManager.GetComponent<zoomin> ().activeZoomIn ();
		yield return new WaitForSeconds(1.1f); // wait time

		GameManager manager = GameObject.FindObjectOfType<GameManager>();

		if (manager.currentScene <= manager.maxScenes) {
            SceneManager.LoadScene("GuidanceScreen");
        }else {
			Destroy (manager);
			SceneManager.LoadScene ("LoaderGameManager");
		}
	}
		
	public void OnTurnOn() {
		StartCoroutine ("waitAndTurnOn");
	}

	IEnumerator waitAndTurnOn() {
		yield return new WaitForSeconds(1);
		SoundManagerScript.PlaySound (SoundManagerScript.OPENING);
		gameObject.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<BoxCollider2D>().enabled = true;
	}
}
