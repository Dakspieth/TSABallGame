using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class PlayCutscenes : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] clips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        videoPlayer.Play();
    }

    void endPlayback(VideoPlayer vp)
    {
        videoPlayer.Stop();
        videoPlayer.enabled = false;
    }
}
