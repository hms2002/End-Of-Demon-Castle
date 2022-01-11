using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossUIControl : MonoBehaviour
{
    public Slider sliderHp;
    Boss boss;
    private void Start() {
        boss = GetComponent<Boss>();
    }
    private void Update() {
        Debug.Log(boss.BossHp);
        sliderHp.value = boss.BossHp;
    }
}
