using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using TMPro;

public class CharacterScript : MonoBehaviour
{
    
    public float health;
    public float speed;
    public float textX;
    float angle;
    [Header("Sword")]
    public bool sword; // spinning sword that does damage on hit
    public int swordDamage;
    public float swordSpeed;
    public GameObject swordPrefab;

    [Header("Unarmed")]
    public bool unarmed; // does damage when ball hits ball
    public int unarmedDamage;
    public bool unarmedSpeedOnHit;

    [Header("Duplicator")]
    public bool duplicator;
    public int duplicateHealth;
    public float duplicateSize;
    public int duplicateDamage;

    [Header("Lifesteal")]
    public bool lifesteal;
    public int lifestealHealth;
    [Range(0,1)]
    public float lifestealChance;
    public int lifestealAmount;
    public int lifestealDamage;
    public float lifestealSpeed;
    public GameObject lifestealPrefab;
    
    [Header("BoostUpDownPowerup")]
    public bool boostUpDownPowerup;
    public float boostUpDownCooldown;
    public float boostUpDownSpeed;  
    public GameObject boostUpDownPanel;
    
    [Header("BoostLeftRightPowerup")]
    public bool boostLeftRightPowerup;
    public float boostLeftRightCooldown;
    public float boostLeftRightSpeed;  
    public GameObject boostLeftRightPanel;



    [Header("HealPowerup")]
    public bool healPowerup;
    public float healCooldown;
    public int healAmount;
    public GameObject healPanel;

    [Header("DamagePowerup")]
    public bool damagePowerup;
    public float damageTime;
    public float damageCooldown;
    public int damageMult;
    public GameObject damagePanel;

    [HideInInspector]
    public Rigidbody2D rb;
    float[] yPositions = {175, -175};
    int powerupNum = 0;
    TMP_Text text;
    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GetComponentInChildren<TextMeshPro>(true).gameObject.SetActive(true);
        GetComponentInChildren<TextMeshPro>().GetComponentInChildren<SpriteRenderer>().sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        text = GetComponentInChildren<TMP_Text>();

        rb = transform.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * Random.Range(-5, 5) + transform.up * Random.Range(0, 3), ForceMode2D.Impulse); // initial randomized velocities
        
        // if you want to make these lines better without all the if statements be my guest
        if (sword)
        {
            Instantiate(swordPrefab, gameObject.transform);
        }
        if (unarmed)
        {
            gameObject.AddComponent<unarmedScript>();
        }
        if (duplicator && gameObject.layer == 0)
        {
            gameObject.AddComponent<duplicateScript>();
        }
        if(lifesteal)
        {
            Instantiate(lifestealPrefab, gameObject.transform);
        }

        // Start adding powerups, max of 2 (change max by adding to the yPositions list)
        if(gameObject.layer == 0)
        {
            if(boostUpDownPowerup && powerupNum < yPositions.Length) 
            {
                gameObject.AddComponent<BoostUpDownScript>();
                boostUpDownPanel.transform.localPosition = new Vector2(boostUpDownPanel.transform.position.x, yPositions[powerupNum]);
                powerupNum++;
            } else
            {
                Destroy(boostUpDownPanel);
            }

            if(boostLeftRightPowerup && powerupNum < yPositions.Length)
            {
                gameObject.AddComponent<BoostLeftRightScript>();
                boostLeftRightPanel.transform.localPosition = new Vector2(boostLeftRightPanel.transform.position.x, yPositions[powerupNum]);
                powerupNum++;
            } else
            {
                Destroy(boostLeftRightPanel);
            }

            if (healPowerup && powerupNum < yPositions.Length)
            {
                gameObject.AddComponent<HealScript>();
                healPanel.transform.localPosition = new Vector2(healPanel.transform.position.x, yPositions[powerupNum]);
                powerupNum++;
            } else
            {
                Destroy(healPanel);
            }

            if(damagePowerup && powerupNum < yPositions.Length)
            {
                gameObject.AddComponent<DamageMultScript>();
                damagePanel.transform.localPosition = new Vector2(damagePanel.transform.position.x, yPositions[powerupNum]);
                powerupNum++;
            } else
            {
                Destroy(damagePanel);
            } 
        }
        

        
    }

    void LateUpdate()
    {
        transform.Find("sprite").eulerAngles += new Vector3(0, 0, angle);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        
        if (rb.linearVelocity.magnitude < speed)
        {
            rb.linearVelocity *= speed;
        }
        if (rb.linearVelocity.magnitude > speed + 10f) 
        {
            Vector2.ClampMagnitude(rb.linearVelocity, speed+10f);
        }
        
        GetComponentInChildren<TextMeshPro>().GetComponentInParent<RectTransform>().position = new Vector3(textX, transform.position.y, 0);
        text.text = health.ToString();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        angle = rb.linearVelocity.x/-7.5f;
    }

    public IEnumerator HitStop(GameObject hitGameobject)
    {
        Time.timeScale = 0;
        SpriteRenderer sprite = hitGameobject.GetComponentInChildren<SpriteRenderer>();
        yield return new WaitForSecondsRealtime(0.01f);
        cam.orthographicSize = 2.75f;
        bool changeColor = false;
        if (hitGameobject != null && hitGameobject.GetComponent<CharacterScript>().health > 0)
        {
            sprite.color = new Color(1, 0, 0);
            changeColor = true;
        }
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(0.15f/10);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 0.02f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, 0.02f);
            if (changeColor)
            {
                //GB = green/blue
                float currentGB = sprite.color.g;
                currentGB = Mathf.Lerp(currentGB, 1, 0.02f);
                sprite.color = new Color(1, currentGB, currentGB);
            }
        }
        Time.timeScale = 1;
        cam.orthographicSize = 3;
        if (changeColor)
        {
            sprite.color = Color.white;
        }
    }

}
