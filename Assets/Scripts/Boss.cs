using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int BossHp = 700;
    public float PatternTime = 5f;
    public int PatternNum = 0;
    public int PrePatternNum = 0;

    void Start()
    {
        PatternManager();
    }

    private void PatternManager()
    {
        int idx = 0;
        do
        {
            PatternNum = Random.Range(1, 3);
        } while (PatternNum == PrePatternNum);

        PrePatternNum = PatternNum;

        switch (PatternNum)
        {
            case 1:
                Pattern_1();
                break;
            case 2:
                Pattern_2();
                break;
            default:
                break;
        }


        Invoke("PatternManager", PatternTime);
    }

    private void Pattern_1()
    {
        Debug.Log("패턴1");
    }

    private void Pattern_2()
    {
        Debug.Log("패턴2");
    }
}
