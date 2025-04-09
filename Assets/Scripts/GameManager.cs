using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text timeTxt;
    public GameObject EndPanel;

    public Card firstCard;
    public Card secondCard;

    int cardCount = 20;
    float warningTime;
    public float time;

    float level = StageManager.instance.stage;

    bool isFast = false;
    bool isPlay = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isPlay = true;
    }

    void Start()
    {
        time = Mathf.Lerp(60, 30, (level - 1) / 4);
        warningTime = time / 3;
    }

    void Update()
    {
        if (isPlay)
        {
            if (!isFast && time < warningTime)
            {
                Faster();
            }
            if (time < 0)
            {
                GameOver();
            }
            if (cardCount == 0)
            {
                GameClear();
            }

            timeTxt.text = time.ToString("N2");
            time -= Time.deltaTime;
        }
    }

    void Faster()
    {
        isFast = true;
        AudioManager.instance.ChangeBgm(AudioManager.Bgm.Fast);
    }

    void GameOver()
    {
        isPlay = false;
        DestroyAllCard();
        EndPanel.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Over);
        time = 0;
    }

    void GameClear()
    {
        isPlay = false;
        MySceneManager.instance.GoClear();
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            cardCount -= 2;
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            AudioManager.instance.PlaySfx(AudioManager.Sfx.True);
        }
        else
        { 
            firstCard.CloseCard();
            secondCard.CloseCard();
            AudioManager.instance.PlaySfx(AudioManager.Sfx.False);
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
