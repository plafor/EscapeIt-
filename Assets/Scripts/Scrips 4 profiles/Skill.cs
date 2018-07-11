using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill 
{
    //ATTRIBUTS
    public bool m_active;
    public Mastery m_mastery;
    public bool m_acquired;
    public int consecutiveSuccess;

    //CONSTRUCTEURS
    public Skill(bool active, Mastery mastery, bool acquired, int cSuccess)
    {
        m_mastery = mastery;
        m_active = active;
        if(mastery == Mastery.Expert)
        {
            m_acquired = acquired;
        }else
        {
            m_acquired = false;
        }
        consecutiveSuccess = cSuccess;
    }
    public Skill(bool active, Mastery mastery) : this(active, mastery, false, 0){ }
    public Skill(): this(true, Mastery.Beginner) { }

    //GETTERS

    public Mastery GetMastery()
    {
        return m_mastery;
    }
    public bool isAcquired()
    {
        return m_acquired;
    }
    public bool isActive()
    {
        return m_active;
    }
    public int GetConsecutiveSuccess()
    {
        return consecutiveSuccess;
    }


    //SETTERS
    public void setMastery(Mastery mastery)
    {
        m_mastery = mastery;
    }
    public void setActive(bool active)
    {
        m_active = active;
    }
    public void SetAcquired(bool acquired)
    {
        if (!acquired || (m_mastery == Mastery.Expert))
        {
            m_acquired = acquired;
        }
    }

    public void IncConsecutiveSuccess()
    {
        if (!m_acquired)
        {
            if (consecutiveSuccess == 2)
            {
                consecutiveSuccess = 0;
                if(m_mastery < Mastery.Expert)
                {
                    m_mastery++;
                }
            }
            else
            {
                consecutiveSuccess++;
            }
        }

    }
    //Méthodes surchargées
    public override string ToString()
    {
        return "[Skill] : "+m_active+" : "+m_mastery+" : "+m_acquired;
    }
}

