using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int index;

    public GameObject cardImage;
    public GameObject buttonText;
    public Sprite[] photos;

    Button btn;
    Image img;
    Animator animator;
    Rigidbody2D rigid;

    private void Awake()
    {
        btn = GetComponent<Button>();
        img = GetComponent<Image>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    //카드 이미지 초기화
    public void InitImage(int idx)
    {
        index = idx;
        cardImage.GetComponent<Image>().sprite = photos[index];
    }

    //카드 뒤집기(애니메이션, 2개 초과 카드 뒤집기 방지, 효과음)
    public void FlipCard()
    {
        if (!GameManager.instance.isPlay)
            return;

        if(GameManager.instance.secCard == null)
        {
            btn.interactable = false;

            cardImage.SetActive(true);

            animator.SetBool("Selected", true);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.CardFlip);

            if (GameManager.instance.firstCard == null)
                GameManager.instance.firstCard = this;
            else
            {
                GameManager.instance.secCard = this;
                GameManager.instance.Match();
            }
        }
    }

    //히든 스테이지 카드 뒤집기(애니메이션, 4개 초과 카드 뒤집기 방지, 효과음)
    public void HiddenFlipCard()
    {
        if (!GameManager.instance.isPlay)
            return;

        if (GameManager.instance.fourthCard == null)
        {
            btn.interactable = false;

            cardImage.SetActive(true);

            animator.SetBool("Selected", true);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.CardFlip);

            if (GameManager.instance.firstCard == null)
                GameManager.instance.firstCard = this;

            else if(GameManager.instance.secCard == null)
                GameManager.instance.secCard = this;

            else if (GameManager.instance.thirdCard == null)
                GameManager.instance.thirdCard = this;

            else
            {
                GameManager.instance.fourthCard = this;
                GameManager.instance.HiddenMatch();
            }
        }
    }

    //뒤집은 카드가 맞지 않으면 다시 뒤집음(Invoke 사용)
    public void UndoCard(int index)
    {
        StartCoroutine(InvokeUndoCard(index));
    }
    IEnumerator InvokeUndoCard(int index)
    {
        yield return new WaitForSeconds(0.5f);

        btn.interactable = true;

        cardImage.SetActive(false);

        animator.SetBool("Selected", false);

        CardDisconnect(index);
    }

    //뒤집은 카드가 맞으면 안 보이게 함(Invoke 사용)
    public void HideCard(int index)
    {
        CardDisconnect(index);
        StartCoroutine(InvokeHideCard());
    }
    IEnumerator InvokeHideCard()
    {
        yield return new WaitForSeconds(0.5f);

        btn.interactable = false;
        buttonText.SetActive(false);
        Color color = img.color;
        color.a = 0f;
        img.color = color;

        cardImage.SetActive(false);
    }

    //게임매니저의 해당하는 카드 오브젝트를 null로 바꿈
    void CardDisconnect(int index)
    {
        switch (index)
        {
            case 1:
                GameManager.instance.firstCard = null;
                break;

            case 2:
                GameManager.instance.secCard = null;
                break;

            case 3:
                GameManager.instance.thirdCard = null;
                break;

            case 4:
                GameManager.instance.fourthCard = null;
                break;

            default:
                break;
        }
    }

    //게임 오버 시 카드를 던지는 애니메이션 효과(2초 후 카드 파괴)
    public void ThrowCardAnim()
    {
        animator.SetTrigger("Over");

        rigid.gravityScale = 1.0f;
        Vector2 dir = Random.value < 0.5f ? Vector2.up + Vector2.left : Vector2.up + Vector2.right;
        rigid.AddForce(dir * 200 * Random.Range(0.5f, 3f));

        Invoke("DestroyCard", 2f);
    }
    void DestroyCard()
    {
        Destroy(gameObject);
    }
}
