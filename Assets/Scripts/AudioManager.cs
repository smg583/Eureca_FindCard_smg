using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume;
    AudioSource[] bgmPlayers;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;

    public enum Bgm { Normal, Warning};
    public enum Sfx { ButtonClick, CardCorrect, CardFlip, CardWrong, GameOver};

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject); //�ߺ� ����
        }
    }

    void Init()
    {
        //����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayers = new AudioSource[2];

        for (int i = 0; i < 2; i++)
        {
            bgmPlayers[i] = bgmObject.AddComponent<AudioSource>();
            bgmPlayers[i].playOnAwake = false;
            bgmPlayers[i].loop = true;
            bgmPlayers[i].volume = bgmVolume;
        }

        bgmPlayers[0].clip = bgmClip[(int)Bgm.Normal];
        bgmPlayers[1].clip = bgmClip[(int)Bgm.Warning];

        //ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < channels; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    //BGM ����(BMG ����, BGM ���� & ����)
    public void ControlBgm(Bgm bgm, bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayers[(int)bgm].Play();
        }
        else
        {
            bgmPlayers[(int)bgm].Stop();
        }
    }

    //ȿ���� Ʋ��
    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < channels; i++)
        {
            if (sfxPlayers[i].isPlaying)
                continue;

            sfxPlayers[i].clip = sfxClips[(int)sfx];
            sfxPlayers[i].Play();
            break;
        }
    }
}
