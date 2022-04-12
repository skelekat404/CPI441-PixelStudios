using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ImportantVariables : MonoBehaviour
{
    // *** Materials ***

    // Earth
    public int numWood = 0;
    public int numRock = 0;
    public int numWool = 0;

    // Marus
    public int numCoal = 0;
    public int numLavaCrystal = 0;

    // Vamia
    public int numPurpleCrystal = 0;
    public int numPurpleEssence = 0;

    public int numMoney = 0;

    //shop items that can be purchased
    public bool hasWarpDrive = false;
    public bool hasRocketBoots = false;
    public bool hasScuba = false;
    public bool hasLavaWalk = false;
    public bool hasJetpack = false;
    //public int numHealthPotions = 0;

    public float playerHealth = 100f;
    // *** Speed Upgrade Items getters and setters ***
    public void setWarpDrive(bool warpDrive)
    {
        hasWarpDrive = warpDrive;
    }
    public bool getWarpDrive()
    {
        return hasWarpDrive;
    }
    public void setRocketBoots(bool rocketBoots)
    {
        hasRocketBoots = rocketBoots;
    }
    public bool getRocketBoots()
    {
        return hasRocketBoots;
    }

    // *** Planet Items getters and setters ***
    public void setScuba(bool scuba)
    {
        hasScuba = scuba;
    }
    public bool getScuba()
    {
        return hasScuba;
    }

    public void setLavaWalk(bool lavaWalk)
    {
        hasLavaWalk = lavaWalk;
    }
    public bool getLavaWalk()
    {
        return hasLavaWalk;
    }

    public void setJetpack(bool jetpack)
    {
        hasJetpack = jetpack;
    }
    public bool getJetpack()
    {
        return hasJetpack;
    }
}
