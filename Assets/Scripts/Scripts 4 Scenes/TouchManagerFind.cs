using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchManagerFind : MonoBehaviour {

	public Text textUI;

	//GameObject gObj = null;
	Plane objPlane;
	//Vector3 mO;
	int count = 0;
	int maxBallsToFind;
	GameObject exit;

	void Start() {
		/*GameObject[] listBalls = GameObject.FindGameObjectsWithTag ("ball");
		foreach (GameObject go in listBalls) {
			go.transform.Rotate (new Vector3 (0, 0, Random.Range(0, 100)));
		}
		maxBallsToFind = listBalls.Length;*/
		print("touchscript started");
		//exit = GameObject.FindGameObjectsWithTag ("door")[0];
		//exit.transform.position = new Vector3(exit.transform.position.x, exit.transform.position.y, -10);
		//exit.GetComponent<Renderer>().enabled = false;
		//exit.SetActive(false);
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) // this gives you access to the back button
		{ 
			NextScene ();
		}


		bool aTouch = false;
		Vector3 pos;
		if (Application.platform != RuntimePlatform.Android)
		{
			// use the input stuff
			aTouch = Input.GetMouseButtonDown(0);
			pos = Input.mousePosition;
		} else {
			// use the android Stuff
			aTouch = (Input.touchCount > 0);
			pos = Input.GetTouch(0).position;
		}

		if (aTouch) {

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

			if (hit.collider != null) {
				GameObject hitObj = hit.collider.gameObject;
				if (hitObj.tag.Equals ("ball")) {
					if (hitObj.GetComponent<Find>().found == false) { 
						++count; //hit.collider.tag;
						//hitObj.SetActive (false);
						hitObj.GetComponent<Renderer>().material.color = Color.gray;
						hitObj.GetComponent<Find>().found = true; 
						if (count == maxBallsToFind) {
							textUI.text = "exit";
							//exit.transform.position = new Vector3(exit.transform.position.x, exit.transform.position.y, -1);
							//exit.GetComponent<Renderer> ().enabled = true;
							exit.SetActive(true);
							//Application.Quit();
						}
					}
				} 
				if (hitObj.tag.Equals ("door")) {
					textUI.text = "next level";
					NextScene ();
				}
			}

		} 
		//if (count > 0) {
		//textUI.text = "Ballons trouvés : " + count + " / " + maxBallsToFind; 
		//}


	}


	void NextScene()
	{
		// load the nextlevel
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
}
