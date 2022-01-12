using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIControl : MonoBehaviour
{
    public Slider sliderHp;
    Player player;
    private void Start() {
        player = GetComponent<Player>();
    }
    private void Update() {
        sliderHp.value = player.player_hp;
    }
}
