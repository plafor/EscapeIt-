using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbacksManager : MonoBehaviour
{

    public float shiftF = 0.7f;

    public void AddStar()
    {
        print("add star");
        GameObject parent = GameObject.Find("ProgressionStars");
        int nbChilds = parent.transform.childCount;
        for (int i = 0; i < nbChilds; i++)
        {
            Transform child = parent.transform.GetChild(i);
            if (child.tag == "emptyStar")
            {
                GameObject firstEmptyStar = child.gameObject;
                firstEmptyStar.GetComponent<Animator>().SetTrigger("fill");
                firstEmptyStar.tag = "star";
                break;
            }

        }
    }

    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    public void DisplayElem2Find(SolutionObject solData, GameObject solObj)
    {
        int nb = solData.getElem2Find();
        print(solObj.name + " required " + nb + " elements to find");
        Transform go = solObj.transform.Find("Feedbacks");
        //new GameObject ("Feedbacks");
        //go.transform.SetParent (solObj.transform);
        float shift = -(shiftF * nb / 2);
        if (nb % 2 == 0)
            shift = shift + shiftF / 2;
        go.transform.localPosition = new Vector2(shift + go.transform.localPosition.x, go.transform.localPosition.y);
        //go.transform.position = new Vector3 (shift, go.transform.position.y, go.transform.position.z);
        for (int i = 0; i < nb; i++)
        {
            //string path = "Prefabs/";
            string path = "Prefabs/" + GameObject.FindObjectOfType<GameManager>().getCurrentLevelData().sceneName + "/";
            GameObject elem = Instantiate(Resources.Load(path + "SmallStarEmpty")) as GameObject;
            //elem.name = "SmallStarEmpty";
            elem.transform.SetParent(go);
            elem.transform.position = new Vector3(0, 0, 0);
            float dx = i * shiftF;
            elem.transform.localPosition = new Vector2(dx, 0f);
        }
    }

    public void AnimateLocalFeedback(GameObject drop)
    {
        Transform go = drop.transform.Find("Feedbacks");
        if (go == null)
        {
            go = drop.transform.parent.Find("Feedbacks");
        }
        int max = go.childCount;
        for (int i = 0; i < max; i++)
        {
            Transform child = go.GetChild(i);
            if (child.GetComponent<SpriteRenderer>().sprite.name == "StarEmpty")
            {
                if (Resources.Load<Sprite>("Sprites/Star") != null)
                {
                    child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Star");
                }
                break;
            }
        }
    }
}
