using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{
    public float health;
    public float speed;

    [Header("Sword")]
    public bool hasSword; // spinning sword that does damage on hit
    public int swordDamage;
    public float swordSpeed;
    public GameObject swordPrefab;

    [Header("Unarmed")]
    public bool unarmed; // does damage when ball hits ball
    public int unarmedDamage;
    public bool unarmedSpeedOnHit;

    [Header("Duplicator")]
    GameObject ballPrefab;
    GameObject parent;
    public bool duplicator;
    public int duplicateHealth;
    public float duplicateSize;
    public int duplicateDamage;
    

    float angle;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * Random.Range(-5, 5) + transform.up * Random.Range(0, 3), ForceMode2D.Impulse);

        if (hasSword)
        {
            Instantiate(swordPrefab, gameObject.transform);
        }

        if(duplicator && gameObject.layer==0)
        {
            parent = new GameObject();
            parent.tag = gameObject.tag;
            parent.name = "parent" + gameObject.tag;
        }
        
    }

    void Update()
    {
        transform.Find("sprite").eulerAngles += new Vector3(0, 0, angle);
        if(health <= 0)
        {
            Destroy(gameObject);
            if (duplicator)
            {
                Destroy(parent);
            }
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
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != gameObject.tag && col.gameObject.tag != "Border") // rules for on collision if unarmed is enabled
        {
            if (unarmed)
            {
                StartCoroutine(HitStop());
                col.gameObject.GetComponent<CharacterScript>().health -= unarmedDamage;
                if (unarmedSpeedOnHit)
                {
                    speed += Mathf.Pow(10,-(speed+0.7f));
                    rb.mass = rb.mass>0.1f ? rb.mass/1.5f : 0.1f;
                }
            }
            
            if(duplicator)
            {
                if(gameObject.layer == 0)
                {
                    //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up * Random.Range(-1, 1) + transform.right * Random.Range(-1, 1), 50);
                    float theta = Random.Range(0, 360);
                    float x = Mathf.Cos(theta)*(transform.localScale.x/2+duplicateSize/2);
                    float y = Mathf.Sin(theta)*(transform.localScale.x/2+duplicateSize/2);
                    ballPrefab = Instantiate(gameObject, new Vector2(transform.position.x+x, transform.position.y+y), Quaternion.identity, parent.transform);
                    ballPrefab.GetComponent<CharacterScript>().health = duplicateHealth;
                    ballPrefab.transform.localScale *= duplicateSize;
                    //ballPrefab.GetComponent<Rigidbody2D>().mass = 0.2f;
                    //ballPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
                    ballPrefab.layer = 6;
                }
                if (gameObject.layer == 6)
                {
                    StartCoroutine(HitStop());
                    col.gameObject.GetComponent<CharacterScript>().health -= unarmedDamage;
                }
            }

        }
        angle = (rb.linearVelocity.x)/-7.5f;
    }

    public IEnumerator HitStop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1;
    }
}
