using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Button PlayBtn, HowToPlayBtn;
    public Animator animator;
    bool howToPlay = false;

    //25343F
    Color bgColor = new Color(0.145098039f, 0.203921569f, 0.247058824f);
    //EAEFEF
    Color textColor = new Color(0.917647059f, 0.937254902f, 0.937254902f);
    bool on = false;

    public void SwitchColor(Button button)
    {
        on = !on;
        TMP_Text[] text = button.GetComponentsInChildren<TMP_Text>();
        if (on)
        {
            button.gameObject.GetComponent<Image>().color = textColor;
            foreach (TMP_Text textObj in text)
            {
                textObj.color = bgColor;
            }
        }
        else
        {
            button.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            foreach (TMP_Text textObj in text)
            {
                textObj.color = textColor;
            }
        }
    }

    public void HowToPlayClick()
    {
        howToPlay = !howToPlay;
        on = !on;
        if(howToPlay)
        {
            animator.SetTrigger("GuidePress");
        } else
        {
            animator.SetTrigger("GuideUnpress");
        }
    }

    public void PlayClick()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
