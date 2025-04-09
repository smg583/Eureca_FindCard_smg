using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public RectTransform timeBar;
    public Image timeBarColor;

    float maxTime;
    float ratio;

    void Start()
    {
        maxTime = GameManager.instance.time;
    }

    void Update()
    {
        ratio = GameManager.instance.time / maxTime;
        timeBar.localScale = new Vector3(ratio, 1f, 1f);
        timeBarColor.color = Color.Lerp(Color.red, Color.green, ratio);
    }
}
