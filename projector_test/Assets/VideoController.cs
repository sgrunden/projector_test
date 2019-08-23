using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoController : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    List<string> videoNames = new List<string>() { "Choose a Video:"};
    
    public VideoPlayer vp;
    public VideoClip[] videos;

    private string videoName ="";

    public void Dropdown_IndexChanged(int index)
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

    void Start()
    {
        PopulateVideoList();
    }

    void PopulateVideoList()
    {
        foreach (VideoClip v in videos)
        {
            videoNames.Add(v.name);
        }
        dropdown.AddOptions(videoNames);
    }

}
