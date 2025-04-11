using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject card;
    public GameObject hiddenCard;
    GameObject[] cards;

    private void Start()
    {
        Place(GameConfig.level == -1 ? false : true);

        Invoke("PlaceOver", 1.3f);
    }

    void Place(bool isNormal)
    {
        int sameCardNum = isNormal ? 2 : 4;

        int cardNum = GameConfig.maxCardNum * sameCardNum;
        cards = new GameObject[cardNum];

        int[] arr = new int[cardNum];

        int num = 0;
        for (int i = 0; i < arr.Length; i += sameCardNum)
        {
            for (int j = 0; j < sameCardNum; j++) 
            {
                arr[i + j] = num;
            }
            num++;
        }
        
        arr = arr.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            cards[i] = Instantiate(isNormal ? card : hiddenCard, transform);
            cards[i].GetComponent<Card>().InitImage(arr[i]);
        }
    }

    //카드 배치가 끝나면 타이머 및 배경음이 나도록 설정
    void PlaceOver()
    {
        GameManager.instance.isPlay = true;
        AudioManager.instance.ControlBgm(AudioManager.Bgm.Normal, true);
    }

    //게임 오버 시에 모든 카드의 던지는 애니메이션 메서드를 호출
    public void ThrowAllCard()
    {
        foreach (GameObject c in cards) 
        {
            c.GetComponent<Card>().ThrowCardAnim();
        }
    }
 }
