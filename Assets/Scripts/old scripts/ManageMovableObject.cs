using UnityEngine;
using System.Collections;



public class ManageMovableObject : MonoBehaviour {

	private static ManageMovableObject currentMovedObject = null;

	private Vector3 initialPos;
	private Vector2 initialDragPos;
	private Vector3 saveScale;

	private bool found; // trouvé ?
	private bool beingMoved;  // drag'n drop en cours ?

	public void InitObject()
	{
		print ("    INIT OBJET  " + tag);
		found = false;
		beingMoved = false;

		gameObject.SetActive (true);

	/*	if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += HandleTouchesBegan; // When click
			TouchManager.Instance.TouchesMoved += HandleTouchesMoved; // When move the touch
			TouchManager.Instance.TouchesEnded += HandleTouchesEnded; // When end the touch
		} */
	}

	string categoryTag () {
		Transform o = transform.Find ("category");
		if (o != null) {
			return o.tag;
		} else return "";
	}

	string tag4ObjectWithName (GameObject go, string name) {
		Transform o = go.transform.Find (name);
		if (o != null) {
			return o.tag;
		} else return "";
	}

	bool isVisible() {
		return this.transform.GetComponent<SpriteRenderer> ().enabled;
	}
	/*
	void HandleTouchesBegan (object sender, TouchEventArgs e)
	{
		if (currentMovedObject != null && currentMovedObject !=this)
			return;



		print ("obj = " + this.transform);
		print (TouchManager.Instance.GetHitTarget(e.Touches[0].Position) == this.transform);
		if( TouchManager.Instance.GetHitTarget(e.Touches[0].Position) == this.transform && isVisible()) //If the touch is in this
		{
		
			currentMovedObject = this;
			initialDragPos = e.Touches [0].Position;
			print ("current = " + this.gameObject);
			this.transform.SetAsLastSibling ();

			//GetComponent<Animator>().SetTrigger("moved");
			beingMoved = true;
			initialPos = transform.position;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.gray;
			gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "DragLayer";	
			saveScale = gameObject.transform.localScale;
			gameObject.transform.localScale = new Vector3(0.45f, 0.45f, 1);

			/*if (categoryTag() == "medicineball") {
				removeTouchEventsOnOtherMedicineBalls ();
			}*/
	/*	}
		//Guidance.reinitTime ();
	}*/
/*
	void HandleTouchesEnded (object sender, TouchEventArgs e)
	{
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "SelectionableObjects";

		// qui est concerné ?
		Transform touched = TouchManager.Instance.GetHitTarget (e.Touches [0].Position);

		if (touched == this.transform) { //If the touch is in this

			// priorité à l'élément qui a deja commencé un drag'n dop
			if (currentMovedObject != null && currentMovedObject != this) {
				//print ("ciao");
				if (currentMovedObject != null) {
					print ("===> transfert au current objet dragged");
					currentMovedObject.HandleTouchesEndedTransfered (e);
				}
				return;
			}

			beingMoved = false;

			Collider2D sol = GoodPlaceBelow2 (e.Touches [0].Position);

			if (sol != null) { // un placement de solution en dessous ?
				print("solution en dessous (sans transfert) => " + sol.gameObject);
				ManageMoving(sol, e.Touches [0].Position);
			}
			else {
				
				goBack ();

			} 
			currentMovedObject = null;
		}
		//Guidance.reinitTime ();


	}*/
/*
	void HandleTouchesEndedTransfered (TouchEventArgs e)
	{
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "SelectionableObjects";

			beingMoved = false;

			Collider2D sol = GoodPlaceBelow2 (e.Touches [0].Position);

			if (sol != null) { // un placement de solution en dessous ?
				print("solution en dessous => " + sol.gameObject);
				ManageMoving(sol, e.Touches [0].Position);
			}
			else {
				goBack ();

			} 
			currentMovedObject = null;
	}
*/
	void ManageMoving(Collider2D sol, Vector2 initPos) {
		//print ("sol");
		sol.GetComponent<SpriteRenderer> ().color = Color.white;
		string tag4solution = sol.gameObject.transform.Find("target").gameObject.tag; 
		string categorySolution = tag4ObjectWithName (sol.gameObject, "category"); 

		string elementTag = "";
		switch (categorySolution) {
		case "association":
			elementTag = this.tag;
			break;
		case "classement":
			elementTag = this.categoryTag ();
			break;
		case "seriation":
			elementTag = this.categoryTag ();
			break;
		}
		if (categorySolution != "seriation") {
			gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "SelectionableObjects";
			if (tag4solution == elementTag) {
				ParticleSystem ps = sol.transform.Find ("Particle System").gameObject.GetComponent <ParticleSystem> ();
				ParticleSystem.EmissionModule em = ps.emission;
				em.enabled = true;
				if (ps.isPlaying)
					ps.Stop ();
				ps.Play ();

				SoundManagerScript.PlaySound (SoundManagerScript.SUCCESS);
		/*		TouchManager.Instance.TouchesBegan -= this.HandleTouchesBegan; 
				TouchManager.Instance.TouchesMoved -= this.HandleTouchesMoved;
				TouchManager.Instance.TouchesEnded -= this.HandleTouchesEnded;
        */
				// selon solution
				if (categorySolution == "classement") {
					//this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;//
					this.gameObject.SetActive (false);
				} else if (categorySolution == "association") {
					this.transform.position = new Vector3 (sol.transform.position.x + Random.Range (-0.15f, 0.15f), sol.transform.position.y + Random.Range (0.0f, 0.2f), sol.transform.position.z);
					GetComponent<SpriteRenderer> ().color = Color.white;
					gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "DragLayer";
				}

				AddStar ();
			} else {
				
				goBack ();
			}
		} else { // cas de la seriation
			
			// si pas d'autres balles deja en dessous !
			GameObject elemBelow = ballAlreadyPlaced (sol.gameObject.transform.position);
			if (elemBelow == null) {
				//print ("personne là");
				this.transform.position = new Vector3 (sol.transform.position.x, sol.transform.position.y + 0.8f, sol.transform.position.z); //+ (1.2f * this.GetComponent<CircleCollider2D>().radius), sol.transform.position.z);
				GetComponent<SpriteRenderer> ().color = Color.white;
				gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "place4B25elements";

				if (tag == sol.gameObject.tag) {  // good place
					print ("bien placé");
					ParticleSystem ps = sol.transform.Find ("Particle System").gameObject.GetComponent <ParticleSystem> ();
					ParticleSystem.EmissionModule em = ps.emission;
					em.enabled = true;
					if (ps.isPlaying)
						ps.Stop ();
					ps.Play ();
					SoundManagerScript.PlaySound (SoundManagerScript.SUCCESS);
		/*			TouchManager.Instance.TouchesBegan -= this.HandleTouchesBegan; 
					TouchManager.Instance.TouchesMoved -= this.HandleTouchesMoved;
					TouchManager.Instance.TouchesEnded -= this.HandleTouchesEnded;
	*/				GetComponent<Collider2D> ().enabled = false;
					AddStar ();
					StartCoroutine(DesactivateSol(sol));

				}
			} else {// deja qqun
				print ("switch entre "+ this.name +" et "+elemBelow.transform.parent.name);
				Vector3 pos = elemBelow.transform.parent.transform.position;
				Vector3 scale = elemBelow.transform.parent.transform.localScale;
				//Vector3 posIni = elemBelow.transform.parent.GetComponent<ManageMovableObject> ().initialPos;
				elemBelow.transform.parent.transform.position = this.initialPos;
				elemBelow.transform.parent.GetComponent<ManageMovableObject>().initialPos = this.initialPos;
				elemBelow.transform.parent.transform.localScale = this.saveScale;
				this.transform.position = pos;
				this.transform.localScale = scale;
				initialPos = pos;
				this.GetComponent<SpriteRenderer> ().color = Color.white;
				elemBelow.transform.parent.GetComponent<SpriteRenderer> ().color = Color.white;

				// et s'ils tombent sur un bon emplacement ?
				verifObjetInSolutionPlace(this.gameObject, initPos);
				verifObjetInSolutionPlace(elemBelow.transform.parent.gameObject, initialDragPos);
			}
		}
	}

	void AddStar() {
		GameObject[] emptyStars = GameObject.FindGameObjectsWithTag ("emptyStar");
		if (emptyStars.Length > 0) {
			System.Array.Sort (emptyStars, CompareObNames);
			GameObject firstEmptyStar = emptyStars [0];
			//Sprite spr = Resources.Load<Sprite> ("Sprites/star");
			//firstEmptyStar.transform.GetComponent<SpriteRenderer> ().sprite = spr;
			firstEmptyStar.GetComponent<Animator> ().SetTrigger ("fill");
			firstEmptyStar.tag = "star";
			if (emptyStars.Length == 1) { // fin

				GameObject exit = GameObject.FindGameObjectWithTag ("door");
				exit.GetComponent<ManageExit> ().OnTurnOn ();
			}
		}
	}

	void verifObjetInSolutionPlace(GameObject go, Vector2 pos) {
		//print("z="+go.transform.position.z);
		Collider2D sol = GoodPlaceBelow2 (pos);
		if (sol == null) {
			print ("PAS DE SOL EN DESSOUS DE " + go);
			return;
		}
		print ("verif sol = " + sol.gameObject + " avec tag = " + sol.gameObject.tag + " alors que tag objet = " + go.tag);
		if (go.tag == sol.gameObject.tag) {  // good place
			ParticleSystem ps = sol.transform.Find ("Particle System").gameObject.GetComponent <ParticleSystem> ();
			ParticleSystem.EmissionModule em = ps.emission;
			em.enabled = true;
			if (ps.isPlaying)
				ps.Stop ();
			ps.Play ();
			SoundManagerScript.PlaySound (SoundManagerScript.SUCCESS);
	/*		TouchManager.Instance.TouchesBegan -= go.gameObject.GetComponent<ManageMovableObject> ().HandleTouchesBegan; 
			TouchManager.Instance.TouchesMoved -= go.gameObject.GetComponent<ManageMovableObject> ().HandleTouchesMoved;
			TouchManager.Instance.TouchesEnded -= go.gameObject.GetComponent<ManageMovableObject> ().HandleTouchesEnded;
	*/		go.gameObject.GetComponent<Collider2D> ().enabled = false;
			AddStar ();
			StartCoroutine(DesactivateSol(sol));

		}
	}

	public void desactivateObject() {
/*		TouchManager.Instance.TouchesBegan -= GetComponent<ManageMovableObject> ().HandleTouchesBegan; 
		TouchManager.Instance.TouchesMoved -= GetComponent<ManageMovableObject> ().HandleTouchesMoved;
		TouchManager.Instance.TouchesEnded -= GetComponent<ManageMovableObject> ().HandleTouchesEnded;
*/		GetComponent<Collider2D> ().enabled = false;
	}

	IEnumerator DesactivateSol(Collider2D sol){
		yield return new WaitForSeconds(2.0f); // wait time
		sol.gameObject.SetActive (false);
	}

	public void removeTouchManager() {
		//print ("NETTOYAGE OBJET");
	/*	TouchManager.Instance.TouchesBegan -= this.HandleTouchesBegan; 
		TouchManager.Instance.TouchesMoved -= this.HandleTouchesMoved;
		TouchManager.Instance.TouchesEnded -= this.HandleTouchesEnded;
	*/}

	GameObject ballAlreadyPlaced(Vector3 pos) {
		GameObject[] list = GameObject.FindGameObjectsWithTag ("movable");
		foreach (GameObject go in list) {
			if (go.transform.parent.gameObject != this.gameObject) {
				Vector3 v = new Vector3 (go.transform.parent.transform.position.x, go.transform.parent.transform.position.y - 0.8f, go.transform.parent.transform.position.z);
				if (v == pos)
					return go;
			}
		}
		return null;
	}

	void goBack() {
		//print ("back");
		SoundManagerScript.PlaySound (SoundManagerScript.FAILURE);
		transform.position = initialPos;			
		GetComponent<SpriteRenderer> ().color = Color.white;
		gameObject.transform.localScale = saveScale;
		found = true;
		gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "SelectionableObjects";
	}
		
	int CompareObNames( GameObject x, GameObject y )
	{
		return x.name.CompareTo( y.name );
	}

//	void HandleTouchesMoved (object sender, TouchEventArgs e)
//	{
		//Guidance.reinitTime ();

		/*if (TouchManager.Instance.GetHitTarget (e.Touches [0].Position) == this.transform) { //If the touch is in this
			//gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "SelectionableObjects";	
			Collider2D sol = GoodPlaceBelow (e.Touches [0].Position);

			if (sol != null) {
				sol.GetComponent<SpriteRenderer> ().color = Color.gray;
			}
		}*/
//	}

	Collider2D GoodPlaceBelow(Vector2 pos){
		RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
		foreach (RaycastHit2D go in hits) {
			//print ("" + go.collider.tag);
			if (go.collider.tag == "solution")
				return go.collider;
		}
		return null;
	}

	Collider2D GoodPlaceBelow2(Vector2 pos){
		RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
		foreach (RaycastHit2D go in hits) {
			//print ("" + go.collider.tag);
			if (go.collider.tag == "solution" || go.collider.gameObject.transform.parent.gameObject.tag == "solution")
				return go.collider;
		}
		return null;
	}
}
