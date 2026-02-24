using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerVars
{
    public static bool fromPreMatch = false;
    public static bool playedTheseGamesBefore = false;
    public static bool sword = false;
    public static bool unarmed = false;
    public static bool duplicate = false;
    public static bool lifesteal = false;
    public static bool boostLR = false;
    public static bool boostUD = false;
    public static bool damageMult = false;
    public static bool heal = false;

    public static string loser = null; 
    public static string[] powerupList = {null, null};
    public static int numPowerups = 0;
    public static bool hitstopping = false;
    public static List<GameObject> healthTextList = new List<GameObject>();
    public static int maxLvl = 1;
}
