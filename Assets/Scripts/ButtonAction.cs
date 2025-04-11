using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    //메인 씬으로 이동하면서 level 값을 통해 스테이지를 결정
    public void StartMainScene(int level)
    {
        GameConfig.level = level;
        GameConfig.maxCardNum = 10 + (level - 1) * 2;
        if(level == -1)
            GameConfig.maxCardNum = 5;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        SceneManager.LoadScene(1);
    }

    //버튼 클릭 시 효과음과 함께 해당 씬 로드
    public void Retry(int index)
    {
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Normal, false);
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Warning, false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        SceneManager.LoadScene(index);
    }

    //버튼 클릭 시 게임을 종료하는 코루틴 시작
    public void QuitGame()
    {
        StartCoroutine(WaitQuitGame());
    }
    IEnumerator WaitQuitGame()
    {
        //버튼 클릭 효과음이 들린 후에 게임 종료
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        yield return new WaitForSeconds(0.5f);

#if UNITY_EDITOR
        // 에디터에서 플레이 중일 땐 강제 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 실제 빌드된 게임에서는 이걸로 종료
        Application.Quit();
#endif
    }
}
