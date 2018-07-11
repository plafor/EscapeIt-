using UnityEngine;
using System.Collections;

public class Find : MonoBehaviour {

	public bool found;

	// Use this for initialization
	void Start () {
		found = false;
		transform.Rotate (new Vector3 (0, 0, Random.Range(0, 100)));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
