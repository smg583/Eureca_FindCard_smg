using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] bgmClip;
    AudioSource bgmPlayer;

    public AudioClip[] sfxClips;
    AudioSource[] sfxPlayer;
    public int channels;

    public enum Bgm { Normal, Fast };
    public enum Sfx { True, False, Click, Flip, Over, Retry };

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
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeBgm(Bgm.Normal);
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.playOnAwake = false;

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayer = new AudioSource[channels];
        for (int i = 0; i < channels; i++)
        {
            sfxPlayer[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayer[i].playOnAwake = false;
        }
    }

    public void ChangeBgm(Bgm bgm)
    {
        if (bgmPlayer.clip != bgmClip[(int)bgm])
        {
            bgmPlayer.clip = bgmClip[(int)bgm];
            bgmPlayer.Play();
        }
        else return;
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < channels; i++)
        {
            if (sfxPlayer[i].isPlaying)
                continue;

            sfxPlayer[i].clip = sfxClips[(int)sfx];
            sfxPlayer[i].Play();
            break;
        }
    }
}