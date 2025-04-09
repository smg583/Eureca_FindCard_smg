using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer frontImage;
    public Animator anim;

    public GameObject front;
    public GameObject back;
  
    public int idx;
    float invokeTime;

    int level;
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"{idx}");
        level = StageManager.instance.stage;
        invokeTime = Mathf.Lerp(1, 0.5f, (level - 1) / 4);
    }

    public void OpenCard()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Flip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyInvoke", invokeTime);
    }

    void DestroyInvoke()
    {
        Destroy(gameObject);
    }


    public void CloseCard()
    {
        Invoke("CloseInvoke", invokeTime);
    }

    void CloseInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
