using UnityEngine;
using System.Collections;

public class lifestealScript : MonoBehaviour
{
    CharacterScript cs;
    float chance;
    int amount;
    [HideInInspector]
    public int damage;
    float speed;

    Transform parent, ball;
    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        parent = transform.parent;
        ball = parent.transform.parent;
        parent.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y);
        gameObject.tag = ball.tag;

        cs = ball.gameObject.GetComponent<CharacterScript>();
        chance = cs.lifestealChance;
        amount = cs.lifestealAmount;
        damage = cs.lifestealDamage;
        speed = cs.lifestealSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        parent.localEulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != gameObject.tag && col.gameObject.layer != 7 && col.gameObject.tag != "Border")
        {
            bool heal = Random.Range(0f,1f) < chance;
            if(heal)
            {
                StartCoroutine(HealHitStop(col.gameObject));
                cs.health += amount;
            }
            else
            {
                StartCoroutine(col.GetComponent<CharacterScript>().HitStop(col.gameObject));
            }
            col.GetComponent<CharacterScript>().health-=damage;
            speed *= -1;

        }

    }

    public IEnumerator HealHitStop(GameObject hitGameobject)
    {
        PlayerVars.hitstopping = true;
        Time.timeScale = 0;
        SpriteRenderer sprite = hitGameobject.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer ballSprite = ball.GetComponentInChildren<SpriteRenderer>();
        yield return new WaitForSecondsRealtime(0.01f);
        cam.orthographicSize = 2.75f;
        bool changeColor = false;
        if (hitGameobject != null && hitGameobject.GetComponent<CharacterScript>().health > 0)
        {
            sprite.color = new Color(1, 0, 0);
            ballSprite.color = new Color(0,1,0);
            changeColor = true;
        }
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(0.15f / 10);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 0.02f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, 0.02f);
            if (changeColor)
            {
                //GB = green/blue
                float currentGB = sprite.color.g;
                float currentRB = ballSprite.color.r;
                currentGB = Mathf.Lerp(currentGB, 1, 0.02f);
                currentRB = Mathf.Lerp(currentRB, 1, 0.02f);
                sprite.color = new Color(1, currentGB, currentGB);
                ballSprite.color = new Color(currentRB, 1, currentRB);
            }
        }
        Time.timeScale = 1;
        cam.orthographicSize = 3;
        if (changeColor)
        {
            sprite.color = Color.white;
            ballSprite.color = Color.white;
        }
        PlayerVars.hitstopping = false;
    }
}
