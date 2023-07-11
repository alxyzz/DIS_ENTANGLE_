using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NextLevelCaller : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        // Subscribe to the "loopPointReached" event of the VideoPlayer
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        // This method will be called when the video ends
        Debug.Log("Video ended");

        // Call your desired method here
        YourMethod();
    }

    private void YourMethod()
    {
        GameManager.Instance.ChangeLevel();
    }
}
