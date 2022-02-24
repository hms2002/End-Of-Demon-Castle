using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int Damage = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Damage = 4;
        int Layer = collision.gameObject.layer;            
        if(collision.CompareTag("CanBroke"))
        {
            BreakableObj obj = collision.GetComponent<BreakableObj>();
            obj.breakObj(Damage + DamageControler.GetInstance().GetMonoDamage());
            gameObject.SetActive(false);
        }
        if(Layer == 12)
        {
            collision.GetComponent<Boss>().damaged(Damage + DamageControler.GetInstance().GetMonoDamage());
            gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "ArrowBorder")
        {
            gameObject.SetActive(false);
        }
    }
}
