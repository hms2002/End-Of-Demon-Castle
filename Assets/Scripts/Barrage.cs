using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    public int Damage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        int Layer = collision.gameObject.layer;
        if(Layer == 10)
        {
            collision.GetComponent<Player>().damaged(Damage);
            gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Border")
        {
            gameObject.SetActive(false);
        }
    }
}
