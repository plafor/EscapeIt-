using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageTouch : MonoBehaviour {

	[SerializeField]
	private GameObject selectedObject;    

	[SerializeField]
	Text DebugText;

	float zoomModifierSpeed = 0.1f;

	void start () {


	}


	void Update () {

		DebugText.text = selectedObject.name;

		if ( Input.touchCount == 0 )
		{
			Touch touch = Input.touches[0];
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit2D hit;
			hit = Physics2D.Raycast (ray.origin, Vector2.zero, Mathf.Infinity);
			DebugText.text = "touch";
			if ( hit != null )
			{
				selectedObject = hit.transform.gameObject;
				print ("find : " + selectedObject.name);
				DebugText.text = "touch " + selectedObject.name;
			}
		}
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			//float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			//float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			float touchesPrevPosDiff = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchesCurrPosDiff = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float zoomModifier = (touchZero.deltaPosition - touchOne.deltaPosition).magnitude * zoomModifierSpeed;

			//if (touchesPrevPosDiff > touchesCurrPosDiff)
			selectedObject.transform.localScale = new Vector3(zoomModifier , zoomModifier , zoomModifier);
		}
	}
}

