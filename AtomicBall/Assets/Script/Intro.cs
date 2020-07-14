using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject Logo;
    [SerializeField] GameObject IntroGO;

    void Update()
    {
        if (Logo.activeSelf)
        {
            if (Logo.GetComponent<VideoPlayer>().isPrepared && !Logo.GetComponent<VideoPlayer>().isPlaying)
            {
                Logo.SetActive(false);
                IntroGO.SetActive(true);
            }
        }
        else 
        {
            if (IntroGO.activeSelf)
            {
                if (IntroGO.GetComponent<VideoPlayer>().isPrepared && !IntroGO.GetComponent<VideoPlayer>().isPlaying)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(1);
    }
}
