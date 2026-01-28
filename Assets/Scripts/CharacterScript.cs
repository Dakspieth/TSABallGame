using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using TMPro;

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
    

    public float angle;
    public float startX;
    [HideInInspector]
    public Rigidbody2D rb;

    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

        GetComponentInChildren<TextMeshPro>().GetComponentInParent<RectTransform>().position = new Vector3(startX, transform.position.y, 0);

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        angle = (rb.linearVelocity.x)/-7.5f;
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

    public void turnRed()
    {

    }
}
