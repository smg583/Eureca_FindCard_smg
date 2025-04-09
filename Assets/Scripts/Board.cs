using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject Card;

    void Start()
    {
        StartCoroutine(SpawnCards());
    }

    IEnumerator SpawnCards()
    {
        List<int> cardNum = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            cardNum.Add(i);
            cardNum.Add(i);
        }
        cardNum = cardNum.OrderBy(x => Random.value).ToList();

        List<Vector2> cardPos = new List<Vector2>();
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                float posX = (x * 2) - 4;
                float posY = 2 - (y * 2);
                cardPos.Add(new Vector2(posX, posY));
            }
        }
        cardPos = cardPos.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(Card, transform);
            go.transform.position = cardPos[i];
            go.GetComponent<Card>().Setting(cardNum[i]);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
