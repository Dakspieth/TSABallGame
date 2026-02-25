using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class CharacterScript : MonoBehaviour
{
    
    public float health;
    public int nextLevel;
    public int currentLvl;
    public float speed;
    public float textX;
    public GameObject powerupPanel;
    public Animator winLoseAnimator;
    public GameObject videoObj;
    
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
    Transform textTransform;
    Camera cam;
    bool winLoseBound = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if(gameObject.tag == "Player" && PlayerVars.fromPreMatch)
        {
            unarmed = PlayerVars.unarmed;
            sword = PlayerVars.sword;
            duplicator = PlayerVars.duplicate;
            lifesteal = PlayerVars.lifesteal;
            boostUpDownPowerup = PlayerVars.boostUD;
            boostLeftRightPowerup = PlayerVars.boostLR;
            damagePowerup = PlayerVars.damageMult;
            healPowerup = PlayerVars.heal;
            PlayerVars.healthTextList.Clear();  
        }
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GetComponentInChildren<TextMeshPro>(true).gameObject.SetActive(true);
        GetComponentInChildren<TextMeshPro>().GetComponentInChildren<SpriteRenderer>().sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        text = GetComponentInChildren<TMP_Text>();
        text.gameObject.name = text.gameObject.name + System.DateTime.Now+System.DateTime.Now.Millisecond;
        PlayerVars.healthTextList.Add(text.gameObject);
        textTransform = GetComponentInChildren<TextMeshPro>().GetComponentInParent<RectTransform>();
        rb = transform.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * Random.Range(3, 5) * (Random.Range(0,1) > 0.5 ? 1 : -1) + transform.up * Random.Range(1, 3), ForceMode2D.Impulse); // initial randomized velocities
        winLoseAnimator.gameObject.SetActive(false);
        PlayerVars.loser = null;
        // if you want to make these lines better without all the if statements be my guest
        if(gameObject.layer == 0){
            Time.timeScale = 0;
            if (sword)
            {
                Instantiate(swordPrefab, gameObject.transform);
            }
            if (unarmed)
            {
                gameObject.AddComponent<unarmedScript>();
            }
            if (duplicator)
            {
                gameObject.AddComponent<duplicateScript>();
            }
            if(lifesteal)
            {
                Instantiate(lifestealPrefab, gameObject.transform);
            }
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
        transform.GetComponentInChildren<SpriteRenderer>().gameObject.transform.eulerAngles += new Vector3(0, 0, Time.timeScale != 0 ? angle : 0);
        if(health <= 0)
        {
            PlayerVars.healthTextList.Remove(text.gameObject);
            if(gameObject.layer == 0){
                PlayerVars.loser = gameObject.tag;
                powerupPanel.SetActive(false);
            }
            Destroy(gameObject);
        }

    }
    void FixedUpdate()
    {
        
        /*if (rb.linearVelocity.magnitude < speed)
        {
            rb.linearVelocity *= speed;
        }
        if (rb.linearVelocity.magnitude > speed + 10f) 
        {
            Vector2.ClampMagnitude(rb.linearVelocity, speed+10f);
        }*/
        
        textTransform.position = new Vector3(textX, transform.position.y, 0);
        text.text = health.ToString();
        if(PlayerVars.loser != null && !winLoseBound)
        {
            if(PlayerVars.loser == "Enemy")
            {
                WinLose(true);
                winLoseBound = true;
            } else if(PlayerVars.loser == "Player")
            {
                WinLose(false);
                winLoseBound = true;
            }   
        }
        
    }

    void WinLose(bool win)
    {
        winLoseAnimator.gameObject.SetActive(true);
        Button button = winLoseAnimator.GetComponentInChildren<Button>(true);
        switch (win)
        {
            case true:
                winLoseAnimator.gameObject.GetComponentInChildren<TMP_Text>().text = "You win!";
                button.onClick.AddListener(WinOnClick);
                videoObj.SetActive(true);
                break;
            case false:
                winLoseAnimator.gameObject.GetComponentInChildren<TMP_Text>().text = "You lose";
                button.onClick.AddListener(LoseOnClick);
                videoObj.SetActive(true);
                break;
            
        }
    }
    void WinOnClick()
    {
        PlayerVars.maxLvl = PlayerVars.maxLvl < currentLvl ? currentLvl : PlayerVars.maxLvl;
        SceneManager.LoadSceneAsync(nextLevel);
    }
    void LoseOnClick()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);        
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        angle = rb.linearVelocity.x/-7.5f;
    }

    public IEnumerator HitStop(GameObject hitGameobject)
    {
        PlayerVars.hitstopping = true;
        Time.timeScale = 0;
        SpriteRenderer sprite = hitGameobject.GetComponentInChildren<SpriteRenderer>();
        yield return new WaitForSecondsRealtime(0.01f);
        cam.orthographicSize = 2.9f;
        bool changeColor = false;
        if (hitGameobject != null && hitGameobject.GetComponent<CharacterScript>().health > 0)
        {
            sprite.color = new Color(1, 0, 0);
            changeColor = true;
        }
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f/10);
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
        PlayerVars.hitstopping = false;
    }


}
