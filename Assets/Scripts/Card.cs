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

    //ī�� �̹��� �ʱ�ȭ
    public void InitImage(int idx)
    {
        index = idx;
        cardImage.GetComponent<Image>().sprite = photos[index];
    }

    //ī�� ������(�ִϸ��̼�, 2�� �ʰ� ī�� ������ ����, ȿ����)
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

    //���� �������� ī�� ������(�ִϸ��̼�, 4�� �ʰ� ī�� ������ ����, ȿ����)
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

    //������ ī�尡 ���� ������ �ٽ� ������(Invoke ���)
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

    //������ ī�尡 ������ �� ���̰� ��(Invoke ���)
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

    //���ӸŴ����� �ش��ϴ� ī�� ������Ʈ�� null�� �ٲ�
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

    //���� ���� �� ī�带 ������ �ִϸ��̼� ȿ��(2�� �� ī�� �ı�)
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
