using TMPro;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    //sigmaboy
    float rotSpeed;
    int damage;
    Transform parent;
    Transform ball;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent;
        ball = parent.transform.parent;
        parent.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y);
        gameObject.tag = ball.tag;
        damage = ball.GetComponent<CharacterScript>().swordDamage;
        rotSpeed = ball.GetComponent<CharacterScript>().swordSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.parent.localEulerAngles += new Vector3(0, 0, rotSpeed*Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != gameObject.tag && col.gameObject.layer != 7 && col.gameObject.tag != "Border")
        {
            StartCoroutine(col.GetComponent<CharacterScript>().HitStop(col.gameObject));
            if (col.gameObject.layer == 0)
            {
            rotSpeed *= -1;
            }
            col.GetComponent<CharacterScript>().health-=damage;
            col.gameObject.GetComponentInChildren<TextMeshPro>().text = "" + col.gameObject.GetComponent<CharacterScript>().health;
        }
    }

    
}
