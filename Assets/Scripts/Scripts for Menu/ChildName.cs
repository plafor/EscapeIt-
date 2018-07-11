using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildName : MonoBehaviour {

    public Text childName;

	// Use this for initialization
	void Start () {
        Update();
	}
	
	// Update is called once per frame
	void Update () {
        childName.text = Authentication.currentProfile.GetName();
	}
}
