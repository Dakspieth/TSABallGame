using UnityEngine;
using TMPro;

public class StartScreenAnim : MonoBehaviour
{
    string[] startTextList = {"3", "2", "1", "Begin!"};
    public TMP_Text startText;
    public void StartAnim(int num)
    {
        startText.text = startTextList[num];
    }

    public void closeStartScreen()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
