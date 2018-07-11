using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class GameManager : MonoBehaviour {

	public TextAsset xmlLocalFile;
	public int maxScenes = 0;
	public int currentScene = 0;
	public LevelData[] levels;
	public string localFileName4Android;

    public string childName;
    void Start()
    {
        CreateFolders();
    }

    void Awake() {
		DontDestroyOnLoad(this);
        //LoadMyXML ();
        SceneManager.LoadScene("ProfileChoice");
        //SceneManager.LoadScene("LivingRoom-1");
    }

    public void CreateFolders()
    {
        string dir = Application.persistentDataPath + "/XMLFiles";
        CreateFolder(dir);
        dir = Application.persistentDataPath + "/XMLFiles/Reinforcers";
        CreateFolder(dir);
        dir = Application.persistentDataPath + "/XMLFiles/Preferences";
        CreateFolder(dir);
    }
    public void CreateFolder(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

	public int getMaxScene() {
		return maxScenes;
	}

	public LevelData getCurrentLevelData()
	{
        Debug.Log(levels.Length);
		return levels [currentScene];
	}


	public void LoadMyXML()
	{
		Debug.Log("Load XML file...");
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		WWW www;
		//#if UNITY_EDITOR
		/*if (xmlLocalFile != null) {
			print ("Local XML file found => load default local file");
			xmlDoc.LoadXml (xmlLocalFile.text);
		} else {
			// Start a download of the given URL
			www = new WWW ("file:///Users/plaforcade/Documents/Boulot/iWorkspaces/workspace4ESCAPEGAMERK/FlatModel4Unity/model/Scenario_OUT.flatmodel");
			print("local xml file not found... => load scenario from generator");
			xmlDoc.LoadXml(www.text); 
		}*/
		//#endif

		//#if UNITY_ANDROID
		string path = Application.persistentDataPath + "/XMLFiles/";
		string completeFile = "file://" + path + localFileName4Android + ".xml";
		print (completeFile);
		www = new WWW (completeFile);
		//yield return www;
		//string xml = File.ReadAllText(www.text);
		xmlDoc.LoadXml(www.text); 
		//#endif

		XmlNodeList levelsList = xmlDoc.GetElementsByTagName("level"); // array of the level nodes.

		maxScenes = levelsList.Count;

		levels = new LevelData[maxScenes];
		int cpt = 0;

		foreach (XmlNode levelInfo in levelsList)
		{
			LevelData newLevel = new LevelData ();
			newLevel.sceneName = levelInfo.Attributes["scene"].Value;
			newLevel.difficulty = levelInfo.Attributes["difficulty"].Value;
			newLevel.nbObjectsToFind = int.Parse( levelInfo.Attributes["nbElementsToPlace"].Value );
			levels [cpt++] = newLevel;


			XmlNodeList levelcontent = levelInfo.ChildNodes;

			List<Element> elems = new List<Element> ();
			List<SolutionObject> sols = new List<SolutionObject> ();
			List<PlacedDecor> decors = new List<PlacedDecor> ();
			List<Hideout> hideouts = new List<Hideout> ();

			foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
			{
				
				if(levelsItens.Name == "element")
				{
					Element elem = new Element ();
					elem.name = levelsItens.Attributes ["name"].Value;
					elem.pos = levelsItens.Attributes ["pos"].Value;
					elems.Add (elem);
					//obj.Add("name",levelsItens.InnerText); // put this in the dictionary.
				}
				if(levelsItens.Name == "solutionobject")
				{
					SolutionObject elem = new SolutionObject ();
					elem.name = levelsItens.Attributes ["name"].Value;
					elem.pos = levelsItens.Attributes ["pos"].Value;
					elem.acceptedElements = levelsItens.Attributes ["acceptedElements"].Value;
					elem.targetedSkill = levelsItens.Attributes ["targetedSkill"].Value;
					elem.nbElem2Find = int.Parse(levelsItens.Attributes ["nbSol2Find"].Value);
					List<Element> subElem = new List<Element> ();
					List<SolutionArea> solAreas = new List<SolutionArea> ();

					foreach (XmlNode sub in levelsItens.ChildNodes) 
					{

						if(sub.Name == "element")
						{
							Element subelement = new Element ();
							subelement.name = sub.Attributes ["name"].Value;
							subelement.pos = sub.Attributes ["pos"].Value;
							subElem.Add (subelement);
							//obj.Add("name",levelsItens.InnerText); // put this in the dictionary.
						}
						if(sub.Name == "solutionarea")
						{
							SolutionArea area = new SolutionArea ();
							area.pos = sub.Attributes ["pos"].Value;
							area.acceptedElement = sub.Attributes ["acceptedElement"].Value;

							solAreas.Add (area);
						}

					}
					elem.elements = (Element[]) subElem.ToArray();
					elem.solutionAreas = (SolutionArea[]) solAreas.ToArray();
					sols.Add (elem);
					//obj.Add("name",levelsItens.InnerText); // put this in the dictionary.
				}

				if(levelsItens.Name == "placeddecor")
				{
					PlacedDecor elem = new PlacedDecor ();
					elem.name = levelsItens.Attributes ["name"].Value;

					List<Element> subElem = new List<Element> ();
					List<Hideout> subHidouts = new List<Hideout> ();

					foreach (XmlNode sub in levelsItens.ChildNodes) 
					{
						if(sub.Name == "element")
						{
							Element subelement = new Element ();
							subelement.name = sub.Attributes ["name"].Value;
							subelement.pos = sub.Attributes ["pos"].Value;
							subElem.Add (subelement);
						}

						if(sub.Name == "hideout")
						{
							Hideout hidoutInDecor = new Hideout ();
							hidoutInDecor.name = sub.Attributes ["name"].Value;
							subHidouts.Add (hidoutInDecor);

							List<Element> subHidoutElem = new List<Element> ();

							foreach (XmlNode sub2 in sub.ChildNodes) 
							{
								if(sub2.Name == "element")
								{
									Element hidoutElement = new Element ();
									hidoutElement.name = sub2.Attributes ["name"].Value;
									hidoutElement.pos = sub2.Attributes ["pos"].Value;
									subHidoutElem.Add (hidoutElement);
								}

							}
							hidoutInDecor.hiddenElements = (Element[])subHidoutElem.ToArray ();

						}

					}
					elem.elements = (Element[]) subElem.ToArray();
					elem.hideouts = (Hideout[])subHidouts.ToArray ();
					decors.Add (elem);
					//obj.Add("name",levelsItens.InnerText); // put this in the dictionary.
				}

				if(levelsItens.Name == "hideout")
				{
					Hideout elem = new Hideout ();
					elem.name = levelsItens.Attributes ["name"].Value;
					elem.pos = levelsItens.Attributes ["pos"].Value;

					List<Element> subElem = new List<Element> ();

					foreach (XmlNode sub in levelsItens.ChildNodes) 
					{
						if(sub.Name == "element")
						{
							Element subelement = new Element ();
							subelement.name = sub.Attributes ["name"].Value;
							subelement.pos = sub.Attributes ["pos"].Value;
							subElem.Add (subelement);
						}
					}
					elem.hiddenElements = (Element[]) subElem.ToArray();
					hideouts.Add (elem);
					//obj.Add("name",levelsItens.InnerText); // put this in the dictionary.
				}
			}

			newLevel.elements = (Element[]) elems.ToArray();
			newLevel.solutionObjects = (SolutionObject[]) sols.ToArray();
			newLevel.placedDecors = (PlacedDecor[])decors.ToArray ();
			newLevel.hideouts = (Hideout[])hideouts.ToArray ();
			currentScene = 0;
		}

	}



}