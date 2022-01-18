using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public int Damage = 10;
    private float DamageTime = 0.5f;
    private float currentDamageTime;

    void OnEnable()
    {
        DamageTime = 0.5f;   
    }
    void Update()
    {
        ElapseTime();
    }

    private void ElapseTime()
    {
        if (currentDamageTime > 0)
        {
            currentDamageTime -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentDamageTime <= 0)
            {
                collision.GetComponent<Player>().damaged(Damage);

                currentDamageTime = DamageTime;
            }
        }
    }

    public void delete()
    {
        gameObject.SetActive(false);
    }
}
