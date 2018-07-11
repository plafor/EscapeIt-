using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManageHidingObjects : MonoBehaviour {

	private bool found; // trouvé ?

	// Use this for initialization
	public void InitHidingObject () {
		found = false;

		gameObject.SetActive (true);

		/*if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += HandleTouchesMeBegan; // When start the touch
			TouchManager.Instance.TouchesEnded += HandleTouchesMeEnded; // When end the touch
			print("init otuch");
		}*/

	}
	/*
	void HandleTouchesMeBegan (object sender, TouchEventArgs e)
	{
		//print ("rentré  " + sender);
		if (TouchManager.Instance.GetHitTarget (e.Touches [0].Position) == this.transform) { 
			//print ("mais pas ici");
			transform.GetComponent<SpriteRenderer> ().color = Color.gray;
			if (!found) {
				SoundManagerScript.PlaySound (SoundManagerScript.ZIP);
			}
		}
		//Guidance.reinitTime ();
	}*/
	/*
	void HandleTouchesMeEnded (object sender, TouchEventArgs e)
	{
		transform.GetComponent<SpriteRenderer> ().color = Color.white;
		if (TouchManager.Instance.GetHitTarget (e.Touches [0].Position) == this.transform) { 
			if (!found) {
				found = true;
				StartCoroutine( activeObjectIfPresent ());
			}

		//Guidance.reinitTime ();
		}
	}
	*/

/*	public void removeTouchManager() {
		print ("NETTOYAGE SAC");
		TouchManager.Instance.TouchesEnded -= this.HandleTouchesMeEnded;
		TouchManager.Instance.TouchesBegan -= this.HandleTouchesMeBegan;
	}
*/
	IEnumerator activeObjectIfPresent() {
		yield return new WaitForSeconds(1);
		//Transform[] listMovableElements = GameObject.Find("ElementsSelectionables").GetComponentsInChildren<Transform>(true);
		GameObject[] listMovableElements = GameObject.FindGameObjectsWithTag ("movable");
		print ("CHERCHE");
		foreach (GameObject go in listMovableElements) {
			if (go.transform.parent.transform.position == transform.Find ("Place4Objects").transform.position && !go.transform.parent.GetComponent<SpriteRenderer> ().enabled) {
				go.transform.parent.GetComponent<SpriteRenderer> ().enabled = true;
				go.transform.parent.GetComponent<CircleCollider2D> ().enabled = true;
				//if (!go.parent.gameObject.active) {
				print ("ON ACTIVE");
				//go.transform.parent.gameObject.SetActive (true);
				//go.transform.parent.gameObject.transform.gameObject.GetComponent<ManageMovableObject> ().InitObject ();
				yield break;
				//}
			}
		}
	}

}
