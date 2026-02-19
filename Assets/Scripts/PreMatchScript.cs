using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;
public class PreMatchScript : MonoBehaviour
{
    // ADADAD
    public GameObject abilities, powerups;

///////////////////////////////////////////////
    public Button unarmedButton;
    String unarmedDesc = "> hits on collision\n> 1 damage on hit";
    
    public Button swordButton;
    String swordDesc = "Spinning sword\n> 3 damage on hit";

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
    String healDesc = "when pressed:\n - heal 1 hp\n\n5 second cooldown";

    
    public TMP_Text heading;
    String heading1 = "Choose your ability";
    string heading2 = "Choose powerups";
    public TMP_Text description;
    String lastDesc = null;
    public Button nextButton, backButton;
    int numPowerups = 0;

    //25343F
    Color bgColor = new Color(0.145098039f, 0.203921569f, 0.247058824f);
    //EAEFEF
    Color textColor = new Color(0.917647059f, 0.937254902f, 0.937254902f);
    void Start()
    {
        PlayerVars.fromPreMatch = true;
        abilities.SetActive(true);
        powerups.SetActive(false);

        unarmedButton.interactable = true;
        swordButton.interactable = true;
        duplicateButton.interactable = true;
        lifestealButton.interactable = true;
        heading.text = heading1;
        description.text = "Choose an ability to see its description"; // initial text
        lastDesc = description.text;
        nextButton.GetComponentInChildren<TMP_Text>().text = "Next";
        nextButton.interactable = false;
        backButton.gameObject.SetActive(false);
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
        print(button.gameObject.GetComponent<Image>().color);
    }
    public void NextClick()
    {   
        if(heading.text == heading2)
        {
            SceneManager.LoadSceneAsync(0);
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

    public void BoostUDClick()
    {
        PlayerVars.boostUD = !PlayerVars.boostUD;
        numPowerups += PlayerVars.boostUD ? 1 : -1;
        MaxPowerups();
        if (PlayerVars.boostUD)
        {
            description.text = boostUDDesc;
            //boostUDButton.gameObject.GetComponent<Image>().color = selectedColor;
        } else
        {
            //boostUDButton.gameObject.GetComponent<Image>().color = Color.white; 
        }

        SwitchColor(PlayerVars.boostUD, boostUDButton);
        SwitchColor(PlayerVars.boostLR, boostLRButton);
        SwitchColor(PlayerVars.damageMult, damageButton);
        SwitchColor(PlayerVars.heal, healButton); 
    }

    public void BoostLRClick()
    {
        
        PlayerVars.boostLR = !PlayerVars.boostLR;
        numPowerups += PlayerVars.boostLR ? 1 : -1;
        MaxPowerups();
        if (PlayerVars.boostLR)
        {
            description.text = boostLRDesc;
            //boostLRButton.gameObject.GetComponent<Image>().color = selectedColor;
        } else
        {
            //boostLRButton.gameObject.GetComponent<Image>().color = Color.white; 
        }
        SwitchColor(PlayerVars.boostUD, boostUDButton);
        SwitchColor(PlayerVars.boostLR, boostLRButton);
        SwitchColor(PlayerVars.damageMult, damageButton);
        SwitchColor(PlayerVars.heal, healButton); 
    }

    public void DamageClick()
    {
        
        PlayerVars.damageMult = !PlayerVars.damageMult;
        numPowerups += PlayerVars.damageMult ? 1 : -1;
        MaxPowerups();
        if (PlayerVars.damageMult)
        {
            description.text = damageDesc;
            //damageButton.gameObject.GetComponent<Image>().color = selectedColor;
        } else
        {
            //damageButton.gameObject.GetComponent<Image>().color = Color.white;
        }
        SwitchColor(PlayerVars.boostUD, boostUDButton);
        SwitchColor(PlayerVars.boostLR, boostLRButton);
        SwitchColor(PlayerVars.damageMult, damageButton);
        SwitchColor(PlayerVars.heal, healButton);        
    }

    public void HealClick()
    {
        
        PlayerVars.heal = !PlayerVars.heal;
        numPowerups += PlayerVars.heal ? 1 : -1;
        MaxPowerups();       
        if (PlayerVars.heal)
        {
            description.text = healDesc;
            //healButton.gameObject.GetComponent<Image>().color = selectedColor;
        } else
        {
            //healButton.gameObject.GetComponent<Image>().color = Color.white;
        }
        SwitchColor(PlayerVars.boostUD, boostUDButton);
        SwitchColor(PlayerVars.boostLR, boostLRButton);
        SwitchColor(PlayerVars.damageMult, damageButton);
        SwitchColor(PlayerVars.heal, healButton); 
    }

    void MaxPowerups()
    {
        if(numPowerups >= 2)
        {
            boostLRButton.interactable = PlayerVars.boostLR;
            boostUDButton.interactable = PlayerVars.boostUD;
            damageButton.interactable = PlayerVars.damageMult;
            healButton.interactable = PlayerVars.heal;
        } else
        {
            boostLRButton.interactable = true;
            boostUDButton.interactable = true;
            damageButton.interactable = true;
            healButton.interactable = true;
        }
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

        SwitchColor(PlayerVars.unarmed, unarmedButton);
        SwitchColor(PlayerVars.sword, swordButton);
        SwitchColor(PlayerVars.duplicate, duplicateButton);
        SwitchColor(PlayerVars.lifesteal, lifestealButton);
    }
}
