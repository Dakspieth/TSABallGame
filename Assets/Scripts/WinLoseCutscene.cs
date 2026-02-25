using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class WinLoseCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage vidImage;
    public VideoClip[] clips;
    public Button moveOnBtn;
    float t = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        //moveOnBtn.interactable = false;
        moveOnBtn.transform.parent.gameObject.SetActive(false);
        videoPlayer.loopPointReached += transitionOut;            
        videoPlayer.clip = PlayerVars.loser == "Player" ? clips[0] : clips[1]; // clips 0 means player lost
        vidImage.gameObject.SetActive(true);
        StartCoroutine(lerpAlpha(0, 1, false));
        videoPlayer.Play();
    }
    //void FixedUpdate()
    IEnumerator lerpAlpha(float start, float end, bool endAfter)
    {
        t = 0;
        //videoPlayer.Pause();        
        while(t < 1)
        {
            float alpha = Mathf.Lerp(start, end, t);
            vidImage.color = new Color(1, 1, 1, alpha);
            t += Time.deltaTime*2;
            //print(vidImage.color.a);
            yield return null;
        }
        vidImage.color = new Color(1, 1, 1, end);
        if (endAfter)
        {
            endPlayback();
        }
        //videoPlayer.Play();
    }
    void transitionOut(VideoPlayer vp)
    {
        moveOnBtn.transform.parent.gameObject.SetActive(true);        
        StartCoroutine(lerpAlpha(1,0, true));
    }
    void endPlayback()
    {
        videoPlayer.Stop();
        //moveOnBtn.interactable = true;
        Destroy(vidImage.gameObject);
        Destroy(gameObject);
    }
}
