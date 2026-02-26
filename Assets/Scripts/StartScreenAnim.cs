using UnityEngine;
using TMPro;

public class StartScreenAnim : MonoBehaviour
{
    string[] startTextList = {"3", "2", "1", "Begin!"};
    public TMP_Text startText;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public void StartAnim(int num)
    {
        startText.text = startTextList[num];
    }

    public void closeStartScreen()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    public void playSound(int soundNum)
    {
        //audioSource.pitch = 1;//1.5f;//pitchChange;
        audioSource.clip = audioClips[soundNum];
        audioSource.Play();
    }
}
