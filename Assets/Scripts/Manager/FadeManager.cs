using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager fadeManager;
    public bool isFadeEnd;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        isFadeEnd = false;
    }

    public static FadeManager GetInstance()
    {
        if (fadeManager == null)
        {
            fadeManager = FindObjectOfType<FadeManager>();
        }

        return fadeManager;
    }

    public void FadeIn(Image fadeImage)
    {
        StartCoroutine(FadeIn_Loading(fadeImage));
    }

    public void FadeOut(Image fadeImage)
    {
        StartCoroutine(FadeOut_Loading(fadeImage));
    }

    public IEnumerator FadeInAndOut(Image fadeImage)
    {
        if (isFadeEnd == true)
        {
            yield break;
        }
        isFadeEnd = true;
        FadeIn(fadeImage);
        //���� ����
        yield return new WaitForSeconds(2.0f);
        FadeOut(fadeImage);
    }
    public IEnumerator FadeInAndOut(float time)
    {
        Image loadingImage = GameManager.GetInstance().fadeImage_loading;
        FadeIn(loadingImage);
        //���� ����
        yield return new WaitForSeconds(time / 5.0f);
        SoundManager.GetInstance().Play("Sound/SystemSound/SwordFight");
        yield return new WaitForSeconds(time / 1.4f);
        FadeOut(loadingImage);
    }

    IEnumerator FadeIn_Loading(Image fadeImage)
    {
        fadeImage.transform.parent.gameObject.SetActive(true);

        Color imageColor = fadeImage.color;

        for (int i = 0; i < 100; i++)
        {
            imageColor.a += 0.01f;
            fadeImage.color = imageColor;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator FadeOut_Loading(Image fadeImage)
    {
        fadeImage.transform.parent.gameObject.SetActive(true);
        Color imageColor = fadeImage.color;
        imageColor.a = 1.0f;

        for (int i = 100; i > 0; i--)
        {
            imageColor.a -= 0.01f;
            fadeImage.color = imageColor;
            yield return new WaitForSeconds(0.01f);
        }

        fadeImage.transform.parent.gameObject.SetActive(false);

        isFadeEnd = false;
    }
}
