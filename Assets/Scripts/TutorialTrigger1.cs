using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialTrigger1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            if(TutorialManager.GetInstance().isFirstMissionClear == false)
            {
                TutorialManager.GetInstance().isFirstMissionClear = true;
                TutorialManager.GetInstance().StertTutorial2();
                gameObject.SetActive(false);
            }
        }
    }
}
