using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
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
    public float levelShield= 0;

    public Stats Clone()
    {
        return (Stats)this.MemberwiseClone(); // Cria uma c�pia rasa da classe Stats
    }
}