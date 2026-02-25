using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayCutscenes : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] clips;
    public RawImage vidImage;
    public Button opponentBtn;
    public Button cutsceneSkip;
    bool buttonDestroyed = false;
    float t = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        opponentBtn.interactable = false;

        if(clips.Length > 1)
        {
        videoPlayer.loopPointReached += playNextClip;            
        } else
        {            
        videoPlayer.loopPointReached += transitionOut;
        }
        videoPlayer.clip = clips[0];
        videoPlayer.Play();
    }
    IEnumerator lerpAlpha(float start, float end, bool endAfter)
    {
        t = 0;
        while(t < 1)
        {
            float alpha = Mathf.Lerp(start, end, t);
            vidImage.color = new Color(1, 1, 1, alpha);
            t += Time.deltaTime*2;
            yield return null;
        }
        vidImage.color = new Color(1, 1, 1, end);
        if (endAfter)
        {
            endPlayback();
        }
    }
    void playNextClip(VideoPlayer vp)
    {
        videoPlayer.clip = clips[1];
        videoPlayer.loopPointReached -= playNextClip;
        videoPlayer.loopPointReached += transitionOut;
        if(!buttonDestroyed)
        {
            Destroy(cutsceneSkip.gameObject);
            buttonDestroyed = true;
        }
        videoPlayer.Play();
    }
    void transitionOut(VideoPlayer vp)
    {
        if(!buttonDestroyed && cutsceneSkip != null)
        {
            Destroy(cutsceneSkip.gameObject);
            buttonDestroyed = true;
        }
        StartCoroutine(lerpAlpha(1,0, true));
    }
    void endPlayback()
    {
        videoPlayer.Stop();
        videoPlayer.enabled = false;
        Destroy(vidImage.gameObject);
        opponentBtn.interactable = true;
    }

    public void skipClick()
    {
        videoPlayer.frame = (long)videoPlayer.frameCount-1;
        buttonDestroyed = true;
        Destroy(cutsceneSkip.gameObject);
    }
}
