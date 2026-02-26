using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealScript : MonoBehaviour
{
    CharacterScript cs;
    int healAmount;
    float cooldownTime;
    Button healButton;
    float timer;
    TMP_Text heading;
    string baseText;
    void Start()
    {
        if(gameObject.layer != 0)
        {
            Destroy(this);
        }
        cs = GetComponent<CharacterScript>();
        cs.healPanel.SetActive(true);
        healAmount = cs.healAmount;
        cooldownTime = cs.healCooldown;
        healButton = GameObject.FindWithTag("HealButton").GetComponent<Button>();
        healButton.onClick.AddListener(Heal);
        heading = healButton.transform.parent.GetComponentInChildren<TMP_Text>();
        baseText = heading.text;
    }
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            heading.text = baseText + " (" + (Mathf.Round(10*timer)/10).ToString("N1") + ")";
        } else
        {
            heading.text = baseText;
        }
    }

    public void Heal()
    {
        cs.audioSources[2].Play();
        cs.health += healAmount;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        healButton.interactable = false;
        timer = cooldownTime;
        yield return new WaitForSeconds(cooldownTime);
        healButton.interactable = true;
    }

}
