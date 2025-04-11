using UnityEngine;

public class TitleImageAnim : MonoBehaviour
{
    public Sprite[] photos;

    SpriteRenderer spriter;

    public float period;
    float timer = 0f;
    int save = -1;
    int temp;

    private void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= period)
        {
            timer = 0f;

            temp = Random.Range(0, photos.Length);
            if (temp == save)
                save = (temp == 0 ? photos.Length : temp) - 1;
            else
                save = temp;

            spriter.sprite = photos[save];
        }
    }
}
