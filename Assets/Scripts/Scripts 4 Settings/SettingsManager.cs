using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private Profile profile;
    private Profile cur;

    //General Settings
    public InputField nameField;
    public Button picture;

    //Skill Settings
    public SkillSettingsScript B3;
    public SkillSettingsScript B4;
    public SkillSettingsScript B8;
    public SkillSettingsScript B9;
    public SkillSettingsScript B13;
    public SkillSettingsScript B19;
    public SkillSettingsScript B25;

    //Skill Rows
    public SkillRowScript B3Row;
    public SkillRowScript B4Row;
    public SkillRowScript B8Row;
    public SkillRowScript B9Row;
    public SkillRowScript B13Row;
    public SkillRowScript B19Row;
    public SkillRowScript B25Row;

    //Preferences Settings

    public Dropdown reinforcer;
    public Dropdown musicTheme;
    public InputField code;

    //Ergonomics Settings


    void Start()
    {
        profile = Authentication.currentProfile;
        //initialize General Sttings Display
        nameField.text = profile.GetName();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Profile/animals");
        picture.image.sprite = sprites[profile.GetSprite()];


        //initialize Skills Settings Display
        SetSkillOnDisplay(B3, profile.GetB3());
        SetSkillOnDisplay(B4, profile.GetB4());
        SetSkillOnDisplay(B8, profile.GetB8());
        SetSkillOnDisplay(B9, profile.GetB9());
        SetSkillOnDisplay(B13, profile.GetB13());
        SetSkillOnDisplay(B19, profile.GetB19());
        SetSkillOnDisplay(B25, profile.GetB25());

        //initialize Rows Display
        DisplaySkillOnRow(profile.GetB3(), B3Row);
        DisplaySkillOnRow(profile.GetB4(), B4Row);
        DisplaySkillOnRow(profile.GetB8(), B8Row);
        DisplaySkillOnRow(profile.GetB9(), B9Row);
        DisplaySkillOnRow(profile.GetB13(), B13Row);
        DisplaySkillOnRow(profile.GetB19(), B19Row);
        DisplaySkillOnRow(profile.GetB25(), B25Row);

        //initialize preferences Display
        DisplayPreferences();
        //initialize Skills Settings Display
    }
    public void SaveSettings()
    {
        //Compare
        CompareWithOlder();

        //remplir nom et image avec les settings
        profile.setName(nameField.text);
        //remplir les compétences avec les settings
        GetSkill(profile.GetB3(), B3);
        GetSkill(profile.GetB4(), B4);
        GetSkill(profile.GetB8(), B8);
        GetSkill(profile.GetB9(), B9);
        GetSkill(profile.GetB13(), B13);
        GetSkill(profile.GetB19(), B19);
        GetSkill(profile.GetB25(), B25);
        //récupérer l'avancement
        Avancement(profile.GetB3(), B3Row);
        Avancement(profile.GetB4(), B4Row);
        Avancement(profile.GetB8(), B8Row);
        Avancement(profile.GetB9(), B9Row);
        Avancement(profile.GetB13(), B13Row);
        Avancement(profile.GetB19(), B19Row);
        Avancement(profile.GetB25(), B25Row);
        Debug.Log("cp : "+profile);
        ProfileManager.Update();
        SavePreferences();
    }

    public void Avancement(Skill s, SkillRowScript sr)
    {
        if(s.consecutiveSuccess >= 0)
        {
            if (sr.star2.IsActive())
            {
                s.consecutiveSuccess = 2;
            }
            else if (sr.star1.IsActive())
            {
                s.consecutiveSuccess = 1;
            }
            else
            {
                s.consecutiveSuccess = 0;
            }
        }
        else
        {
            Debug.Log("Avancement :" + s);
            s.consecutiveSuccess = 0;
        }


    }
    public void SetSkillOnDisplay(SkillSettingsScript B, Skill pB)
    {
        B.activated.isOn = pB.m_active;
        B.masteryLevel.value = (int)pB.m_mastery;
        B.acquired.isOn = pB.m_acquired;
    }
    public void GetSkill(Skill pB, SkillSettingsScript B)
    {
        pB.m_active = B.activated.isOn;
        pB.m_mastery = (Mastery)B.masteryLevel.value;
        pB.m_acquired = B.acquired.isOn;
        Debug.Log(pB);

    }

    public void DisplaySkillOnRow(Skill s, SkillRowScript r)
    {
        if (!s.m_active)
        {
            r.master.enabled = false;
            r.masterImage.enabled = true;
            r.star1.enabled = false;
            r.star2.enabled = false;

            switch (s.m_mastery)
            {
                case Mastery.Beginner:
                    r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-blue64");
                    r.master.text = "Débutant";
                    break;
                case Mastery.Elementary:
                    r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-green64");
                    r.master.text = "Élémentaire";
                    break;
                case Mastery.Intermediate:
                    r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-yellow64");
                    r.master.text = "Intermédiaire";
                    break;
                case Mastery.Advanced:
                    r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-orange64");
                    r.master.text = "Avancé";
                    break;
                case Mastery.Expert:
                    r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-red64");
                    r.master.text = "Expert";
                    break;
            }
        }
        else
        {
            r.master.enabled = true;
            r.masterImage.enabled = true;
            r.star1.enabled = true;
            r.star2.enabled = true;
            if (s.m_acquired)
            {
                r.master.text = "Acquis";
                r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-purple64");
            }
            else
            {
                switch (s.m_mastery)
                {
                    case Mastery.Beginner:
                        r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-blue64");
                        r.master.text = "Débutant";
                        break;
                    case Mastery.Elementary:
                        r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-green64");
                        r.master.text = "Élémentaire";
                        break;
                    case Mastery.Intermediate:
                        r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-yellow64");
                        r.master.text = "Intermédiaire";
                        break;
                    case Mastery.Advanced:
                        r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-orange64");
                        r.master.text = "Avancé";
                        break;
                    case Mastery.Expert:
                        r.masterImage.sprite = Resources.Load<Sprite>("Sprites/Profile/Masteries/mastery-red64");
                        r.master.text = "Expert";
                        break;
                }
            }

            if (s.consecutiveSuccess > 1)
            {
                r.star1.enabled = true;
                r.star2.enabled = true;
            }
            else if (s.consecutiveSuccess > 0)
            {
                r.star1.enabled = true;
                r.star2.enabled = false;
            }
            else
            {
                r.star1.enabled = false;
                r.star2.enabled = false;
            }
        }
    }
    public void UpdateRows()
    {
        SaveSettings();

        DisplaySkillOnRow(profile.GetB3(), B3Row);
        DisplaySkillOnRow(profile.GetB4(), B4Row);
        DisplaySkillOnRow(profile.GetB8(), B8Row);
        DisplaySkillOnRow(profile.GetB9(), B9Row);
        DisplaySkillOnRow(profile.GetB13(), B13Row);
        DisplaySkillOnRow(profile.GetB19(), B19Row);
        DisplaySkillOnRow(profile.GetB25(), B25Row);

    }

    public void CompareWithOlder()
    {

        if(profile.GetB3().m_mastery != (Mastery)B3.masteryLevel.value)
        {
            profile.GetB3().consecutiveSuccess = -1;
        }
        if (profile.GetB4().m_mastery != (Mastery)B4.masteryLevel.value)
        {
            profile.GetB4().consecutiveSuccess = -1;
        }
        if (profile.GetB8().m_mastery != (Mastery)B8.masteryLevel.value)
        {
            profile.GetB8().consecutiveSuccess = -1;
        }
        if (profile.GetB9().m_mastery != (Mastery)B9.masteryLevel.value)
        {
            profile.GetB9().consecutiveSuccess = -1;
        }
        if (profile.GetB13().m_mastery != (Mastery)B13.masteryLevel.value)
        {
            profile.GetB13().consecutiveSuccess = -1;
        }
        if (profile.GetB19().m_mastery != (Mastery)B19.masteryLevel.value)
        {
            profile.GetB19().consecutiveSuccess = -1;
        }
        if (profile.GetB25().m_mastery != (Mastery)B25.masteryLevel.value)
        {
            profile.GetB25().consecutiveSuccess = -1;
        }
    }
    public void changePicture(int index)
    {
        profile.SetSprite(index);
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Profile/animals");
        picture.image.sprite = sprites[profile.GetSprite()];
    }

    public void SavePreferences()
    {
        //instancier Preferences
        Preferences pref = new Preferences();
        //remplir ses différents attributs
        pref.SetReinforcer((Reinforcer)reinforcer.value);
        pref.SetMusicTheme((MusicTheme)musicTheme.value);      
        pref.SetCode(code.text);
        //appel au XMLManager
        XMLManager.SavePreferences(pref);
        DisplayPreferences();
    }
    public void DisplayPreferences()
    {
        //instancier Preferences
        Preferences pref = new Preferences();
        //appel au XMLManager
        pref = XMLManager.LoadPreferences();
        //affichage sur l'écran
        reinforcer.value = (int) pref.GetReinforcer();
        musicTheme.value = (int) pref.GetMusicTheme();
        code.text = pref.GetCode();
    }
}
