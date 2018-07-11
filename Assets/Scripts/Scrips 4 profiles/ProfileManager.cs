using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager {
    //attribut
    private static List<Profile> profiles = new List<Profile>();


    //Getters et setters
    public static List<Profile> GetProfiles()
    {
        return profiles;
    }
    public static int GetNextId()
    {
        int i = 0;
        while (IsFoundId(profiles, i))
        {
            i++;
        }
        return i;
    }
    public static bool IsFoundId(List<Profile> profiles, int i)
    {
        bool b = false;
        foreach(Profile p in profiles)
        {
            if(p.GetId() == i)
            {
                b = true;
                break;
            }
        }
        return b;
    }
    public static void SetProfiles()
    {
        profiles = XMLManager.LoadProfiles();
        profiles.Sort((x, y) => DateTime.Compare(y.GetLastAuth(), x.GetLastAuth()));
    }
    public static List<Profile> GetProfilesFromXML()
    {
        SetProfiles();
        return GetProfiles();
    }


    //Ajouter ou retirer des profils
    public static void AddProfile(Profile profileToAdd)
    {
        profiles.Add(profileToAdd);
        profiles.Sort((x, y) => DateTime.Compare(y.GetLastAuth(), x.GetLastAuth()));
    }

    //ATTENTION DEFINIR L OPERATEUR D EGALITE
    public static void RemoveProfile(Profile profileToRemove)
    {
        for (int i = profiles.Count - 1; i >= 0; i--)
        {
            if (profiles[i] == profileToRemove)
            {
               profiles.RemoveAt(i);
            }
        }
    }


    //sauvegarde des profils

    public static void Save()
    {
        XMLManager.SaveProfiles(profiles);
    }

    //authentification
    public static void Connect(Profile p)
    {
        p.setLastAuth();
        profiles.Sort((x, y) => DateTime.Compare(y.GetLastAuth(), x.GetLastAuth()));
        Authentication.Connect(p);
    }
    public static void Update()
    {
        profiles[0] = Authentication.currentProfile;
        Save();
        SetProfiles();
    }

    public static void Disconnect()
    {
        Authentication.QuitCurrentProfile();
        Save();
    }
    //supression du profil courant
    public static void Delete()
    {
        RemoveProfile(Authentication.currentProfile);
        Save();   
    }
    public static void SavePreferences()
    {

    }
}
