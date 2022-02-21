using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour
{
    Image image;
    public Sprite[] images;
    int count;

    private void Start()
    {
        image = GetComponent<Image>();
        count = 0;

        StartCoroutine(AnimateImage());
    }

    IEnumerator AnimateImage()
    {
        while (true)
        {
            if (count == images.Length)
            {
                count = 0;
            }

            image.sprite = images[count];
            count++;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
