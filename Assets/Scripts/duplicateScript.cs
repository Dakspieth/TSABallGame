using TMPro;
using UnityEngine;

public class duplicateScript : MonoBehaviour
{
    CharacterScript cs;
    GameObject ballPrefab;
    GameObject parent;

    int health, damage;
    float size;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cs = gameObject.GetComponent<CharacterScript>();
        health = cs.duplicateHealth;
        damage = cs.duplicateDamage;
        size = cs.duplicateSize;

        if (gameObject.layer == 0)
        {
            parent = new GameObject();
            parent.tag = gameObject.tag;
            parent.name = "parent" + gameObject.tag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cs.health <= 0)
        {
            Destroy(parent);
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != gameObject.tag && col.gameObject.tag != "Border")
        {
            if (gameObject.layer == 0)
            {
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up * Random.Range(-1, 1) + transform.right * Random.Range(-1, 1), 50);
                float theta = Random.Range(0, 360);
                float x = Mathf.Cos(theta) * (transform.localScale.x / 2 + size / 2);
                float y = Mathf.Sin(theta) * (transform.localScale.x / 2 + size / 2);
                ballPrefab = Instantiate(gameObject, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity, parent.transform);
                ballPrefab.GetComponent<CharacterScript>().health = health;
                ballPrefab.transform.localScale *= size;
                //ballPrefab.GetComponent<Rigidbody2D>().mass = 0.2f;
                //ballPrefab.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
                ballPrefab.layer = 6;
            }
            if (gameObject.layer == 6)
            {
                StartCoroutine(cs.HitStop(col.gameObject));
                col.gameObject.GetComponent<CharacterScript>().health -= damage;
                col.gameObject.GetComponentInChildren<TextMeshPro>().text = "" + col.gameObject.GetComponent<CharacterScript>().health;
                col.gameObject.GetComponent<CharacterScript>().turnRed();
            }
        }
    }

}
