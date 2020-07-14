using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip click;
    public void StartGame()
    {
        source.PlayOneShot(click);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        source.PlayOneShot(click);
        Application.Quit();
    }
}
