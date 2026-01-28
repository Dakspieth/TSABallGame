using TMPro;
using UnityEngine;

public class unarmedScript : MonoBehaviour
{
    //sigmaboy
    CharacterScript cs;
    float damage;
    bool speedOnHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cs = gameObject.GetComponent<CharacterScript>();
        damage = cs.unarmedDamage;
        speedOnHit = cs.unarmedSpeedOnHit;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != gameObject.tag && col.gameObject.tag != "Border")
        {
            StartCoroutine(cs.HitStop(col.gameObject));
            col.gameObject.GetComponent<CharacterScript>().health -= damage;
            col.gameObject.GetComponentInChildren<TextMeshPro>().text = "" + col.gameObject.GetComponent<CharacterScript>().health;
            if (speedOnHit)
            {
                cs.speed += Mathf.Pow(10, -(cs.speed + 0.7f));
                cs.rb.mass = cs.rb.mass > 0.1f ? cs.rb.mass / 1.5f : 0.1f;
            }
        }

    }
}
