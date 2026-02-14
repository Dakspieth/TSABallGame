using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostScript : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterScript cs;
    float speed;
    float cooldownTime;
    float timer;
    
    Button buttonLeft, buttonRight;
    TMP_Text heading;
    string baseText;

    void Start()
    {
        if(gameObject.layer != 0)
        {
            Destroy(this);
        }
        rb = GetComponent<Rigidbody2D>();
        cs = GetComponent<CharacterScript>();
        speed = cs.boostSpeed;
        cooldownTime = cs.boostCooldown;
        cs.boostPanel.SetActive(true);
        buttonLeft = GameObject.FindWithTag("LeftBoost").GetComponent<Button>();
        buttonLeft.onClick.AddListener(LeftBoost);
        buttonRight = GameObject.FindWithTag("RightBoost").GetComponent<Button>();
        buttonRight.onClick.AddListener(RightBoost);
        heading = buttonLeft.transform.parent.GetComponentInChildren<TMP_Text>();
        baseText = heading.text;
    }

    public void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            heading.text = baseText + " (" + (Mathf.Round(10 * timer)/10).ToString("N1") + ")";
        } else
        {
            heading.text = baseText;
        }
    }

    public void LeftBoost()
    {
        rb.AddForce(Vector2.right * -speed, ForceMode2D.Impulse);
        StartCoroutine(Cooldown());
    }

    public void RightBoost()
    {
        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        buttonLeft.interactable = false;
        buttonRight.interactable = false;
        timer = cooldownTime;
        yield return new WaitForSeconds(cooldownTime);
        buttonLeft.interactable = true;
        buttonRight.interactable = true;

    }
}
