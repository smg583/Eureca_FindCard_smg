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

    //�Ʒ��� ���� ������������ ���
    public Card thirdCard;
    public Card fourthCard;

    int remainCardNum;

    public float maxTime;
    public float warningTime;
    float timer;

    bool isRed;

    //������ ���ư��� ������ true, �ƴϸ� false ���� ������ bool ����
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

    //2���� ī�尡 �´� ī������ Ȯ��
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

    //4���� ī�尡 �´� ī������ Ȯ��(����)
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

    //0.5�� �� ī�� ���߱� ���� or ���� ȿ���� ���
    IEnumerator DelayCardSfx(bool isCorrect)
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySfx(isCorrect ? AudioManager.Sfx.CardCorrect : AudioManager.Sfx.CardWrong);
    }

    //����� BGM ��ȯ & Ÿ�̸� �۾� ���� ����
    void ChangeWarning()
    {
        isRed = true;
        timeText.color = Color.red;
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Normal, false);
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Warning, true);
    }

    //���� ���� UI Ȱ��ȭ(�ڷ�ƾ ���)
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

    //���� Ŭ���� UI Ȱ��ȭ(Invoke ���)
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

    //Ÿ�̸� �ؽ�Ʈ�� ������Ʈ
    void TimeTextUpdate()
    {
        timeText.text = timer.ToString("N2");
    }
}
