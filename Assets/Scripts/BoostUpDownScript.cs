using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostUpDownScript : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterScript cs;
    float speed;
    float cooldownTime;
    float timer;
    
    Button buttonUp, buttonDown;
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
        speed = cs.boostUpDownSpeed;
        cooldownTime = cs.boostUpDownCooldown;
        cs.boostUpDownPanel.SetActive(true);
        buttonUp = GameObject.FindWithTag("UpBoost").GetComponent<Button>();
        buttonUp.onClick.AddListener(UpBoost);
        buttonDown = GameObject.FindWithTag("DownBoost").GetComponent<Button>();
        buttonDown.onClick.AddListener(DownBoost);
        heading = buttonDown.transform.parent.GetComponentInChildren<TMP_Text>();
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

    public void UpBoost()
    {
        cs.audioSources[2].Play();
        //rb.AddForce(new Vector2(0, rb.linearVelocityY) + Vector2.up * speed, ForceMode2D.Impulse);
        rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Abs(rb.linearVelocityY) + speed);
        StartCoroutine(Cooldown());
    }

    public void DownBoost()
    {
        cs.audioSources[2].Play();
        //rb.AddForce(new Vector2(0, -rb.linearVelocityY) + Vector2.up * -speed, ForceMode2D.Impulse);
        rb.linearVelocity = new Vector2(rb.linearVelocityX, -Mathf.Abs(rb.linearVelocityY) - speed);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        buttonUp.interactable = false;
        buttonDown.interactable = false;
        timer = cooldownTime;
        yield return new WaitForSeconds(cooldownTime);
        buttonUp.interactable = true;
        buttonDown.interactable = true;

    }
}
