using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainImage_Thunder : MonoBehaviour
{
    Image image;
    public Sprite[] images;

    float thunderPlayTime = 1.0f; //번개 재생 속도에 따라 값 수정 바람

    void Start()
    {
        image = GetComponent<Image>();

        StartCoroutine(MakeThunderRandom());
    }

    IEnumerator MakeThunderRandom()
    {
        StartCoroutine(PlayThunder());

        while (true)
        {
            yield return new WaitForSeconds(thunderPlayTime);
            float timing = Random.Range(5.0f, 9.0f);
            yield return new WaitForSeconds(timing);
            StartCoroutine(PlayThunder());
        }
    }

    IEnumerator PlayThunder()
    {
        SetAlphaOne();

        for (int imageNumber = 0; imageNumber < images.Length; imageNumber++)
        {
            image.sprite = images[imageNumber];
            yield return new WaitForSeconds(0.15f);
        }

        StartCoroutine(GraduallyReduceAlpha());
    }

    void SetAlphaOne()
    {
        Color imageColor = image.color;

        imageColor.a = 1.0f;
        image.color = imageColor;
    }

    IEnumerator GraduallyReduceAlpha()
    {
        Color imageColor = image.color;

        for (int i = 100; i > 0; i--)
        {
            imageColor.a -= 0.01f;
            image.color = imageColor;
            yield return new WaitForSeconds(0.005f);
        }
    }
}
