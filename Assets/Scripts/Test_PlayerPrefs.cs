using UnityEngine;

public class Test_PlayerPrefs : MonoBehaviour
{
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit(); // 재시작 유도
    }

    public void SetAllPlayerPrefs()
    {
        PlayerPrefs.SetInt(GameConfig.clearLevelString, 3);
        PlayerPrefs.Save();
        Application.Quit(); // 재시작 유도
    }
}
