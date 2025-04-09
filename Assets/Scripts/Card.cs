using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    public SpriteRenderer frontImage;
    public Animator anim;

    public GameObject front;
    public GameObject back;

    public int idx;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"{idx}");
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);

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
        Invoke("DestroyInvoke", 1f);
    }

    void DestroyInvoke()
    {
        Destroy(gameObject);
    }


    public void CloseCard()
    {
        Invoke("CloseInvoke", 1f);
    }

    void CloseInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
