using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomAOE : MonoBehaviour
{
    public int Damage = 10;
    private float DamageTime = 0.5f;
    private float currentDamageTime;
    Animator anim;
    CapsuleCollider2D capsule;

    void Start()
    {
        capsule = gameObject.GetComponent<CapsuleCollider2D>();
        DamageTime = 0.5f;
        capsule.enabled = true;
        anim = GetComponent<Animator>();
        StartCoroutine("VenomEnd");
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
        /*Debug.Log("zz");*/
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (currentDamageTime <= 0)
            {
                collision.GetComponent<Boss>().damaged(Damage);

                currentDamageTime = DamageTime;
            }
        }
    }

    IEnumerator VenomEnd()
    {
        yield return new WaitForSeconds(5f);
        capsule.enabled = false;
        anim.SetTrigger("VenomEnd");
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
