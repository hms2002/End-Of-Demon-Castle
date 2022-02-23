using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomAOE : MonoBehaviour
{
    public int Damage = 3;
    private float DamageTime = 0.5f;
    private float currentDamageTime;
    Animator anim;
    CapsuleCollider2D capsule;
    AudioSource audioSource;
    Boss boss;
    BreakableObj breakableObj;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SoundManager.GetInstance().Play(audioSource, "Sound/PlayerSound/SkillSound/PoisonBottleBreak", 0.6f);
        capsule = gameObject.GetComponent<CapsuleCollider2D>();
        DamageTime = 0.5f;
        capsule.enabled = true;
        anim = GetComponent<Animator>();
        StartCoroutine("VenomEnd");
    }

    void FixedUpdate()
    {
        ElapseTime();
    }

    private void ElapseTime()
    {
        currentDamageTime -= Time.deltaTime;
        if (currentDamageTime <= 0)
        {
            currentDamageTime = DamageTime;
            capsule.enabled = true;
        }
        else if(currentDamageTime <= DamageTime - 0.1f)
        {
            capsule.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (layer == 12)
        {
            if (boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(Damage + DamageControler.GetInstance().GetDotDamage());
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(Damage + DamageControler.GetInstance().GetDotDamage());
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
