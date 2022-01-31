using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainImageAnimationOutput : MonoBehaviour
{
    public Image image;
    public Sprite[] images;
    int cnt;

    private void Start()
    {
        image = GetComponent<Image>();
        cnt = 0;
        StartCoroutine("AnimateImage");
    }

    IEnumerator AnimateImage()
    {

        while(true)
        {
            if (cnt == images.Length)
                cnt = 0;

            image.sprite = images[cnt];
            cnt++;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
