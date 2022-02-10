using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainImageAnimationOutput1 : MonoBehaviour
{
    void Start()
    {
        SoundManager.GetInstance().Play("Sound/BGM/BGM_MainMenu", 0.3f, Define.Sound.Bgm);
    }
}
