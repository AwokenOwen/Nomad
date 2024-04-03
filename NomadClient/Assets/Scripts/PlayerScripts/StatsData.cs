using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    public float HealthStat;
    public float DefenseStat;
    public float PhysicalStrengthStat;
    public float MagicalStrengthStat;
    public float StaminaStat;

    public float currentHealth;
    public float maxHealth;

    public float currentStamina;
    public float maxStamina;

    public StatsData() 
    { 
        HealthStat = 0f; 
        DefenseStat = 0f; 
        StaminaStat = 0f;
        PhysicalStrengthStat = 0f; 
        MagicalStrengthStat = 0f;

        this.maxHealth = calculateMaxHealth();
        this.currentHealth = maxHealth;

        this.maxStamina = calculateMaxStamina();
        this.currentStamina = maxStamina;
    }

    float calculateMaxHealth()
    {
        //calculate the health based on the health stat

        return 1f;
    }

    float calculateMaxStamina()
    {
        //calculate the stamina based on the stamina stat

        return 1f;
    }

    public void increaseStat(Stats stat, float increaseValue)
    {
        switch (stat)
        {
            case Stats.HEALTH:
                HealthStat += increaseValue;
                calculateMaxHealth();
                break;
            case Stats.DEFENSE:
                DefenseStat += increaseValue;
                break;
            case Stats.STAMINA:
                StaminaStat += increaseValue;
                calculateMaxStamina();
                break;
            case Stats.PHYSICAL_STRENGTH:
                PhysicalStrengthStat += increaseValue;
                break;
            case Stats.MAGICAL_STRENGTH:
                MagicalStrengthStat += increaseValue;
                break;
        }
    }
}
