using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManagerTuto : MonoBehaviour {

	//public Text tCount;

	GameObject gObj = null;
	Plane objPlane;
	Vector3 mO;

	// Update is called once per frame
	void Update () {

		// touch or click ?
		if (InputHelper.GetTouches().Count > 0) {
			// drag begining ?
			if (InputHelper.GetTouches()[0].phase == TouchPhase.Began) {
				Ray mouseRay = GenerateMouseRay (InputHelper.GetTouches()[0].position);
				RaycastHit2D hit;
				hit = Physics2D.Raycast (mouseRay.origin, Vector2.zero, Mathf.Infinity);

				if (hit != null && hit.transform != null && hit.transform.gameObject != null) {
					gObj = hit.transform.gameObject;
					//print ("hit : " + gObj);
					DragNDropData script = gObj.GetComponent<DragNDropData>();
					// if hit object is not dragable then verify if it is the exit opened door !
					if (script == null) {
						ManageExit scriptExit = gObj.GetComponent<ManageExit>();
						if (scriptExit != null)
							// let's go out
							scriptExit.Exit (); 
							
						gObj = null;
						return;
					}
					// touching an hiding object ?
					if (script.hidable) {
						script.showHiddenElements ();
						gObj = null;
						return;
					}
					// not touching a movable object ? => get away
					if (script.moveable == false) {
						gObj = null;
						return;
					}
					// it seems we touch a dragable object...
					// let's save its origin position
					gObj.GetComponent<DragNDropData> ().SaveOrigin ();

					// dragging stuff
					objPlane = new Plane (Camera.main.transform.forward * -1, gObj.transform.position);
					Ray mRay = Camera.main.ScreenPointToRay (InputHelper.GetTouches()[0].position);
					float rayDistance;
					RaycastHit2D hit2D = Physics2D.GetRayIntersection ( mRay );
					objPlane.Raycast (mRay, out rayDistance);
					mO = gObj.transform.position - mRay.GetPoint (rayDistance);
				}
			} else if (InputHelper.GetTouches()[0].phase == TouchPhase.Moved && gObj) {   // drag in progress ?
				Ray mRay = Camera.main.ScreenPointToRay (InputHelper.GetTouches()[0].position);
				float rayDistance;
				if (objPlane.Raycast (mRay, out rayDistance)) {
					gObj.transform.position = mRay.GetPoint (rayDistance) + mO;
				}
			} else if (InputHelper.GetTouches()[0].phase == TouchPhase.Ended && gObj) { // droping element ?

				gObj.GetComponent<DragNDropData> ().DeactivateColliders();

				gObj.GetComponent<DragNDropData> ().DeleteCopy();
				
				Ray mouseRay = GenerateMouseRay (InputHelper.GetTouches()[0].position);
				RaycastHit2D hit;
				GameObject drop;
				hit = Physics2D.Raycast (mouseRay.origin, Vector2.zero, Mathf.Infinity);

				gObj.GetComponent<DragNDropData> ().ActivateColliders();


				if (hit != null && hit.transform != null) {
					drop = hit.transform.gameObject;
					print ("drop on :" + drop);
					// dropped on a dropable object ?
					DropData script = drop.GetComponent<DropData> ();

					if (script != null) {
						// drop on a good solution object ?
						if (script.dropPossibleWith (gObj)) {
							//print ("possible drop !");
							gObj.GetComponent<DragNDropData> ().DropToPosition (drop);

							/*if (script.compatibleWith(gObj)) {
								GameObject.Find ("Setup").GetComponent<InitGameElements> ().maxBallsToFind--;
								if (GameObject.Find ("Setup").GetComponent<InitGameElements> ().maxBallsToFind == 0) {
								// no more elements to move => let's opening the door
									GameObject.Find ("Sortie").GetComponent<ManageExit> ().OnTurnOn ();
								}
							}*/
							gObj = null;
						} else {
							// no drop possible here !
							gObj.GetComponent<DragNDropData> ().ReturnOrigin ();
							gObj = null;
						}
					} else if (drop.GetComponent<DragNDropData> () != null && drop.GetComponent<DragNDropData> ().moveable
						&& drop.transform.parent.GetComponent<DropData>() != null && drop.transform.parent.GetComponent<DropData>().solutionArea) {
						print("SWAP with movable object");
						GameObject parent = gObj.transform.parent.gameObject;
						GameObject parent2 = drop.transform.parent.gameObject;

						gObj.transform.parent = null;
						drop.transform.parent = null;
						gObj.GetComponent<DragNDropData> ().DropToPosition (parent2);
						drop.GetComponent<DragNDropData> ().DropToPosition (parent);

						/*
						GameObject.Find ("Setup").GetComponent<InitGameElements> ().maxBallsToFind -= 2;
						if (GameObject.Find ("Setup").GetComponent<InitGameElements> ().maxBallsToFind < 1) {
							// no more elements to move => let's opening the door
							GameObject.Find ("Sortie").GetComponent<ManageExit> ().OnTurnOn ();
						}*/

						gObj = null;
					} else {
						// don't know what object is behind... better go back home !
						gObj.GetComponent<DragNDropData> ().ReturnOrigin ();
						gObj = null;
					}

				} else {
					gObj.GetComponent<DragNDropData> ().ReturnOrigin ();
					gObj = null;
				}
			}
		}

	}

	Ray GenerateMouseRay(Vector3 touchPos)
	{
		Vector3 mousePosFar = new Vector3 (touchPos.x, touchPos.y, Camera.main.farClipPlane);
		Vector3 mousePosNear = new Vector3 (touchPos.x, touchPos.y, Camera.main.nearClipPlane);
		Vector3 mousePosF = Camera.main.ScreenToWorldPoint (mousePosFar);
		Vector3 mousePosN = Camera.main.ScreenToWorldPoint (mousePosNear);
		Ray mr = new Ray (mousePosN, mousePosF - mousePosN);
		return mr;
	}

}