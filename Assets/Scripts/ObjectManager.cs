using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject BarragePrefab;
    GameObject[] Barrage;
    GameObject[] targetPool;
    void Awake()
    {
        Barrage = new GameObject[50];

        Generate();
    }

    void Generate()
    {
        for(int index = 0; index < Barrage.Length; index++)
        {
            Barrage[index] = Instantiate(BarragePrefab);
            Barrage[index].SetActive(false);   
        }
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "Barrage":
                targetPool = Barrage;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}
