using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

/* This script contains an array of VideoClips and adds the name of each VideoClip to a list of strings called videoNames.

 * This list of names is then added to the drop down menu using the method PopulateVideoList().

 * When the user selects a video name via the drop down menu, it is then compared to the names of the VideoClips in the VideoClip
   array until it finds a match, and then that VideoClip is loaded into the VideoPlayer to be played upon a button click.

 * PlayButton(): Reactivates the vpSurface gameobject and plays the video.

 * EndButton(): Deactivates the vpSurface gameobject and stops the video.
 
 * SetVPProperties(): Sets the 
*/

public class VideoController : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    List<string> videoNames = new List<string>() { "Choose a Video:"};

    public Button play, pause, end;

    public GameObject vpSurface;
    public RenderTexture vpRenderTexture;

    public VideoPlayer vp;
    public VideoClip[] videos;

    private string videoName = "";

    public AudioSource vpAudio;

    void Start()
    {
        PopulateVideoList();
        play.onClick.AddListener(PlayButton);
        pause.onClick.AddListener(PauseButton);
        end.onClick.AddListener(EndButton);
    }

    public void Dropdown_IndexChanged(int index) //Changes the clip to be played according to the drop down menu selection
    {
        videoName = videoNames[index];
        foreach (VideoClip v in videos)
        {
            if (videoName == v.name) 
            {
                vp.clip = v;
            }
        }
    }

    void PopulateVideoList() //Adds the names of the videos to the drop down menu
    {
        foreach (VideoClip v in videos)
        {
            videoNames.Add(v.name);
        }
        dropdown.AddOptions(videoNames);
    }

    void PlayButton() //Controls Play button on-click actions
    {
        vp.Prepare();
        vpSurface.SetActive(true);
        vp.Play();
    }

   void PauseButton() //Controls Pause button on-click actions
    {
        vp.Pause();
    }

   void EndButton() //Controls End button on-click actions
    {
        vpSurface.SetActive(false);
        vp.Stop();
        vpRenderTexture.Release();
        
    }

    void SetVPProperties() //Sets the Audio Source and targetTexture for the Video Player
    {
        vp.targetTexture = vpRenderTexture;
        //vp.SetTargetAudioSource(vpAudio);
    }

}
