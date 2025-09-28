using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public delegate void HealthChanged(float hp);
    public event HealthChanged OnHealthChanged;
    public delegate void XpChange(float xp);
    public event XpChange OnXpChanged;
    public float maxHP = 0;
    public float hp = 0;
    public float baseAttack = 0;
    public float attackSpeed = 0;
    public float criticalChance = 0;
    public float criticalPercent = 0;
    public float speed = 0;
    public bool invincible = false;

    public float souls = 0;

    public float xp = 0;
    public float maxXp = 0;
    public float levelBible = 0;
    public float levelHadounken = 0;
    public float levelIma = 0;
    public float levelFoice = 0;
    public float levelShield = 0;

    public Stats Clone()
    {
        return (Stats)this.MemberwiseClone(); // Cria uma c�pia rasa da classe Stats
    }

    public void HpRemove(float damage)
    {
        hp -= damage;
        OnHealthChanged?.Invoke(hp);
    }

    public void ExpHandler( float points = 10 ) {
        if(xp == maxXp) {
            xp = 0;
            maxXp += 100;
        }
        OnXpChanged?.Invoke(points);
    }
}