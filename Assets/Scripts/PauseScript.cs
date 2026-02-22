using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using JetBrains.Annotations;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    bool on = false;
    bool hover = false;
    public GameObject pausePanel;
    //25343F
    Color bgColor = new Color(0.145098039f, 0.203921569f, 0.247058824f);
    //EAEFEF
    Color textColor = new Color(0.917647059f, 0.937254902f, 0.937254902f);
    public void PauseClick()
    {
        StartCoroutine(waitForHitstop());
    }
    IEnumerator waitForHitstop()
    {
        yield return new WaitUntil(() => PlayerVars.hitstopping == false);
        on = !on;
        foreach(GameObject item in PlayerVars.healthTextList)
        {
            item.SetActive(!on);
        }
        Time.timeScale = on ? 0 : 1;
        pausePanel.SetActive(on);
    }

    public void MenuClick()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void SwitchColor(Button button)
    {
        hover = !hover;
        TMP_Text[] text = button.GetComponentsInChildren<TMP_Text>();
        if (hover)
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
}
