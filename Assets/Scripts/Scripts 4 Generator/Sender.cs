using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Sender : MonoBehaviour {

    //private const string url = "http://localhost/EscapeIt/";
    private const string url = "https://la-projets.univ-lemans.fr/escapeit/Generator/Index.php";

    // Use this for initialization
    void Start () {
        
        StreamReader reader = new StreamReader(Application.persistentDataPath+ "/XMLFiles/scenarioIntel.xml");
        string xmlData = reader.ReadToEnd();

        WWWForm form = new WWWForm();
        form.AddField("xml", xmlData);
        byte[] rawFormData = form.data;
       
        WWW request = new WWW(url,rawFormData);

        StartCoroutine(OnResponse(request));
        //StartCoroutine(Upload());
	}
    IEnumerator Upload()
    {
        StreamReader reader = new StreamReader(Application.persistentDataPath + "/XMLFiles/scenarioIntel.xml");
        string xmlData = reader.ReadToEnd();

        WWWForm form = new WWWForm();
        form.AddField("xml", xmlData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log(www.responseCode);
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    private IEnumerator OnResponse(WWW req)
    {
        yield return req;
        Debug.Log(req.text);
        HandleResponse(req.text);
        LoadGameStructure();
    }
    public void HandleResponse(string text)
    {
        if (text.Contains("ERROR"))
        {
            Debug.LogError("error : "+text);
        }
        else
        {
            //text = FormatScenario(text);
            CreateScenario(text);
        }
    }
    public void CreateScenario(string text)
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/XMLFiles/" + "Scenario_OUT.xml", text);
        GameManager manager = GameObject.FindObjectOfType<GameManager>();

        manager.localFileName4Android = "Scenario_OUT";//Scenario pour tester livingRoom et Scenario-OUT pour tester le générateur
        manager.LoadMyXML();

    }
    public void LoadGameStructure()
    {
        StartCoroutine(LoadGameStructureScreen());
    }

    IEnumerator LoadGameStructureScreen()
    {
        yield return true; //new WaitForSeconds(1.0f); // wait time
        SceneManager.LoadScene("GameStructure");
    }
    public string FormatScenario(string text)
    {
        int index = text.IndexOf("<?");
        text.Substring(index);
        return text;
    }
}
