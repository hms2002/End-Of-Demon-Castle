using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Enter");
        Player player = other.GetComponent<Player>();
        if(player == null)
            return;
        Debug.Log("Player");
        int layer = other.gameObject.layer;
        if(layer == 10)
        {
            player.damaged(1000);
        }
    }
}
