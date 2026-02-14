using UnityEngine;
using UnityEngine.UI;

public class BoostScript : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 5;
    Button buttonLeft, buttonRight;

    void Start()
    {
        if(gameObject.layer != 0)
        {
            Destroy(this);
        }
        rb = GetComponent<Rigidbody2D>();
        buttonLeft = GameObject.FindWithTag("LeftBoost").GetComponent<Button>();
        buttonLeft.onClick.AddListener(LeftBoost);
        buttonRight = GameObject.FindWithTag("RightBoost").GetComponent<Button>();
        buttonRight.onClick.AddListener(RightBoost);
    }

    public void LeftBoost()
    {
        rb.AddForce(Vector2.right * -speed, ForceMode2D.Impulse);
    }

    public void RightBoost()
    {
        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }

}
