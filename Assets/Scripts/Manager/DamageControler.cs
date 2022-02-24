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
    float chargeDamage = 0;
    float forPlayerDamage = 0;

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

    public void SetChargeDamage(int idx)
    {
        switch (idx)
        {
            case 0:
                chargeDamage = 35;
                break;
            case 1:
                chargeDamage = 40;
                break;
            case 2:
                chargeDamage = 55;
                break;
        }
    }
    public void UpgradeForPlayerDamage()
    {
        forPlayerDamage = 3;
    }

    public void DowngradeForPlayerDamage()
    {
        forPlayerDamage = 0;
    }

    public float GetDotDamage()
    { return dotDamage; }

    public float GetMonoDamage()
    { return monoDamage; }

    public float GetChargeDamage()
    { return chargeDamage; }

    public float GetForPlayerDamage()
    { return forPlayerDamage; }
}
