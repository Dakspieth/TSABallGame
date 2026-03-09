using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class PreMatchScript : MonoBehaviour
{
    // ADADAD
    public GameObject abilities, powerups;

///////////////////////////////////////////////
    public Button unarmedButton;
    String unarmedDesc = "> hits on collision\n> 1 damage on hit";
    
    public Button swordButton;
    String swordDesc = "Spinning sword\n> 2 damage on hit";

    public Button duplicateButton;
    String duplicateDesc = "creates duplicates on hit that have:\n> 1 hp\n> 1 damage on hit";
    
    public Button lifestealButton;
    String lifestealDesc = "spinning sword\n> on hit:\n - 1 damage\n - chance to\n   heal 1 hp";
///////////////////////////////////////////////

    Color selectedColor = new Color(0.75f, 1, 0.75f);
///////////////////////////////////////////////
    public Button boostUDButton;
    String boostUDDesc = "when pressed:\n - dash\n   up/down\n\n5 second cooldown";
    public Button boostLRButton;
    String boostLRDesc = "when pressed:\n - dash\n   left/right\n\n5 second cooldown";
    public Button damageButton;
    String damageDesc = "when pressed:\n - 2x damage\n   for 1 second\n\n5 second cooldown";
    public Button healButton;
    String healDesc = "when pressed:\n - heal 1 hp\n\n3 second cooldown";

    
    public TMP_Text heading;
    String heading1 = "Choose your ability";
    string heading2 = "Choose powerups";
    public TMP_Text description;
    String lastDesc = null;
    public Button nextButton, backButton;
    public GameObject opponentPanel;
    bool on;

    //25343F
    Color bgColor = new Color(0.145098039f, 0.203921569f, 0.247058824f);
    //EAEFEF
    Color textColor = new Color(0.917647059f, 0.937254902f, 0.937254902f);
    void Start()
    {
        PlayerVars.fromPreMatch = true;
        abilities.SetActive(true);
        powerups.SetActive(false);
        int currentLvl = (SceneManager.GetActiveScene().buildIndex + 1) / 2;
        PlayerVars.maxLvl = PlayerVars.maxLvl < currentLvl ? currentLvl : PlayerVars.maxLvl;

        unarmedButton.interactable = true;
        swordButton.interactable = true;
        duplicateButton.interactable = true;
        lifestealButton.interactable = true;
        heading.text = heading1;
        description.text = "Choose an ability to see its description"; // initial text
        lastDesc = description.text;
        nextButton.GetComponentInChildren<TMP_Text>().text = "Next";
        nextButton.interactable = PlayerVars.playedTheseGamesBefore;
        backButton.gameObject.SetActive(false);
        //print(PlayerVars.numPowerups);
        SwitchColor(PlayerVars.unarmed, unarmedButton);
        SwitchColor(PlayerVars.sword, swordButton);
        SwitchColor(PlayerVars.duplicate, duplicateButton);
        SwitchColor(PlayerVars.lifesteal, lifestealButton);

        SwitchColor(PlayerVars.boostUD, boostUDButton);
        SwitchColor(PlayerVars.boostLR, boostLRButton);
        SwitchColor(PlayerVars.damageMult, damageButton);
        SwitchColor(PlayerVars.heal, healButton); 
    }

    void SwitchColor(bool On, Button button) 
    {
        TMP_Text[] text = button.GetComponentsInChildren<TMP_Text>();
        if(On)
        {
            button.gameObject.GetComponent<Image>().color = textColor;
            foreach (TMP_Text textObj in text){
                textObj.color = bgColor;                
            }
        } else
        {
            button.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            foreach (TMP_Text textObj in text){
                textObj.color = textColor;                
            }
        }
    }
    public void SwitchColorHover(Button button)
    {
        on = !on;
        TMP_Text[] text = button.GetComponentsInChildren<TMP_Text>();
        if (on)
        {
            button.gameObject.GetComponent<Image>().color = textColor;
            foreach (TMP_Text textObj in text)
            {
                textObj.color = bgColor;
            }
        }
        else
        {
            button.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            foreach (TMP_Text textObj in text)
            {
                textObj.color = textColor;
            }
        }
    }
    public void NextClick(int scene)
    {   
        if(heading.text == heading2)
        {
            SceneManager.LoadSceneAsync(scene);
        }
        abilities.SetActive(false);
        powerups.SetActive(true);
        heading.text = heading2;
        backButton.gameObject.SetActive(true);
        nextButton.GetComponentInChildren<TMP_Text>().text = "Start";

        (lastDesc, description.text) = (description.text, lastDesc);
        
    }
    public void BackClick()
    {
        abilities.SetActive(true);
        powerups.SetActive(false);
        heading.text = heading1;
        backButton.gameObject.SetActive(false);
        nextButton.GetComponentInChildren<TMP_Text>().text = "Next";
        (lastDesc, description.text) = (description.text, lastDesc);
    }
    public void MenuClick()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void BoostUDClick()
    {
        PlayerVars.boostUD = !PlayerVars.boostUD;
        PlayerVars.numPowerups += PlayerVars.boostUD ? 1 : -1;
        description.text = boostUDDesc;
        
        if (PlayerVars.boostUD && PlayerVars.numPowerups <= 2)
        {
            PlayerVars.powerupList[PlayerVars.numPowerups-1] = "boostUD";
        }
        MaxPowerups("boostUD", PlayerVars.boostUD);

    }

    public void BoostLRClick()
    {
        
        PlayerVars.boostLR = !PlayerVars.boostLR;
        PlayerVars.numPowerups += PlayerVars.boostLR ? 1 : -1;
        description.text = boostLRDesc;
        if (PlayerVars.boostLR && PlayerVars.numPowerups <= 2)
        {
            PlayerVars.powerupList[PlayerVars.numPowerups-1] = "boostLR";
        }
        MaxPowerups("boostLR", PlayerVars.boostLR);

    }

    public void DamageClick()
    {
        
        PlayerVars.damageMult = !PlayerVars.damageMult;
        PlayerVars.numPowerups += PlayerVars.damageMult ? 1 : -1;
        description.text = damageDesc;
        if (PlayerVars.damageMult && PlayerVars.numPowerups <= 2)
        {
            PlayerVars.powerupList[PlayerVars.numPowerups-1] = "damage";
        }
        MaxPowerups("damage", PlayerVars.damageMult);

    }

    public void HealClick()
    {
        
        PlayerVars.heal = !PlayerVars.heal;
        PlayerVars.numPowerups += PlayerVars.heal ? 1 : -1;
        description.text = healDesc;
        if (PlayerVars.heal && PlayerVars.numPowerups <= 2)
        {
            PlayerVars.powerupList[PlayerVars.numPowerups-1] = "heal";
        }
        MaxPowerups("heal", PlayerVars.heal);       

    }

    void MaxPowerups(String current, bool on)
    {
        if (!on)
        {
            for(int i=0; i < PlayerVars.powerupList.Length; i++)
            {
                if(PlayerVars.powerupList[i] == current && i == 0)
                {
                    PlayerVars.powerupList[0] = PlayerVars.powerupList[1];
                    PlayerVars.powerupList[1] = null;
                } else if (PlayerVars.powerupList[i] == current)
                {
                    PlayerVars.powerupList[1] = null;
                }                        
            }        
        }
        if(PlayerVars.numPowerups > 2)
        {
            // bad code, dont care
            switch (PlayerVars.powerupList[0])
            {
                case "boostUD":
                    PlayerVars.boostUD = false;
                    break;
                case "boostLR":
                    PlayerVars.boostLR = false;
                    break;
                case "damage":
                    PlayerVars.damageMult = false;
                    break;
                case "heal":
                    PlayerVars.heal = false;
                    break;
                default:
                    break;
            }
            PlayerVars.powerupList[0] = PlayerVars.powerupList[1];
            PlayerVars.powerupList[1] = current;
            PlayerVars.numPowerups = 2;
        }
       //print(String.Join(", ", PlayerVars.powerupList));
        SwitchColor(PlayerVars.boostUD, boostUDButton);
        SwitchColor(PlayerVars.boostLR, boostLRButton);
        SwitchColor(PlayerVars.damageMult, damageButton);
        SwitchColor(PlayerVars.heal, healButton); 
    }
    
    public void UnarmedClick()
    {
        PlayerVars.unarmed = true; // true
        PlayerVars.sword = false;
        PlayerVars.duplicate = false;
        PlayerVars.lifesteal = false;
        description.text = unarmedDesc;
        unarmedButton.interactable = false; // false
        swordButton.interactable = true;
        duplicateButton.interactable = true;
        lifestealButton.interactable = true;

        nextButton.interactable = true;
        PlayerVars.playedTheseGamesBefore = true;

        SwitchColor(PlayerVars.unarmed, unarmedButton);
        SwitchColor(PlayerVars.sword, swordButton);
        SwitchColor(PlayerVars.duplicate, duplicateButton);
        SwitchColor(PlayerVars.lifesteal, lifestealButton);

    }
    public void SwordClick()
    {
        PlayerVars.unarmed = false;
        PlayerVars.sword = true;  // true
        PlayerVars.duplicate = false;
        PlayerVars.lifesteal = false;
        description.text = swordDesc;
        unarmedButton.interactable = true;
        swordButton.interactable = false; // false 
        duplicateButton.interactable = true;
        lifestealButton.interactable = true;

        nextButton.interactable = true;
        PlayerVars.playedTheseGamesBefore = true;


        SwitchColor(PlayerVars.unarmed, unarmedButton);
        SwitchColor(PlayerVars.sword, swordButton);
        SwitchColor(PlayerVars.duplicate, duplicateButton);
        SwitchColor(PlayerVars.lifesteal, lifestealButton);
    }
    public void DuplicateClick()
    {
        PlayerVars.unarmed = false;
        PlayerVars.sword = false;
        PlayerVars.duplicate = true; // true
        PlayerVars.lifesteal = false;
        description.text = duplicateDesc;
        unarmedButton.interactable = true;
        swordButton.interactable = true;
        duplicateButton.interactable = false; // false
        lifestealButton.interactable = true;

        nextButton.interactable = true;
        PlayerVars.playedTheseGamesBefore = true;

        SwitchColor(PlayerVars.unarmed, unarmedButton);
        SwitchColor(PlayerVars.sword, swordButton);
        SwitchColor(PlayerVars.duplicate, duplicateButton);
        SwitchColor(PlayerVars.lifesteal, lifestealButton);
    }
    public void LifestealClick()
    {
        PlayerVars.unarmed = false;
        PlayerVars.sword = false;
        PlayerVars.duplicate = false;
        PlayerVars.lifesteal = true; // true
        description.text = lifestealDesc;
        unarmedButton.interactable = true;
        swordButton.interactable = true;
        duplicateButton.interactable = true;
        lifestealButton.interactable = false; // false

        nextButton.interactable = true;
        PlayerVars.playedTheseGamesBefore = true;

        SwitchColor(PlayerVars.unarmed, unarmedButton);
        SwitchColor(PlayerVars.sword, swordButton);
        SwitchColor(PlayerVars.duplicate, duplicateButton);
        SwitchColor(PlayerVars.lifesteal, lifestealButton);
    }
    public void OpponentClick()
    {
        Destroy(opponentPanel);
    }
}
