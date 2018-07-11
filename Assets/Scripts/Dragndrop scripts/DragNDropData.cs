using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragNDropData : MonoBehaviour
{

    public bool moveable;
    public bool hidable;
    public string name;
    public string category;

    private GameObject childObject;

    Vector3 origin;
    string layer;
    Vector3 scale;
    //int orderLayout;

    public void SaveOrigin()
    {
        childObject = Instantiate(gameObject) as GameObject;
        childObject.transform.parent = null; //gameObject.transform.parent;
        childObject.transform.position = gameObject.transform.position;

        childObject.GetComponent<SpriteRenderer>().color = Color.gray;

        //childObject.transform.parent = parentObject.transform
        origin = gameObject.transform.position;
        layer = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "draglayer";
        scale = gameObject.transform.localScale;
        //orderLayout = gameObject.GetComponent<SpriteRenderer> ().sortingOrder;
        //gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x * 1.2f,
        // gameObject.transform.localScale.y * 1.2f, 1.0f); 
        //print ("change layer");
    }

    public void DeleteCopy()
    {
        if (childObject != null) {
            Destroy(childObject);
            childObject = null;
        }
        
    }    

    public void ReturnOrigin()
    {
        DeleteCopy();
        gameObject.transform.position = origin;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = layer;
        //gameObject.GetComponent<SpriteRenderer> ().sortingOrder = orderLayout;
        //print ("back layer");
        gameObject.transform.localScale = scale;
    }

    public void DeactivateColliders()
    {
        if (gameObject.GetComponent<BoxCollider2D>() != null)
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (gameObject.GetComponent<CircleCollider2D>() != null)
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        if (gameObject.GetComponent<PolygonCollider2D>() != null)
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }


    public void ActivateColliders()
    {
        if (gameObject.GetComponent<BoxCollider2D>() != null)
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (gameObject.GetComponent<CircleCollider2D>() != null)
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        if (gameObject.GetComponent<PolygonCollider2D>() != null)
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }


    public void DropToPosition(GameObject drop)
    {
        DeleteCopy();
        
        print(gameObject.name + " will be dropped into " + drop.name);
        Transform ownOrigin = gameObject.transform.parent;
        //if (drop.GetComponent<SpriteRenderer> () != null && drop.GetComponent<DropData> () != null && drop.GetComponent<DropData> ().solutionArea)
        //	gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = drop.GetComponent<SpriteRenderer> ().sortingLayerName;
        //else 
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "solutionPlace";

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = drop.GetComponent<SpriteRenderer>().sortingOrder;
        /*if (drop.transform.parent.GetComponent <DragNDropData> () != null) {
			
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		}*/
        // find original scale from prefab
        string path = "Prefabs/" + GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().sceneName + "/";

        string name = gameObject.GetComponent<DragNDropData>().name;
        //print ("name foireux = " + name);
        GameObject elem = Instantiate(Resources.Load(path + name)) as GameObject;

        gameObject.transform.SetParent(null);
        gameObject.transform.localScale = elem.transform.localScale;
        //print ("transform to local scale = ");
        //print(elem.transform.localScale);
        Destroy(elem);

        if (drop.GetComponent<DropData>() != null && drop.GetComponent<DropData>().solutionArea)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = drop.GetComponent<SpriteRenderer>().sortingLayerName;
            //print ("disable solution area collider of " + drop.name);
            drop.GetComponent<DropData>().DeactivateColliders();
        }

        // reactivate colliders of origin 
        if (ownOrigin != null)
        {
            GameObject currentParent = ownOrigin.gameObject;
            //print ("parent is " + parent.name);
            if (currentParent.GetComponent<DropData>() != null && currentParent.GetComponent<DropData>().solutionArea)
            {
                print("re-enable solution area collider of " + currentParent.name);
                currentParent.GetComponent<DropData>().ActivateColliders();
            }
        }

        if (drop.GetComponent<DropData>() != null && drop.GetComponent<DropData>().elementsDisappearedOnDrop)
        {
            gameObject.SetActive(false);
        }
        /*print ("childcount of (" + drop.name + ") = " + drop.transform.childCount);
		for (int i = 0; i < drop.transform.childCount; i++) {
			GameObject child = drop.transform.GetChild (i).gameObject;
			print("Child[" + i + "] = " + child.name);
		}
		*/
        if (drop.transform.childCount == 0
            || drop.GetComponent<DropData>().elementsDisappearedOnDrop
            || drop.transform.GetChild(0).name == "Particle System")
        {
            gameObject.transform.SetParent(drop.transform);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = drop.GetComponent<SpriteRenderer>().sortingLayerName;

            Vector3 pos = drop.transform.position;
            gameObject.transform.position = pos;
            origin = pos;
        }
        else
        {
            /*print ("move");
			if (hasAlreadyAnObject(drop)) {
				print ("deja qqun => swap" );
				GameObject otherGo = drop.transform.GetChild (0).gameObject;
				// swap places
				otherGo.GetComponent <DragNDropData> ().DropToPosition (ownOrigin.gameObject);
			}*/

            for (int i = 0; i < drop.transform.childCount; i++)
            {
                Transform child = drop.transform.GetChild(i);
                if (child.GetComponent<DropData>() == null)
                    continue;
                if (child.childCount == 0)
                {

                    gameObject.transform.SetParent(child);
                    Vector3 pos = child.transform.position;
                    gameObject.transform.position = pos;
                    origin = pos;
                    changeLayerWith(child.gameObject);

                }
            }
        }
        if (drop.GetComponent<DropData>() != null && drop.GetComponent<DropData>().compatibleWith(gameObject))
        {

            moveable = false;
            DeactivateColliders();

            Transform psgo = drop.transform.Find("Particle System");
            if (psgo != null)
            {
                ParticleSystem ps = psgo.gameObject.GetComponent<ParticleSystem>();
                ParticleSystem.EmissionModule em = ps.emission;
                em.enabled = true;
                if (ps.isPlaying)
                    ps.Stop();
                ps.Play();
            }
            SoundManagerScript.PlaySound(SoundManagerScript.SUCCESS);


            //acceptedElements == this.name || drop.GetComponent<DropData> ().acceptedElements == this.category) {
            GameObject.Find("FeedbacksManager").GetComponent<FeedbacksManager>().AddStar();
            string difficulty = GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().difficulty;
            if (!difficulty.Equals("EXPERT") && !difficulty.Equals("ADVANCED"))
                GameObject.Find("FeedbacksManager").GetComponent<FeedbacksManager>().AnimateLocalFeedback(drop);


            GameObject.Find("Setup").GetComponent<InitGameElements>().maxBallsToFind--;
            if (GameObject.Find("Setup").GetComponent<InitGameElements>().maxBallsToFind == 0)
            {
                // no more elements to move => let's opening the door
                GameObject.Find("Sortie").GetComponent<ManageExit>().OnTurnOn();
            }

        }

    }

    private void changeLayerWith(GameObject parent)
    {
        if (parent.GetComponent<SpriteRenderer>() == null)
            return;
        string layer = parent.GetComponent<SpriteRenderer>().sortingLayerName;
        print("Layer = " + layer);
        if (layer != "")
        {
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = layer;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = parent.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }

    private bool hasAlreadyAnObject(GameObject go)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            GameObject child = go.transform.GetChild(0).gameObject;
            if (child.GetComponent<DragNDropData>() != null)
            {
                if (child.GetComponent<DragNDropData>().moveable)
                    return true;
            }
        }
        return false;
    }

    public void showHiddenElements()
    {
        gameObject.GetComponent<DragNDropData>().hidable = false;

        DeactivateColliders();

        // change hidding object appearence if required
        LevelData data = GameObject.FindObjectOfType<GameManager>().getCurrentLevelData();
        string nameobject = "";
        if (gameObject.GetComponent<SpriteRenderer>().sprite != null)
            nameobject = gameObject.GetComponent<SpriteRenderer>().sprite.name;
        Sprite newSprite = Resources.Load<Sprite>("Sprites/" + data.sceneName + "/" + nameobject + "Opened");
        if (newSprite != null)
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;

        if (gameObject.GetComponent<SpriteRenderer>().enabled == false)
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

        string nameToFind = gameObject.name.Split('(')[0];

        SoundManagerScript.PlaySound(SoundManagerScript.UNHIDDE);

        // for each hidden place
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject go = gameObject.transform.GetChild(i).gameObject;

            if (go.name.Equals(nameToFind + "Front"))
                go.SetActive(true);
            // if the place is not empty, then it contains an object to show !
            if (go.transform.childCount > 0)
            {
                Transform elem = go.transform.GetChild(0);
                elem.gameObject.SetActive(true);
                elem.tag = "selection";
                elem.GetComponent<SpriteRenderer>().sortingOrder = go.GetComponent<SpriteRenderer>().sortingOrder + 1;
                print("rend visible");
                /*if (elem.GetComponent<SpriteRenderer> () != null) {
					elem.GetComponent<SpriteRenderer> ().sortingOrder = 1;
				}*/
                /*if (elem.GetComponent<DragNDropData> () != null) {
					elem.GetComponent<DragNDropData> ().SaveOrigin ();
				}*/
            }
        }

    }

}
