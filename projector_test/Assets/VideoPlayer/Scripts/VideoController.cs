using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using System.IO;

/*
 * PlayVideo(): Reactivates the vpSurface gameobject and plays the video.

 * EndVideo(): Deactivates the vpSurface gameobject, stops the video, and temporarily releases the rendertexture on the vpSurface
   so there is no remaining frame from a previous video.
*/
namespace Projector
{

    public class VideoController : MonoBehaviour
    {
        public GameObject vpSurface; //The quad that the VideoPlayer is drawn onto
        public RenderTexture vpRenderTexture;//The RenderTexture attached to the quad that the VideoPlayer is drawn onto

        public VideoPlayer vp;
        public VideoClip[] videos;
        public VideoClip currentClip;

        public string videoDirPath;

        [SerializeField] List<string> videoNameList;

        private string videoName = "";

        public AudioSource vpAudio;

        private void Awake()
        {
            GetVideoListFromDir();
        }

        private void OnDisable()
        {
            vpRenderTexture.Release();
        }

        public List<string> GetListOfVideoNames()
        {
            return videoNameList;
        }
        private void GetVideoListFromDir()
        {
            videoNameList = new List<string>();
            videoDirPath = Application.dataPath + "/VideoPlayer/Resources/Videos";
            var dirInfo = new DirectoryInfo(videoDirPath);
            FileInfo[] fileInfo = dirInfo.GetFiles();
            foreach (FileInfo file in fileInfo)
            {
                if(file.Extension == ".mp4")
                {
                    videoNameList.Add(file.Name);
                }
            }
        }

        public void SelectVideo(string videoName)
        {
            foreach (string name in videoNameList)
            {
                if (videoName == name)
                {
                    currentClip = Resources.Load<VideoClip>("Videos/" + videoName.Replace(".mp4", ""));
                    vp.clip = currentClip;
                    vp.SetTargetAudioSource(0, vpAudio); //Sets the VideoPlayer audio source to the AudioSource gameobject
                }
            }


        }


        public void PlayVideo() //Controls Play button on-click actions
        {
            vp.Prepare();
            vpSurface.SetActive(true);
            vp.Play();
        }

        public void PauseVideo() //Controls Pause button on-click actions
        {
            vp.Pause();
        }

        public void EndVideo() //Controls End button on-click actions
        {
            vpSurface.SetActive(false);
            vp.Stop();
            vpRenderTexture.Release();

        }

        public void SetTargetTexture() //Sets the targetTexture for the VideoPlayer to draw on
        {
            vp.targetTexture = vpRenderTexture;
        }

    }
}
