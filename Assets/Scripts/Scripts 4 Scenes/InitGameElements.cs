using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class InitGameElements : MonoBehaviour
{

    //private string bx;

    public int maxBallsToFind;   // max elems to find => will be read by other scripts so it's public !
    private GameObject exit;      // the "door" gameobject

    // will be run at every reload of the scene
    void Awake()
    {

        //GameObject.FindObjectOfType<GameManager> ().LoadMyXML ();

        LoadElementsFromLevelData();

        print("nb elements to find => " + maxBallsToFind);

        ManageFeedbacks();

    }

    void LoadElementsFromLevelData()
    {

        // end ?
        if (GameObject.FindObjectOfType<GameManager>().maxScenes <= GameObject.FindObjectOfType<GameManager>().currentScene)
        {
            GameObject.FindObjectOfType<GameManager>().currentScene = 0;
        }

        maxBallsToFind = GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().nbObjectsToFind;

        foreach (SolutionObject e in GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().solutionObjects)
        {
            initSolutionObject(e);
        }

        foreach (PlacedDecor p in GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().placedDecors)
        {
            initDecorObject(p);
        }

        foreach (Hideout h in GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().hideouts)
        {
            initHideoutObject(h);
        }

        foreach (Element e in GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().elements)
        {
            initElementInPos(e);
        }
    }

    GameObject initElementInPos(Element e)
    {


        GameObject elem = InstantiatePrefabWithName(e.name);
        elem.tag = "selection";
        moveObjectToPlaceWithName(elem, e.pos);

        return elem;
    }

    void initSolutionObject(SolutionObject e)
    {

        GameObject position = GameObject.Find(e.pos);

        GameObject elem = InstantiatePrefabWithName(e.name);

        elem.transform.position = position.transform.position;

        GameObject newParent = GameObject.Find("SolutionObjects");
        elem.transform.parent = newParent.transform;

        if (elem.GetComponent<DropData>() != null)
        {
            elem.GetComponent<DropData>().acceptedElements = e.acceptedElements;
        }

        /*if (elem.transform.Find ("target") != null) {
			GameObject targetObj = elem.transform.Find ("target").gameObject;
			targetObj.GetComponent<SpriteRenderer> ().
		}*/

        // B3 ?
        if (e.requiredObject())
        {// && elem.transform.Find ("object2find") != null) {
            print("required object");
            GameObject object2find = InstantiatePrefabWithName(e.acceptedElements);
            object2find.name = "similarObjectToFind";
            object2find.transform.SetParent(elem.transform);
            Transform location = elem.GetComponent<DropData>().findFreePosition();
            if (location != null)
            {
                moveObjectToPlaceWithName(object2find, location.name);
                //object2find.transform.position = location.position;
                object2find.GetComponent<DragNDropData>().moveable = false;
                //string layer = "solutionPlace";
                //if (location.gameObject.GetComponent<SpriteRenderer> () != null) {
                //	layer = location.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName;
                //} 
                //object2find.GetComponent<SpriteRenderer> ().sortingLayerName = location.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName;
                object2find.GetComponent<DragNDropData>().DeactivateColliders();
            }
        }
        // B4 ?
        if (e.requiredImage() && elem.transform.Find("image") != null)
        {
            print("Target Image of the solution element needs to be updated");

            GameObject target = elem.transform.Find("image").gameObject;
            if (target != null)
            {
                LevelData data = GameObject.FindObjectOfType<GameManager>().getCurrentLevelData();
                target.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + data.sceneName + "/" + e.acceptedElements + "Image");
                print("Target image updated !");
            }
            else
            {
                print("pb when updating the target image");
            }

        }
        else if (elem.transform.Find("image") != null)
        {
            // hide target Image
            GameObject target = elem.transform.Find("image").gameObject;
            if (target != null)
            {
                target.SetActive(false);
            }
        }

        foreach (Element element in e.elements)
        {
            GameObject elem2 = initElementInPos(element);
            // solution objects already placed must not be moved
            elem2.GetComponent<DragNDropData>().moveable = false;
        }

        string difficulty = GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().difficulty;
        if (!difficulty.Equals("EXPERT") && !difficulty.Equals("ADVANCED"))
            GameObject.Find("FeedbacksManager").GetComponent<FeedbacksManager>().DisplayElem2Find(e, elem);

        foreach (SolutionArea solArea in e.solutionAreas)
        {
            //print ("we look for " + solArea.pos);
            Transform area = elem.transform.Find(solArea.pos);
            //print (area.gameObject);
            area.gameObject.GetComponent<DropData>().acceptedElements = solArea.acceptedElement;
            area.gameObject.GetComponent<DropData>().dropable = true;
            area.gameObject.GetComponent<DropData>().solutionArea = true;
        }
        //target.tag = e.acceptedElements;
        //elem.transform.Find ("target").gameObject.tag = e.acceptedElements;
    }

    void initDecorObject(PlacedDecor p)
    {

        GameObject elem = InstantiatePrefabWithName(p.name);

        GameObject newParent = GameObject.Find("AdditionalElements");
        elem.transform.SetParent(newParent.transform);

        foreach (Element element in p.elements)
        {
            initElementInPos(element);
        }

        foreach (Hideout hideout in p.hideouts)
        {
            hideout.pos = p.name;
            initHideoutObjectInDecor(hideout, elem);
        }
    }

    void initHideoutObjectInDecor(Hideout h, GameObject parent)
    {
        GameObject elem = InstantiatePrefabWithName(h.name);
        moveObjectToPlaceWithName(elem, h.pos);
        // specific case of hidout within a decor element => set parent
        elem.transform.SetParent(parent.transform);

        foreach (Element element in h.hiddenElements)
        {
            initElementInHiddedPos(element, elem);
        }

    }

    void initHideoutObject(Hideout h)
    {

        GameObject elem = InstantiatePrefabWithName(h.name);

        moveObjectToPlaceWithName(elem, h.pos);

        // change layer of subplaces with the new layer of the hideout that just moved
        string newLayer = elem.transform.GetComponent<SpriteRenderer>().sortingLayerName;
        int order = elem.transform.GetComponent<SpriteRenderer>().sortingOrder;
        for (int i = 0; i < elem.transform.childCount; i++)
        {
            Transform child = elem.transform.GetChild(i);
            child.GetComponent<SpriteRenderer>().sortingLayerName = newLayer;
            child.GetComponent<SpriteRenderer>().sortingOrder = order;
        }

        foreach (Element element in h.hiddenElements)
        {
            initElementInHiddedPos(element, elem);
        }
    }

    void initElementInHiddedPos(Element e, GameObject parent)
    {

        GameObject elem = InstantiatePrefabWithName(e.name);
        moveObjectToPlaceWithNameUnderParentObject(elem, e.pos, parent);

        elem.SetActive(false);
    }

    void ManageFeedbacks()
    {
        GameObject star = GameObject.FindGameObjectWithTag("emptyStar");
        star.name = "StarEmpty" + maxBallsToFind;
        Vector3 posFirstStar = star.transform.position;
        float width = star.GetComponent<BoxCollider2D>().bounds.size.x;
        print("" + width);
        string path = "Prefabs/" + GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().sceneName + "/";

        for (int j = 1; j < maxBallsToFind; j++)
        {
            GameObject newStar = Instantiate(Resources.Load(path + "StarEmpty")) as GameObject;
            newStar.transform.position = new Vector3(posFirstStar.x - (width * j), posFirstStar.y, 1);
            newStar.transform.parent = GameObject.Find("ProgressionStars").transform;
            newStar.name = "StarEmpty" + (maxBallsToFind - j);
        }
    }

    private GameObject InstantiatePrefabWithName(string name)
    {
        string path = "Prefabs/" + GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().sceneName + "/";
        print(name);
        GameObject elem = Instantiate(Resources.Load(path + name)) as GameObject;
        return elem;
    }

    private void moveObjectToPlaceWithName(GameObject elem, string posName)
    {
        //print ("Element " + e.name + " in [" + e.pos + "]");

        GameObject position = GameObject.Find(posName);
        print("position 2 found " + posName);
        moveObjectToPlaceWithGameObject(elem, position);
    }

    private void moveObjectToPlaceWithGameObject(GameObject elem, GameObject position)
    {
        if (position == null)
        {
            print("Pos NOT found");
            return;
        }
        elem.transform.position = position.transform.position;

        // ONLY for the GYMNASIUM
        if (position.GetComponent<Variables>() != null)
        {
            int prctScale = position.GetComponent<Variables>().prctDisplay;
            elem.transform.localScale = new Vector3(1 * prctScale / 100.0f, 1 * prctScale / 100.0f, 1);
        }
        if (position.GetComponent<SpriteRenderer>() != null)
        {
            elem.GetComponent<SpriteRenderer>().sortingLayerName = position.GetComponent<SpriteRenderer>().sortingLayerName;
            elem.GetComponent<SpriteRenderer>().sortingOrder = position.GetComponent<SpriteRenderer>().sortingOrder;
        }
        else
        {
            elem.GetComponent<SpriteRenderer>().sortingLayerName = "movableObjects";
        }
        elem.transform.SetParent(position.transform);
    }

    private void moveObjectToPlaceWithNameUnderParentObject(GameObject elem, string posName, GameObject parent)
    {
        print("Element " + elem.name + " in [" + posName + "]");
        GameObject position = parent.transform.Find(posName).gameObject;

        moveObjectToPlaceWithGameObject(elem, position);
        //position.GetComponent<SpriteRenderer> ().sortingLayerName = parent.GetComponent<SpriteRenderer> ().sortingLayerName;
        elem.GetComponent<SpriteRenderer>().sortingLayerName = position.GetComponent<SpriteRenderer>().sortingLayerName;
        elem.GetComponent<SpriteRenderer>().sortingOrder = position.GetComponent<SpriteRenderer>().sortingOrder;
    }

    /// <summary>
    /// TO DELETE
    /// </summary>



    /*
	void ManageObjectsAndPositions() {
		if (bx != "B25") {// pas seriation
			GameObject[] listBalls4seriation = GameObject.FindGameObjectsWithTag ("medicineball");
			foreach (GameObject go in listBalls4seriation) {
				go.transform.parent.gameObject.SetActive (false);
			}
		} else {
			GameObject[] listElements = GameObject.FindGameObjectsWithTag ("movable");
			foreach (GameObject g in listElements) {
				Transform category = g.transform.parent.Find ("category");
				if (category != null && category.gameObject.tag != "medicineball") {
					print (">> cache elem " + g.transform.parent.gameObject.tag);
					g.transform.parent.gameObject.SetActive (false);
				}
			}

		}

		GameObject[] listMovableElements = GameObject.FindGameObjectsWithTag ("movable");
		// get parents
		GameObject[] listBalls = new GameObject[listMovableElements.Length];
		int val = 0;
		foreach (GameObject go in listMovableElements) {
			listBalls [val++] = go.transform.parent.gameObject;
		}
		//maxBallsToFind = listBalls.Length;

		GameObject[] listPlaces = GameObject.FindGameObjectsWithTag ("place");

		foreach (GameObject go in listBalls) {

			GameObject place;
			do {
				int placeNb = Random.Range (0, listPlaces.Length);
				place = listPlaces [placeNb];
			} while(place.GetComponent<Variables> ().used == true);

			place.GetComponent<Variables> ().used = true;
			int prctScale = place.GetComponent<Variables> ().prctDisplay;
			//print ("scale=" + prctScale);
			//GameObject ball = listBalls[0];
			//ball.transform.position = new Vector3(Random.Range (0f, Screen.width-100), (Screen.height / 2.0f) + Random.Range (0f, Screen.height / 2.0f), 0);
			//ball.transform.position = new Vector3(Random.Range (-5.0f, 5.0f), Random.Range (-0.4f, -2.84f), 0);
			go.transform.position = place.transform.position;
			go.transform.localScale = new Vector3(1*prctScale/100.0f, 1*prctScale/100.0f, 1);
			go.GetComponent<ManageMovableObject> ().InitObject ();
			if (place.transform.parent.tag == "hideplace") {
				go.GetComponent<SpriteRenderer> ().enabled = false;
				go.GetComponent<CircleCollider2D> ().enabled = false;
				print("CACHE");
				//go.SetActive (false);
			} 
		}
	}


	void ManageDecor() {
		// choix de 0, 1 ou X objets décoratifs supplémentaires
		GameObject[] listDecoElements = GameObject.FindGameObjectsWithTag ("deco");
		int numDecoSupp = Random.Range (0, listDecoElements.Length+1);
		int numDeco;
		for (int j = 0; j < numDecoSupp; j++) {
			do {
				numDeco = Random.Range (0, listDecoElements.Length);
			} while (listDecoElements [numDeco].activeSelf == false);
			listDecoElements [numDeco].SetActive(false);
		}
	}

	void ManageHidenPlace() {
		// choix de 0, 1 ou X objets décoratifs supplémentaires
		GameObject[] listHidenElements = GameObject.FindGameObjectsWithTag ("hideplace");
		int numDecoSupp = Random.Range (0, listHidenElements.Length+1);
		int numDeco;

		// choose some hidden places
		for (int j = 0; j < numDecoSupp; j++) {
			do {
				numDeco = Random.Range (0, listHidenElements.Length);
			} while (listHidenElements [numDeco].activeSelf == false);
			listHidenElements [numDeco].SetActive(false);
		}

		// randomly position them
		listHidenElements = GameObject.FindGameObjectsWithTag ("hideplace");
		GameObject[] listPlaces = GameObject.FindGameObjectsWithTag ("place");
		foreach (GameObject go in listHidenElements) {
			GameObject place;
			do {
				int placeNb = Random.Range (0, listPlaces.Length);
				place = listPlaces [placeNb];
			} while(place.transform.parent.tag == "hideplace" || place.GetComponent<Variables> ().used == true);

			place.GetComponent<Variables> ().used = true;
			int prctScale = place.GetComponent<Variables> ().prctDisplay;
			go.transform.position = place.transform.position;
			go.transform.localScale = new Vector3(1*prctScale/100.0f, 1*prctScale/100.0f, 1);
			go.GetComponent<ManageHidingObjects> ().InitHidingObject ();

			go.transform.Find ("Place4Objects").GetComponent<Variables> ().prctDisplay = prctScale;
		}

		// randomly change their sprite
		foreach (GameObject go in listHidenElements) {
			int spriteNb = Random.Range (0, 3);
			Sprite spr = null;
			switch (spriteNb) {
			case 0: 
				spr = Resources.Load<Sprite> ("Sprites/bags/sac1");
				break;
			case 1: 
				spr = Resources.Load<Sprite> ("Sprites/bags/sac2");
				break;
			case 2: 
				spr = Resources.Load<Sprite> ("Sprites/bags/sac3");
				break;
			}
			go.transform.GetComponent<SpriteRenderer> ().sprite = spr;
		}
	}



	string[] tabSpriteAssoNames = {"ballon basket", "ballon tennis", "ballon foot"};
	string[] tabSpriteImageAssoNames = {"imageballonbasket", "imageballetennis", "imageballonfoot"};
	string[] tabTagAssoNames = {"basketball", "tennisball", "football"};

	void ManageAssociationObjet() {
		//desactivate other solution zones
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("classement");
		foreach (GameObject go in gos) {
			GameObject soluce = go.transform.parent.gameObject;
			soluce.SetActive(false);
		}
		GameObject[] goseria = GameObject.FindGameObjectsWithTag ("seriation");
		foreach (GameObject go in goseria) {
			GameObject soluce = go.transform.parent.gameObject;
			soluce.SetActive(false);
		}

		//varier l'objet nécessaire
		GameObject sol = GameObject.FindGameObjectWithTag ("association");
		GameObject soluceGo = sol.transform.parent.gameObject;
		GameObject image = soluceGo.transform.Find ("image").gameObject;
		image.SetActive (false);
		GameObject target = soluceGo.transform.Find ("target").gameObject;
		// change target
		int nb = Random.Range (0, 3);
		target.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/" + tabSpriteAssoNames[nb]);
		target.tag = tabTagAssoNames[nb];


		//s'assurer d'avoir un objet
		GameObject[] listTargetElements = GameObject.FindGameObjectsWithTag (target.tag);
		print ("il y a " + listTargetElements.Length + " elements de type " + target.tag);
		if (listTargetElements.Length == 1) { // only the solution place ?
			print("on ajoute un element de type " + target.tag);
			GameObject[] listMovableElements = GameObject.FindGameObjectsWithTag ("movable");
			GameObject elemToChange = listMovableElements[0].transform.parent.gameObject;
			elemToChange.tag = target.tag;
			elemToChange.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/" + tabSpriteAssoNames[nb]);
		}
		maxBallsToFind = listTargetElements.Length - 1;
	}

	void ManageAssociationImage() {
		//desactivate other solution zones
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("classement");
		foreach (GameObject go in gos) {
			GameObject soluce = go.transform.parent.gameObject;
			//soluce.GetComponent<SpriteRenderer> ().enabled = false;
			soluce.SetActive(false);
		}
		GameObject[] goseria = GameObject.FindGameObjectsWithTag ("seriation");
		foreach (GameObject go in goseria) {
			GameObject soluce = go.transform.parent.gameObject;
			soluce.SetActive(false);
		}


		//maxBallsToFind = 1;
		//varier l'objet nécessaire
		GameObject sol = GameObject.FindGameObjectWithTag ("association");
		GameObject soluceGo = sol.transform.parent.gameObject;
		GameObject targetObj = soluceGo.transform.Find ("target").gameObject;
		targetObj.GetComponent<SpriteRenderer> ().enabled = false;
		GameObject target = soluceGo.transform.Find ("image").gameObject;
		// change target
		int nb = Random.Range (0, 3);
		target.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/" + tabSpriteImageAssoNames[nb]);
		target.tag = tabTagAssoNames[nb];
		soluceGo.transform.Find ("target").gameObject.tag = tabTagAssoNames[nb];


		//s'assurer d'avoir un objet
		GameObject[] listTargetElements = GameObject.FindGameObjectsWithTag (soluceGo.transform.Find ("image").gameObject.tag);
		if (listTargetElements.Length == 2) { // only the solution place ?
			GameObject[] listMovableElements = GameObject.FindGameObjectsWithTag ("movable");
			GameObject elemToChange = listMovableElements[0].transform.parent.gameObject;
			elemToChange.tag = soluceGo.transform.Find ("image").gameObject.tag;
			elemToChange.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite> ("Sprites/" + tabSpriteAssoNames[nb]);
		}
		maxBallsToFind = listTargetElements.Length - 2;
	}

	void ManageClassement() {
		//desactivate other solution zones
		GameObject go = GameObject.FindGameObjectWithTag ("association");
		GameObject soluce = go.transform.parent.gameObject;
		soluce.SetActive (false);
		GameObject[] goseria = GameObject.FindGameObjectsWithTag ("seriation");
		foreach (GameObject g in goseria) {
			GameObject sol = g.transform.parent.gameObject;
			sol.SetActive(false);
		}
		maxBallsToFind = 0;

		GameObject[] listBacsSol = GameObject.FindGameObjectsWithTag ("solution");
		foreach (GameObject sol in listBacsSol) {
			string target = sol.transform.Find ("target").gameObject.tag;
			print (" target ==> " + target);
			maxBallsToFind += GameObject.FindGameObjectsWithTag (target).Length - 1;
		}
		// eventuel switch de placement des 2 bacs solution
		if (Random.Range (0, 2) == 0) {
			if (listBacsSol.Length == 2) { // normalement tjrs le cas mais on ne sait jamais...
				Vector3 pos1 = listBacsSol[0].gameObject.transform.position;
				listBacsSol [0].gameObject.transform.position = listBacsSol [1].gameObject.transform.position;
				listBacsSol [1].gameObject.transform.position = pos1;
			}
		}
	}

	void ManageSeriation() {
		//desactivate other solution zones
		GameObject go = GameObject.FindGameObjectWithTag ("association");
		GameObject sol = go.transform.parent.gameObject;
		sol.SetActive (false);
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("classement");
		foreach (GameObject goo in gos) {
			GameObject soluce = goo.transform.parent.gameObject;
			//soluce.GetComponent<SpriteRenderer> ().enabled = false;
			soluce.SetActive(false);
		}

		GameObject[] listMedicineBalls = GameObject.FindGameObjectsWithTag ("medicineball");
		maxBallsToFind = listMedicineBalls.Length - 1;

		// choix entre 1 ou 2 objets solutions
		int nbMedicineBalls2toSoluce = Random.Range(0, 2) + 1;

		int lastRandom = 0;
		for (int i=0; i<nbMedicineBalls2toSoluce; i++) {
			int random;
			// placer au moins des balles à sa bonne place
			do {
				random = (Random.Range(0, 4)) * 2 +1; // random entre 1 3 5 7
			} while (random == lastRandom);
			lastRandom = random;

			GameObject medicineBallToPlace = GameObject.FindGameObjectWithTag ("medicineball" + random);
			print ("balle à placer = " + medicineBallToPlace);
			GameObject[] listGameObjectWithGoodTag = GameObject.FindGameObjectsWithTag (medicineBallToPlace.tag);
			foreach (GameObject g in listGameObjectWithGoodTag) {
				if (g.name == "target") {
					print ("Objet trouvé : " + g.transform.parent);
					Vector3 v = new Vector3 (g.transform.parent.transform.position.x, g.transform.parent.transform.position.y + 0.8f, g.transform.parent.transform.position.z);
					medicineBallToPlace.transform.position = v;
					medicineBallToPlace.transform.localScale = new Vector3(0.45f, 0.45f, 1);
					medicineBallToPlace.GetComponent<ManageMovableObject>().desactivateObject();
					maxBallsToFind--;
				}
			}
		}

	}*/
}
