using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int Damage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        int Layer = collision.gameObject.layer;            
        if(collision.CompareTag("CanBroke"))
        {
            BreakableObj obj = collision.GetComponent<BreakableObj>();
            obj.breakObj();
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
