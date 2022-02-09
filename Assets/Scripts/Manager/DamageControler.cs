using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControler : MonoBehaviour
{
    static DamageControler damageControler;

    public static DamageControler GetInstance()
    {
        if(damageControler == null)
        {
            damageControler = FindObjectOfType<DamageControler>();
        }

        return damageControler;
    }

    float dotDamage = 0;
    float monoDamage = 0;

    public void UpgradeDamage()
    {
        dotDamage = 3;
        monoDamage = 5;
    }

    public void DowngradeDamage()
    {
        dotDamage = 0;
        monoDamage = 0;
    }

    public float GetDotDamage()
    { return dotDamage; }

    public float GetMonoDamage()
    { return monoDamage; }
}
