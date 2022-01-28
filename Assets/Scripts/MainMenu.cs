using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject canvas_mainImage;
    public Image[] storyImage;
    int count;

    private void Start()
    {
        count = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(count)
            {
                case 0:

                    FadeManager.GetInstance().StartCoroutine("FadeInAndOut", GameManager.GetInstance().fadeImage_loading);
                    StartCoroutine("Polaroid");
                    Invoke("FalseMainImage", 1.1f);

                    count++;

                    break;

                case 1:

                    if (FadeManager.GetInstance().isFadeEnd)
                    {
                        return;
                    }
                    Skip();
                    count++;

                    break;
            }
        }
    }

    void FalseMainImage()
    {
        canvas_mainImage.SetActive(false);
    }

    public void Skip()
    {
        StopCoroutine("Polaroid");

        Color imageColor;

        for (int i = 0; i < 3; i++)
        {
            imageColor = storyImage[i].color;
            imageColor.a = 1;
            storyImage[i].color = imageColor;
        }

        Debug.Log("¿Ô³ª?");
        FadeManager.GetInstance().StartCoroutine("FadeInAndOut", 5.0f);
        Invoke("StoryImageFalse", 1.5f);
    }

    IEnumerator Polaroid()
    {
        Color imageColor;

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < 3; i++)
        {
            imageColor = storyImage[i].color;
            imageColor.a = 0;
            storyImage[i].color = imageColor;
        }

        for (int i = 0; i < 3; i++)
        {
            FadeManager.GetInstance().FadeIn(storyImage[i]);
            SoundManager.GetInstance().Play("Sound/SystemSound/Paper");
            yield return new WaitForSeconds(2.0f);
        }
    }

    void StoryImageFalse()
    {
        storyImage[1].transform.parent.gameObject.SetActive(false);
    }
}
