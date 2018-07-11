using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Profile {

    //ATTRIBUTS
    private int id;
    private string m_name;
    private int m_sprite;
    private DateTime m_lastAuth; //Dernière authentification de l'utilisateur
    private Skill B3;
    private Skill B4;
    private Skill B8;
    private Skill B9;
    private Skill B13;
    private Skill B19;
    private Skill B25;
    private Reinforcer reinforcer;
    private MusicTheme musicTheme;

    //CONSTRUCTEURS
    public Profile(string name)
    {
        id = ProfileManager.GetNextId();
        m_name = name;
        m_sprite = 0;
        m_lastAuth = DateTime.Now;
        B3 = new Skill();
        B4 = new Skill();
        B8 = new Skill();
        B9 = new Skill();
        B13 = new Skill();
        B19 = new Skill();
        B25 = new Skill();

    }
    public Profile(): this("Dorian") { }
    public Profile(Profile p)
    {
        id = p.id;
        m_name = p.m_name;
        m_sprite = p.m_sprite;
        m_lastAuth = p.m_lastAuth;
        B3 = p.B3;
        B4 = p.B4;
        B8 = p.B8;
        B9 = p.B9;
        B13 = p.B13;
        B19 = p.B19;
        B25 = p.B25;
    }
    public Profile(SProfile p)
    {
        id = p.id;
        m_name = p.m_name;
        m_sprite = p.m_sprite;
        m_lastAuth = p.m_lastAuth;
        B3 = p.B3;
        B4 = p.B4;
        B8 = p.B8;
        B9 = p.B9;
        B13 = p.B13;
        B19 = p.B19;
        B25 = p.B25;
    }


    //GetTERS
    public string GetName()
    {
        return m_name;
    }
    public DateTime GetLastAuth()
    {
        return m_lastAuth;
    }
    public int GetSprite()
    {
        return m_sprite;
    }
    //GetTERS SKILLS
    public Skill GetB3()
    {
        return B3;
    }
    public Skill GetB4()
    {
        return B4;
    }
    public Skill GetB8()
    {
        return B8;
    }
    public Skill GetB9()
    {
        return B9;
    }
    public Skill GetB13()
    {
        return B13;
    }

    public Skill GetB19()
    {
        return B19;
    }
    public Skill GetB25()
    {
        return B25;
    }
    public int GetId()
    {
        return id;
    }

    //SETTERS
    public void setName(string name)
    {
        m_name = name;
    }
    public void setLastAuth()
    {
        m_lastAuth = DateTime.Now;
    }
    public void SetSprite(int index)
    {
        m_sprite = index;
    }
    
    //METHODES

    override public string ToString()
    {
        return "Profile("+id+") : " + m_name
            +"\n sprite : "+m_sprite
            +"\n B3 : "+B3.ToString()
            + "\n B4 : " + B4.ToString()
            + "\n B8 : " + B8.ToString()
            + "\n B9 : " + B9.ToString()
            + "\n B13 : " + B13.ToString()
            + "\n B19 : " + B19.ToString()
            + "\n B25 : " + B25.ToString();
    }
    
}
