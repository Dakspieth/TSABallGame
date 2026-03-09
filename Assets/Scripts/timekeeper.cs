using UnityEngine;

public class timekeeper : MonoBehaviour
{
    private float cd = 0;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //resets time scale to 1 after 3 second of being low just in case game gets frozen
        cd += Time.unscaledDeltaTime;
        if(Time.timeScale > 0.1f)
        {
            cd = 0;
        }

        if(cd > 3)
        {
            Time.timeScale = 1;
            cd = 0;
        }
    }
}