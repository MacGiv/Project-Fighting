using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Progress Data")]
public class ProgressData : ScriptableObject
{
    public ProgressLevel currentLevel;

    public int maxHealth;


    public ProgressLevel GetProgressLevel() { return currentLevel; }


    public void SetProgressLevel(ProgressLevel levelToChange)
    {
        currentLevel = levelToChange;
    }

    public void SetMaxHealth(int newHealthPoints)
    {
        maxHealth = newHealthPoints;
    }

}
