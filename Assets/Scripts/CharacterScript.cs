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
    public bool duplicator;
    public int duplicateHealth;
    public float duplicateSize;
    public int duplicateDamage;
    

    float angle;
    [HideInInspector]
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * Random.Range(-5, 5) + transform.up * Random.Range(0, 3), ForceMode2D.Impulse);

        if (hasSword)
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
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        angle = (rb.linearVelocity.x)/-7.5f;
    }

    public IEnumerator HitStop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1;
    }
}
