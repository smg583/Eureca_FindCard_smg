using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public Text stageTxt;

    public int stage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        stage = 1;
        stageTxt.text = $"stage\n{stage}";
    }
    
    public void NextStage()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
        if (stage < 5)
        {
            stage++;
        }
        else return;
            stageTxt.text = $"stage\n{stage}";
    }

    public void PrevStage()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
        if (stage > 1)
        {
            stage--;
        }
        else return;
            stageTxt.text = $"stage\n{stage}";
    }
}
