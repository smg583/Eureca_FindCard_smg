using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button LevelTwoBtn;
    public Button LevelThreeBtn;
    public GameObject HiddenBtn;

    private void Start()
    {
        switch (PlayerPrefs.GetInt(GameConfig.clearLevelString))
        {
            case 1:
                LevelTwoBtn.interactable = true;
                break;

            case 2:
                LevelTwoBtn.interactable = true;
                LevelThreeBtn.interactable = true;
                break;

            case -1:
            case 3:
                LevelTwoBtn.interactable = true;
                LevelThreeBtn.interactable = true;
                HiddenBtn.SetActive(true);
                break;

            default:
                break;
        }
    }
}
