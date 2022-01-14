using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject BarragePrefab;
    public GameObject ArrowPrefab;
    public GameObject ShockwavePrefab;
    public GameObject LaserPrefab;
    GameObject[] Barrage;
    GameObject[] Arrow;
    GameObject[] Shockwave;
    GameObject[] Laser;
    GameObject[] targetPool;
    void Awake()
    {
        Barrage = new GameObject[50];
        Arrow = new GameObject[30];
        Shockwave = new GameObject[2];
        Laser = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        for(int index = 0; index < Barrage.Length; index++)
        {
            Barrage[index] = Instantiate(BarragePrefab);
            Barrage[index].SetActive(false);   
        }
        for(int index = 0; index < Arrow.Length; index++)
        {
            Arrow[index] = Instantiate(ArrowPrefab);
            Arrow[index].SetActive(false);   
        }
        for(int index = 0; index < Shockwave.Length; index++)
        {
            Shockwave[index] = Instantiate(ShockwavePrefab);
            Shockwave[index].SetActive(false);
        }
        for(int index = 0; index < Laser.Length; index++)
        {
            Laser[index] = Instantiate(LaserPrefab);
            Laser[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "Barrage":
                targetPool = Barrage;
                break;
            case "Arrow":
                targetPool = Arrow;
                break;
            case "Shockwave":
                targetPool = Shockwave;
                break;
            case "Laser":
                targetPool = Laser;
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
