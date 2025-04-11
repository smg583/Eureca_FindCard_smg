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
            Destroy(gameObject); //중복 방지
        }
    }

    void Init()
    {
        //배경음 플레이어 초기화
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

        //효과음 플레이어 초기화
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

    //BGM 관리(BMG 선택, BGM 시작 & 중지)
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

    //효과음 틀기
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
