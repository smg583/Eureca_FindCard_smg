using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource;
    public AudioClip trueClip;
    public AudioClip falseClip;
    public AudioClip overClip;

    public Card firstCard;
    public Card secondCard;
    int cardCount = 20;

    public Text timeTxt;
    public GameObject endTxt;
    public float time;

    bool isOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        time = 60f;
        audioSource = GetComponent<AudioSource>();
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playIdle();
        }
    }

    void Update()
    {
        if (isOver) return;

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time < 0)
        {
            isOver = true;
            timeTxt.text = "Failed";
            Time.timeScale = 0;
            audioSource.PlayOneShot(overClip);
            endTxt.SetActive(true);

            DestroyAllCard();
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(trueClip);

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if (cardCount == 0)
            {
                SceneManager.LoadScene("ClearScene");
            }
        }
        else
        {
            audioSource.PlayOneShot(falseClip);

            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    void DestroyAllCard()
    {
        Card[] allCards = FindObjectsOfType<Card>();
        foreach (Card card in allCards)
        {
            Destroy(card.gameObject);
        }
    }
}
