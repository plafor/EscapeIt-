using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoomin : MonoBehaviour {

	private bool zoomIn;
	private float scale = 1f;
	private bool active;
	private GameObject zoomAnim;

	// Use this for initialization
	void Start () {
		scale = 50f;
		active = false;
		zoomAnim = GameObject.Find ("ZoomAnim");
		zoomAnim.GetComponent<Image> ().enabled = false;
		activeZoomOut ();
	}
		
	public void activeZoomIn() {
		active = true; 
		scale = 50f;
		zoomIn = true;
		zoomAnim.transform.localScale = new Vector3 (scale, scale, scale);
		zoomAnim.GetComponent<Image> ().enabled = true;
	}

	public void activeZoomOut() {
		active = true; 
		zoomIn = false;
		scale = 1f;
		zoomAnim.transform.localScale = new Vector3 (scale, scale, scale);
		zoomAnim.GetComponent<Image> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (!active)
			return;
		if (!zoomIn) {
			if (scale < 50.0f) {
				scale += 0.7f;
				zoomAnim.transform.localScale = new Vector3 (scale, scale, scale);
			} else {
				active = false;
				zoomAnim.GetComponent<Image> ().enabled = false;
			}
		} else {
			if (scale > 1.0f) {
				scale -= 0.7f;
				zoomAnim.transform.localScale = new Vector3 (scale, scale, scale);
			} else {
			active = false;
			zoomAnim.GetComponent<Image> ().enabled = false;
		}
		}
	}
}
