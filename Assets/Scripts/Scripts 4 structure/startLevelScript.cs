using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update(){

		if (Input.GetMouseButtonDown(0)) {
			Vector3 worldTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 v = new Vector2(worldTouch.x, worldTouch.y);
			RaycastHit2D hit = Physics2D.Raycast ( v, Vector2.zero, Mathf.Infinity);
			if (hit != null && hit.collider != null){
				//print ("hit");
				if (hit.collider.gameObject.tag == "startLevel") {
					SoundManagerScript.PlaySound (SoundManagerScript.STARTING);
					StartCoroutine(LoadKittID());
				} 
			} 
		} 
	}

	IEnumerator LoadKittID(){
		yield return new WaitForSeconds(1.0f); // wait time
		LevelData data = GameObject.FindObjectOfType<GameManager> ().getCurrentLevelData();
		SceneManager.LoadScene(data.sceneName);

	}
}
