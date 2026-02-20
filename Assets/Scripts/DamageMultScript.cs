using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageMultScript : MonoBehaviour
{
    CharacterScript cs;
    float multAmount;
    float damageTime;
    float cooldownTime;
    Button damageButton;
    float timer;
    TMP_Text heading;
    string baseText;
    int duplicateDamage;
    void Start()
    {
        if(gameObject.layer != 0)
        {
            Destroy(this);
        }
        cs = GetComponent<CharacterScript>();
        cs.damagePanel.SetActive(true);
        multAmount = cs.damageMult;
        damageTime = cs.damageTime;
        cooldownTime = cs.damageCooldown;
        damageButton = GameObject.FindWithTag("DamageButton").GetComponent<Button>();
        damageButton.onClick.AddListener(DamageMult);
        heading = damageButton.transform.parent.GetComponentInChildren<TMP_Text>();
        baseText = heading.text;
        duplicateDamage= cs.duplicateDamage;
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

    public void DamageMult()
    {
        if (cs.sword)
        {
            GetComponentInChildren<swordScript>().damage*=2;
        }
        if(cs.unarmed)
        {
            GetComponent<unarmedScript>().damage *= 2;
        }
        if(cs.duplicator)
        {
            GetComponent<duplicateScript>().damage = duplicateDamage*2;
            cs.duplicateDamage = duplicateDamage *2;
            foreach(Transform child in GetComponent<duplicateScript>().parent.transform)
            {
                child.gameObject.GetComponent<duplicateScript>().damage = duplicateDamage * 2;
            }
        }
        if (cs.lifesteal)
        {
            GetComponentInChildren<lifestealScript>().damage *= 2;
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        damageButton.interactable = false;
        timer = cooldownTime;
        yield return new WaitForSeconds(damageTime);
        if (cs.sword)
        {
            GetComponentInChildren<swordScript>().damage/=2;
        }
        if(cs.unarmed)
        {
            GetComponent<unarmedScript>().damage /= 2;
        }
        if(cs.duplicator)
        {
            GetComponent<duplicateScript>().damage = duplicateDamage;
            cs.duplicateDamage = duplicateDamage;
            foreach(Transform child in GetComponent<duplicateScript>().parent.transform)
            {
                child.gameObject.GetComponent<duplicateScript>().damage = duplicateDamage;
            }
        }
        if (cs.lifesteal)
        {
            GetComponentInChildren<lifestealScript>().damage /= 2;
        }
        yield return new WaitForSeconds(cooldownTime-damageTime > 0 ? cooldownTime-damageTime : 0);

        damageButton.interactable = true;
    }

}
