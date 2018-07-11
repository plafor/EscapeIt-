using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropData : MonoBehaviour {

	public bool dropable;
	public bool elementsDisappearedOnDrop;
	public string acceptedElements;
	public bool solutionArea;
	public bool refuseSameCategory;

	public bool compatibleWith(GameObject go) {
		return acceptedElements.Equals (go.GetComponent<DragNDropData> ().name) || acceptedElements.Equals (go.GetComponent<DragNDropData> ().category);
	}

	public bool dropPossibleWith(GameObject go) {
		//print ("Calculate if " + acceptedElements + " can accept " + go.GetComponent<DragNDropData> ().name);

		if (!dropable)
			return false;
		if (solutionArea) {
			if (refuseSameCategory) {
				return gameObject.transform.GetComponent<DropData> ().acceptedElements.Equals(go.GetComponent<DragNDropData> ().name) ;
			} else {
				return gameObject.transform.parent.transform.GetComponent<DropData> ().acceptedElements.Equals (go.GetComponent<DragNDropData> ().category);
			}
		}
		if (acceptedElements != go.GetComponent<DragNDropData> ().name && acceptedElements != go.GetComponent<DragNDropData> ().category)
			return false;
		if (elementsDisappearedOnDrop)
			return true;
		if (gameObject.transform.childCount == 0) 
			return true;
		else {
			//int nbPos = 0;

			for (int i = 0; i < gameObject.transform.childCount; i++) {
				Transform child = gameObject.transform.GetChild (i);
				if (child.GetComponent<DropData> () != null && child.transform.childCount == 0) 
					return true;  //false;
				// sinon le child n'est pas un movable mais un dropable sans fils alors OK !
				//print(child.name);
				if (child.name == "Particle System" && gameObject.transform.childCount == 1) 
						return true;
			}
			return false; //true;
		}

	}

	public Transform findFreePosition()
	{
		int nbChilds = gameObject.transform.childCount;
		if (nbChilds == 0) {
			return null;
		}
		ArrayList possiblePos = new ArrayList();
		Transform first = null;
		for (int i = 0; i < nbChilds; i++) {
			Transform child = gameObject.transform.GetChild (i);
			if (child.GetComponent<DropData> () != null) {
				if (first == null)
					first = child;
				if (child.childCount > 0) {
					possiblePos.Add (child);
				}
			}
		}
		if (possiblePos.Count > 0) {
			int choice = Random.Range (0, possiblePos.Count);
			return (Transform) possiblePos [choice];
		} else {
			return first;
		}
	}

	public void DeactivateColliders() {
		if (gameObject.GetComponent<BoxCollider2D> () != null)
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		if (gameObject.GetComponent<CircleCollider2D> () != null)
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		if (gameObject.GetComponent<PolygonCollider2D> () != null)
			gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
	}

	public void ActivateColliders() {
		if (gameObject.GetComponent<BoxCollider2D> () != null)
			gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		if (gameObject.GetComponent<CircleCollider2D> () != null)
			gameObject.GetComponent<CircleCollider2D> ().enabled = true;
		if (gameObject.GetComponent<PolygonCollider2D> () != null)
			gameObject.GetComponent<PolygonCollider2D> ().enabled = true;
	}

}
