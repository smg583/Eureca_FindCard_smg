using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource audioSource;

    public AudioClip idleClip;
    public AudioClip fastClip;

    bool isFast;
    bool isStop;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playClip(idleClip);
    }

    void Update()
    {
        if (GameManager.instance == null) return;

        if (!isFast && GameManager.instance.time < 20 && GameManager.instance.time >= 0)
        {
            playClip(fastClip);
            isFast = true;
        }
        else if (!isStop && GameManager.instance.time < 0)
        {
            audioSource.Stop();
            isStop = true;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ClearScene")
        {
            isFast = false;
            isStop = false;
            playClip(idleClip);
        }
    }

    public void playIdle()
    {
        isFast = false;
        isStop = false;
        playClip(idleClip);
    }

    public void playClip(AudioClip clip)
    {
        if (audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
