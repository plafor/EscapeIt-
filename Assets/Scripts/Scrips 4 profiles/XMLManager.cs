using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System;

public class XMLManager : MonoBehaviour {

    public static XMLManager ins;

    void Awake()
    {
        ins = this;
    }

    //liste des profils
    private static  ProfileDB profileDB = new ProfileDB();

   //save function
    public static void SaveProfiles(List<Profile> pList)
    {
        profileDB.profiles.Clear();
        for (int i = 0; i < pList.Count; i++)
        {
            profileDB.profiles.Add(new SProfile(pList[i]));
            Debug.Log("Profile : " + pList[i]);
            Debug.Log("SProfile : "+profileDB.profiles[i]);
        }
        XmlSerializer serializer = new XmlSerializer(typeof(ProfileDB));
        FileStream stream = new FileStream(Application.persistentDataPath+"/XMLFiles/profiles.xml", FileMode.Create);//overwrites
        serializer.Serialize(stream, profileDB);
        stream.Close();
    }

    public static List<Profile> LoadProfiles()
    {
        List<Profile> ps = new List<Profile>();
        if (File.Exists(Application.persistentDataPath + "/XMLFiles/profiles.xml")) { 
            XmlSerializer serializer = new XmlSerializer(typeof(ProfileDB));
            FileStream stream = new FileStream(Application.persistentDataPath + "/XMLFiles/profiles.xml", FileMode.Open);
            profileDB = serializer.Deserialize(stream) as ProfileDB;
            stream.Close();
            for (int i = 0; i < profileDB.profiles.Count; i++)
            {
                ps.Add(new Profile(profileDB.profiles[i]));
            }
        }
        return ps;
    }

    public static void SavePreferences(Preferences pref)
    {
        SPref spref = new global::SPref(pref);
        XmlSerializer serializer = new XmlSerializer(typeof(SPref));
        FileStream stream = new FileStream(Application.persistentDataPath + "/XMLFiles/Preferences/child"+Authentication.currentProfile.GetId()+"preferences.xml", FileMode.Create);//overwrites
        serializer.Serialize(stream, spref);
        stream.Close();
    }
    //utilisé à la création d'un profil
    public static void SavePreferences(Preferences pref, int id)
    {
        SPref spref = new global::SPref(pref);
        XmlSerializer serializer = new XmlSerializer(typeof(SPref));
        FileStream stream = new FileStream(Application.persistentDataPath + "/XMLFiles/Preferences/child" + id + "preferences.xml", FileMode.Create);//overwrites
        serializer.Serialize(stream, spref);
        stream.Close();
    }
    public static Preferences LoadPreferences()
    {   
       if (File.Exists(Application.persistentDataPath + "/XMLFiles/Preferences/child" + Authentication.currentProfile.GetId() + "preferences.xml"))
        {
            Preferences resul;
            SPref temp;
            XmlSerializer serializer = new XmlSerializer(typeof(SPref));
            FileStream stream = new FileStream(Application.persistentDataPath + "/XMLFiles/Preferences/child" + Authentication.currentProfile.GetId() + "preferences.xml", FileMode.Open);
            temp = serializer.Deserialize(stream) as SPref;
            stream.Close();
            resul = new global::Preferences(temp);
            return resul;
        }else
        {
            Preferences pref = new Preferences();
            SavePreferences(pref);
            return LoadPreferences();
        }

    }
}

public class SPref
{
    public Reinforcer reinforcer;
    public MusicTheme musicTheme;
    public string code;

    public SPref(){}
    public SPref(Preferences pref)
    {
        reinforcer = pref.GetReinforcer();
        musicTheme = pref.GetMusicTheme();
        code = pref.GetCode();
    }


}

public class ProfileDB
{
   //attributs
    [XmlArray("Profiles")]//Nom d'Élément pour la sérialisation en xml
    public List<SProfile> profiles = new List<SProfile>();

    //constructeur par défaut
    public ProfileDB() { }


    //Méthodes
    public List<Profile> GetProfiles()
    {
        List<Profile> profileList = new List<Profile>();
        for (int i = 0; i < profiles.Count; i++ )
        {
            profileList.Add(new Profile(profiles[i]));
        }
        return profileList;
    }
}
public class SProfile
{
    public int id;
    public string m_name;
    public DateTime m_lastAuth; //Dernière authentification de l'utilisateur
    public int m_sprite;
    public Skill B3;
    public Skill B4;
    public Skill B8;
    public Skill B9;
    public Skill B13;
    public Skill B19;
    public Skill B25;

    //constructeurs
    public SProfile(){}
    public SProfile(Profile p)
    {
        id = p.GetId();
        m_name = p.GetName();
        m_lastAuth = p.GetLastAuth();
        m_sprite = p.GetSprite();
        B3 = p.GetB3();
        B4 = p.GetB4();
        B8 = p.GetB8();
        B9 = p.GetB9();
        B13 = p.GetB13();
        B19 = p.GetB19();
        B25 = p.GetB25();

    }

    //Méthodes surchargées
    override public string ToString()
    {
        return "SProfile :"+m_name+
            "\n"+B3+
            "\n" + B4 +
            "\n" + B8 +
            "\n" + B9 +
            "\n" + B13 +
            "\n" + B19+
            "\n" + B25;
    }

}