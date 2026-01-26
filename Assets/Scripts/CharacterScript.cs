using UnityEngine;
using TMPro;
public class CharacterScript : MonoBehaviour
{
    public float health;
    public float speed;
    public bool hasSword; // spinning sword that does damage on hit
    public int swordDamage;
    public float swordSpeed;
    public GameObject swordPrefab;
    public bool unarmed; // does damage when ball hits ball
    public int unarmedDamage;
    public bool unarmedSpeedOnHit;
    TextMeshProUGUI healthTxt;

    float angle;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthTxt = GetComponentInChildren<TextMeshProUGUI>();
        rb = transform.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * Random.Range(-5, 5) + transform.up * Random.Range(0, 3), ForceMode2D.Impulse);

        if (hasSword)
        {
            Instantiate(swordPrefab, gameObject.transform);
        }
    }

    void Update()
    {
        transform.Find("sprite").eulerAngles += new Vector3(0, 0, angle);
        if(health <= 0)
        {
            gameObject.SetActive(false);
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
        if (unarmed && col.gameObject.tag != gameObject.tag && col.gameObject.tag != "Border") // rules for on collision if unarmed is enabled
        {
            col.gameObject.GetComponent<CharacterScript>().health -= unarmedDamage;
            col.gameObject.GetComponentInChildren<TextMeshPro>().text = "" + col.gameObject.GetComponent<CharacterScript>().health;
            if (unarmedSpeedOnHit)
            {
                speed += Mathf.Pow(10,-(speed+0.7f));
                rb.mass = rb.mass>0.1f ? rb.mass/1.5f : 0.1f;
            }
            
        }
        angle = (rb.linearVelocity.x)/-7.5f;
    }

}
