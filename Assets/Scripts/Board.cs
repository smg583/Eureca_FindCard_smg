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
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 ,8 ,8 , 9, 9 };
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

        int i = 0;

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                float posX = (x * 2) - 4;
                float posY = 2 - (y * 2);

                GameObject go = Instantiate(Card, this.transform);
                go.transform.position = new Vector2(posX, posY);

                go.GetComponent<Card>().Setting(arr[i]);
                i++;
            }
        }
    }
}
