using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayCutscenes : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] clips;
    public Button opponentBtn;
    public Button cutsceneSkip;
    bool buttonDestroyed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        opponentBtn.interactable = false;

        if(clips.Length > 1)
        {
        videoPlayer.loopPointReached += playNextClip;            
        } else
        {            
        videoPlayer.loopPointReached += endPlayback;
        }
        videoPlayer.Play();
    }

    void playNextClip(VideoPlayer vp)
    {
        videoPlayer.clip = clips[1];
        videoPlayer.loopPointReached -= playNextClip;
        videoPlayer.loopPointReached += endPlayback;
        if(!buttonDestroyed)
        {
            Destroy(cutsceneSkip.gameObject);
        }
        videoPlayer.Play();
    }

    void endPlayback(VideoPlayer vp)
    {
        videoPlayer.Stop();
        videoPlayer.enabled = false;
        opponentBtn.interactable = true;
    }

    public void skipClick()
    {
        videoPlayer.frame = (long)videoPlayer.frameCount-1;
        buttonDestroyed = true;
        Destroy(cutsceneSkip.gameObject);
    }
}
