using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;
using System.Linq;
using System.Collections.Generic;
using Unity.Hierarchy;

public class MainMenuScript : MonoBehaviour
{
    public Button PlayBtn, HowToPlayBtn, guideNextBtn;
    public Animator animator;
    bool howToPlay = false;

    //25343F
    Color bgColor = new Color(0.145098039f, 0.203921569f, 0.247058824f);
    //EAEFEF
    Color textColor = new Color(0.917647059f, 0.937254902f, 0.937254902f);
    [HideInInspector]
    public bool on = false;
    bool guideNext = false;
    bool play = false;
    List<bool> levelBools = new List<bool> {false, false, false, false, false};
    List<int> levelNums = new List<int> {1, 3, 5, 7, 9};
    public List<Button> levelBtns = new List<Button> {};
    public AudioSource audioSource;

    void Start()
    {
        for(int i = 4; i>PlayerVars.maxLvl-1; i--)
        {
            Destroy(levelBtns[i].gameObject);
            levelBtns.RemoveAt(i);
            levelBools.RemoveAt(i);
        }
    } 
    public void SwitchColor(Button button)
    {
        int num = 0;
        foreach(Button item in levelBtns)
        {
            if(item == button)
            {
                num++;
                break;
            }
        }
        if(num==0)
        {
            on = !on; 
        }

        TMP_Text[] text = button.GetComponentsInChildren<TMP_Text>();
        if (on)
        {

            button.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            foreach (TMP_Text textObj in text)
            {
                textObj.color = textColor;
            }
            if(button == PlayBtn || button == HowToPlayBtn)
            {
                button.gameObject.GetComponent<Image>().color = textColor;
                foreach (TMP_Text textObj in text)
                {
                    textObj.color = bgColor;
                }
            }
        }
        else
        {
            button.gameObject.GetComponent<Image>().color = textColor;
            foreach (TMP_Text textObj in text)
            {
                textObj.color = bgColor;
            }
            if(button == PlayBtn || button == HowToPlayBtn)
            {
                button.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                foreach (TMP_Text textObj in text)
                {
                    textObj.color = textColor;
                }
            }
        }

        for (int i = 0; i < levelBtns.Count; i++){
            if(button == levelBtns[i])
            {
                if (levelBools[i])
                {
                    button.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    foreach (TMP_Text textObj in text)
                    {
                        textObj.color = textColor;
                    }
                } else
                {
                    button.gameObject.GetComponent<Image>().color = textColor;
                    foreach (TMP_Text textObj in text)
                    {
                        textObj.color = bgColor;
                    }
                }
            }
        }
        
    }
    public void GuideNextClick()
    {
        guideNext = !guideNext;
        if (guideNext)
        {
            animator.SetTrigger("GuideNextPress");
        } else
        {
            animator.SetTrigger("GuideNextUnpress");
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
        play = !play;
        on = !on;
        if(play)
        {
            animator.SetTrigger("PlayPress");
        } else
        {
            animator.SetTrigger("PlayUnpress");
        }

        for(int i = 0; i < levelBtns.Count; i++)
        {
            levelBools[i] = false;
        }
    }

    public void LevelHover(int level)
    {
        levelBools[level-1] = !levelBools[level-1];

    }
    public void LevelClick(int level)
    {
        switch(level)
        {
            case 1:
                SceneManager.LoadSceneAsync(levelNums[0]);
                break;
            case 2:
                SceneManager.LoadSceneAsync(levelNums[1]);
                break;
            case 3:
                SceneManager.LoadSceneAsync(levelNums[2]);
                break;
            case 4:
                SceneManager.LoadSceneAsync(levelNums[3]);
                break;
            case 5:
                SceneManager.LoadSceneAsync(levelNums[4]);
                break;
            default:
                break;
            
        }
    }
    
}
