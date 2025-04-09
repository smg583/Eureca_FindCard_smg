using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GoStart()
    {
        AudioManager.instance.ChangeBgm(AudioManager.Bgm.Normal);
        SceneManager.LoadScene("StartScene");
    }
    public void GoMain()
    {
        AudioManager.instance.ChangeBgm(AudioManager.Bgm.Normal);
        SceneManager.LoadScene("MainScene");
    }
    public void GoClear()
    {
        AudioManager.instance.ChangeBgm(AudioManager.Bgm.Normal);
        SceneManager.LoadScene("ClearScene");
    }

    public void GoStage()
    {
        AudioManager.instance.ChangeBgm(AudioManager.Bgm.Normal);
        SceneManager.LoadScene("StageScene");
    }
}
