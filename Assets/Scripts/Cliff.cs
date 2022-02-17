using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if(player == null)
            return;
        int layer = other.gameObject.layer;
        if(layer == 10)
        {
            player.damaged(1000);
        }
    }
}
