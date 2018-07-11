using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public class GeneratorXMLCreator : MonoBehaviour {

    public static GeneratorXMLCreator ins;

    void Awake()
    {
        ins = this;
    }


    public static void Create(Profile profile, int nbLevel)
    {

        ScenarioIntel si = new ScenarioIntel(profile, nbLevel);
        XmlSerializer serializer = new XmlSerializer(typeof(ScenarioIntel),"Profile");
        FileStream stream = new FileStream(Application.persistentDataPath + "/XMLFiles/scenarioIntel.xml", FileMode.Create);//overwrites
        serializer.Serialize(stream, si);
        stream.Close();
    }
}


[XmlRoot("Profile")]

public class ScenarioIntel
{

    //[XmlAttribute("xmlns")]
    //public string s = "Profile";

    public int nbLevel;

    
    [XmlArray("Skills")]
    [XmlArrayItem("Skill")]
    public List<SkillIntel> liste = new List<SkillIntel>();

    public ScenarioIntel() { }
    public ScenarioIntel(Profile profile, int nbLvl)
    {
        nbLevel = nbLvl;
        
        SkillIntel B3 = new SkillIntel("B3", profile.GetB3());
        SkillIntel B4 = new SkillIntel("B4", profile.GetB4());
        SkillIntel B8 = new SkillIntel("B8", profile.GetB8());
        SkillIntel B9 = new SkillIntel("B9", profile.GetB9());
        SkillIntel B13 = new SkillIntel("B13", profile.GetB13());
        SkillIntel B19 = new SkillIntel("B19", profile.GetB19());
        SkillIntel B25 = new SkillIntel("B25", profile.GetB25());
        SkillIntel[] intel = { B3, B4, B8, B9, B13, B19, B25 };
        liste.AddRange(intel);  

    }
}



public class SkillIntel {

    [XmlAttribute("name")]
    public string Name;

    public bool active;
    public Mastery mastery;
    public bool acquired;

    public SkillIntel() { }
    public SkillIntel(string name, Skill s)
    {
        Name = name;
        active = s.m_active;
        mastery = s.m_mastery;
        acquired = s.m_acquired;
    }
}


