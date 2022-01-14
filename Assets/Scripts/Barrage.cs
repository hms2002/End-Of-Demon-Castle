using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    public int Damage = 10;
    public int breakableLayer = 8;
    void OnEnable()
    {
        breakableLayer = 8;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        int Layer = collision.gameObject.layer;
        if(Layer == 10)
        {
            collision.GetComponent<Player>().damaged(Damage);
            gameObject.SetActive(false);
        }
        if(Layer == breakableLayer)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {   
        if (collision.gameObject.layer == 8)
        {
            breakableLayer = 8;
        }
    }

    public void TimeDiffernce()
    {
        
    }
}
