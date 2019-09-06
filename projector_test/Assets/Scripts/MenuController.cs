using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public VideoController vc;

    public Button play, pause, end;

    public TMPro.TMP_Dropdown dropdown;
    List<string> videoNames = new List<string>() { "Choose a Video:" };

    private string videoName = "";

    void Start()
    {
        PopulateVideoList();
        play.onClick.AddListener(vc.PlayVideo);
        pause.onClick.AddListener(vc.PauseVideo);
        end.onClick.AddListener(vc.EndVideo);
    }

    void PopulateVideoList() //Takes the array of VideoClips from the VideoController script and adds the name of each clip to the drop down list.
    {
        foreach (VideoClip v in vc.videos)
        {
            videoNames.Add(v.name);
        }
        dropdown.AddOptions(videoNames);
    }

    void Dropdown_IndexChanged(int index) //Changes the clip to be played according to the drop down menu selection
    {
        videoName = videoNames[index];
        foreach (VideoClip v in vc.videos)
        {
            if (videoName == v.name)
            {
                vc.vp.clip = v;
                vc.vp.SetTargetAudioSource(0, vc.vpAudio); //Sets the VideoPlayer audio source to the AudioSource gameobject
            }
        }
    }
}
