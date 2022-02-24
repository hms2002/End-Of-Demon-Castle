using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainImageAnimationOutput2 : MonoBehaviour
{
    Image image;
    Image backImage;
    public GameObject backImageObject;
    public Sprite[] images;
    int count;

    private void Start()
    {
        image = GetComponent<Image>();
        backImage = backImageObject.GetComponent<Image>();
        count = 0;
        StartCoroutine("AnimateImage");
    }

    IEnumerator AnimateImage()
    {
        backImage.sprite = images[count];
        count++;
        image.sprite = images[count];

        while (true)
        {
            StartCoroutine(FadeInImage(image));
            yield return new WaitForSeconds(0.35f);
            backImage.sprite = image.sprite;
            count++;
            SetAlphaZero();

            if (count == images.Length)
            {
                count = 0;
            }

            image.sprite = images[count];
        }
    }

    IEnumerator FadeInImage(Image fadeImage)
    {
        Color imageColor = fadeImage.color;

        for (int i = 0; i < 100; i++)
        {
            imageColor.a += 0.01f;
            fadeImage.color = imageColor;
            yield return new WaitForSeconds(0.001f);
        }
    }

    void SetAlphaZero()
    {
        Color imageColor = image.color;
        imageColor.a = 0.0f;
        image.color = imageColor;
    }
}
