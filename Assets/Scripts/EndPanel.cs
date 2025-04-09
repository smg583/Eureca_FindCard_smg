using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Retry()
    {
        Time.timeScale = 1.0f;
        Invoke("LoadScene", 0.3f);
        audioSource.PlayOneShot(clip);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.playIdle();
        }
    }


    void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
