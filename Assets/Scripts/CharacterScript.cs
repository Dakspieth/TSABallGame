using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float health;
    public bool hasSword;
    public GameObject swordPrefab;

    float angle;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * Random.Range(-10, 10) + transform.up * Random.Range(0, 3), ForceMode2D.Impulse);

        if (hasSword)
        {
            Instantiate(swordPrefab, gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, angle);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.transform.position.y > transform.position.y && col.gameObject.tag != gameObject.tag && col.gameObject.tag != "Border")
        {
              
        }
        angle = (rb.linearVelocity.x)/-15;
    }
    
}
