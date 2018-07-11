using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour {

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
					print ("touch sur " + gObj.name + " " + gObj.transform.tag);
					if (hit.transform.tag != "selection" && hit.transform.tag != "dino") {
						print ("on se casse");	
						gObj = null;
						return;
					}
					GameObject selection = GameObject.FindGameObjectWithTag ("selection");

					if (selection != null) {
						selection.tag = "dino";
						print ("change");
					}
					gObj.tag = "selection";
					objPlane = new Plane (Camera.main.transform.forward * -1, gObj.transform.position);
					Ray mRay = Camera.main.ScreenPointToRay (InputHelper.GetTouches()[0].position);
					float rayDistance;
					//RaycastHit2D hit2D = Physics2D.GetRayIntersection ( mRay );
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
				
				gObj = null;
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
