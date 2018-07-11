using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizerSauzer : MonoBehaviour {

	private static float MAX = 4.0f;
	private static float MIN = 0.5f;

	public void bigger() {
		GameObject selection = GameObject.FindGameObjectWithTag ("selection");
		if (selection == null)
			return;
		Vector3 scale = selection.transform.localScale;
		if (scale.x >= MAX || scale.x <= -MAX)
			return;
		if (scale.x > 0)
			scale.x += 0.1f;
		else 
			scale.x -= 0.1f;
		if (scale.y > 0)
			scale.y += 0.1f;
		else 
			scale.y -= 0.1f;
		selection.transform.localScale = scale;
	}

	public void smaller() {
		GameObject selection = GameObject.FindGameObjectWithTag ("selection");
		if (selection == null)
			return;
		Vector3 scale = selection.transform.localScale;
		if (scale.x <= MIN && scale.x >= - MIN)
			return;
		if (scale.x > 0)
			scale.x -= 0.1f;
		else 
			scale.x += 0.1f;
		if (scale.y > 0)
			scale.y -= 0.1f;
		else 
			scale.y += 0.1f;
		selection.transform.localScale = scale;
	}

	public void flipperDolphin() {
		GameObject selection = GameObject.FindGameObjectWithTag ("selection");
		if (selection == null)
			return;
		Vector3 scale = selection.transform.localScale;
		scale.x *= -1;
		selection.transform.localScale = scale;
	}
}
