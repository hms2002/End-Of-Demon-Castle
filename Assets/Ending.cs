using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public void toMain()
    {
        GameManager.GetInstance().toMain();
    }
}
