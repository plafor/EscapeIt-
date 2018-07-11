using UnityEngine;
using System.Collections;

public class Guidance : MonoBehaviour {

	private static int FIRST_GUIDANCE = 7;
	public static float timeSinceLastFound;

	// Use this for initialization
	void Start () {
		timeSinceLastFound = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - timeSinceLastFound > FIRST_GUIDANCE ) {
			//print ("TIME's UP!");
			timeSinceLastFound = Time.time;
			GameObject go = GameObject.FindGameObjectWithTag ("movable");
			if (go == null)
				return;
			Behaviour be = (Behaviour)go.transform.parent.gameObject.GetComponent ("Halo");
			be.enabled = true;
			
		}
	}

	public static void reinitTime() {
		timeSinceLastFound = Time.time;
	}
}
