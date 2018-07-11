using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences {

    //Attributs

    private Reinforcer reinforcer;
    private MusicTheme musicTheme;
    private string code;

    //Constructeur

    public Preferences()
    {
        reinforcer = Reinforcer.dino;
        musicTheme = MusicTheme.mario;
        code = "0000";
    }
    public Preferences(SPref spref)
    {
        reinforcer = spref.reinforcer;
        musicTheme = spref.musicTheme;
        code = spref.code;
    }

    //Getters et setters

    public Reinforcer GetReinforcer()
    {
        return reinforcer;
    }
    public MusicTheme GetMusicTheme()
    {
        return musicTheme;
    }
    public string GetCode()
    {
        return code;
    }

    public void SetReinforcer(Reinforcer r)
    {
        reinforcer = r;
    }
    public void SetMusicTheme(MusicTheme m)
    {
        musicTheme = m;
    }
    public void SetCode(string c)
    {
        code = c;
    }
	
}
