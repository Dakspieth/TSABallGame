using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.transform.position.y > transform.position.y && col.gameObject.tag != gameObject.tag && col.gameObject.tag != "Border")
        {
            health--;
            print(gameObject.name + " " + transform.position.y);
            print(col.gameObject.name + " " +  col.gameObject.transform.position.y);
        }
    }
}
