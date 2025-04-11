using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text timeText;
    public GameObject overUI;
    public GameObject winUI;
    public Board board;

    public Card firstCard;
    public Card secCard;

    //아래는 히든 스테이지에서 사용
    public Card thirdCard;
    public Card fourthCard;

    int remainCardNum;

    public float maxTime;
    public float warningTime;
    float timer;

    bool isRed;

    //게임이 돌아가고 있으면 true, 아니면 false 값을 가지는 bool 변수
    public bool isPlay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        timer = maxTime;
        remainCardNum = GameConfig.maxCardNum;
        isRed = false;
        firstCard = null;
        secCard = null;
    }

    private void Update()
    {
        if (isPlay)
        {
            if (remainCardNum == 0)
                GameClear();

            timer -= Time.deltaTime;
            if (timer <= 0)
                GameOver();

            if (!isRed && timer <= warningTime)
                ChangeWarning();

            TimeTextUpdate();
        }
    }

    //2개의 카드가 맞는 카드인지 확인
    public void Match()
    {
        if(firstCard.index == secCard.index)
        {
            remainCardNum--;
            firstCard.HideCard(1);
            secCard.HideCard(2);

            StartCoroutine(DelayCardSfx(true));
        }
        else
        {
            firstCard.UndoCard(1);
            secCard.UndoCard(2);

            StartCoroutine(DelayCardSfx(false));
        }
    }

    //4개의 카드가 맞는 카드인지 확인(히든)
    public void HiddenMatch()
    {
        bool check1 = firstCard.index == secCard.index;
        bool check2 = secCard.index == thirdCard.index;
        bool check3 = thirdCard.index == fourthCard.index;

        if (check1 && check2 && check3)
        {
            remainCardNum--;
            firstCard.HideCard(1);
            secCard.HideCard(2);
            thirdCard.HideCard(3);
            fourthCard.HideCard(4);

            StartCoroutine(DelayCardSfx(true));
        }
        else
        {
            firstCard.UndoCard(1);
            secCard.UndoCard(2);
            thirdCard.UndoCard(3);
            fourthCard.UndoCard(4);

            StartCoroutine(DelayCardSfx(false));
        }
    }

    //0.5초 후 카드 맞추기 성공 or 실패 효과음 출력
    IEnumerator DelayCardSfx(bool isCorrect)
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySfx(isCorrect ? AudioManager.Sfx.CardCorrect : AudioManager.Sfx.CardWrong);
    }

    //긴박한 BGM 변환 & 타이머 글씨 색깔 변경
    void ChangeWarning()
    {
        isRed = true;
        timeText.color = Color.red;
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Normal, false);
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Warning, true);
    }

    //게임 오버 UI 활성화(코루틴 사용)
    void GameOver()
    {
        isPlay = false;
        timer = 0f;
        TimeTextUpdate();
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Warning, false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.GameOver);

        StartCoroutine(ActiveOverUI());
    }
    IEnumerator ActiveOverUI()
    {
        yield return new WaitForSeconds(0.3f);
        board.ThrowAllCard();

        yield return new WaitForSeconds(0.7f);
        overUI.SetActive(true);
    }

    //게임 클리어 UI 활성화(Invoke 사용)
    void GameClear()
    {
        PlayerPrefs.SetInt(GameConfig.clearLevelString, GameConfig.level);

        isPlay = false;
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Normal, false);
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Warning, false);

        Invoke("delayActiveWinUI", 1f);
    }
    void delayActiveWinUI()
    {
        winUI.SetActive(true);
    }

    //타이머 텍스트를 업데이트
    void TimeTextUpdate()
    {
        timeText.text = timer.ToString("N2");
    }
}
