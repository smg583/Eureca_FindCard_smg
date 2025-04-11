using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    //���� ������ �̵��ϸ鼭 level ���� ���� ���������� ����
    public void StartMainScene(int level)
    {
        GameConfig.level = level;
        GameConfig.maxCardNum = 10 + (level - 1) * 2;
        if(level == -1)
            GameConfig.maxCardNum = 5;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        SceneManager.LoadScene(1);
    }

    //��ư Ŭ�� �� ȿ������ �Բ� �ش� �� �ε�
    public void Retry(int index)
    {
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Normal, false);
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Warning, false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        SceneManager.LoadScene(index);
    }

    //��ư Ŭ�� �� ������ �����ϴ� �ڷ�ƾ ����
    public void QuitGame()
    {
        StartCoroutine(WaitQuitGame());
    }
    IEnumerator WaitQuitGame()
    {
        //��ư Ŭ�� ȿ������ �鸰 �Ŀ� ���� ����
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        yield return new WaitForSeconds(0.5f);

#if UNITY_EDITOR
        // �����Ϳ��� �÷��� ���� �� ���� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� ����� ���ӿ����� �̰ɷ� ����
        Application.Quit();
#endif
    }
}
