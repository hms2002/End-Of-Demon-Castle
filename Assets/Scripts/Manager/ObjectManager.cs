using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject BarragePrefab;
    public GameObject ArrowPrefab;
    public GameObject ShockwavePrefab;
    public GameObject LaserPrefab;
    public GameObject LaserPivotPrefab;
    public GameObject BigBarragePrefab;
    public GameObject AOEPrefab;
    GameObject[] Barrage;
    GameObject[] Arrow;
    GameObject[] Shockwave;
    GameObject[] Laser;
    GameObject[] LaserPivot;
    GameObject[] BigBarrage;
    GameObject[] AOE;
    GameObject[] targetPool;
    void Awake()
    {
        Barrage = new GameObject[150];
        Arrow = new GameObject[50];
        Shockwave = new GameObject[2];
        Laser = new GameObject[20];
        LaserPivot = new GameObject[10];
        BigBarrage = new GameObject[5];
        AOE = new GameObject[11];

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
        for (int index = 0; index < LaserPivot.Length; index++)
        {
            LaserPivot[index] = Instantiate(LaserPivotPrefab);
            LaserPivot[index].SetActive(false);
        }
        for (int index = 0; index < BigBarrage.Length; index++)
        {
            BigBarrage[index] = Instantiate(BigBarragePrefab);
            BigBarrage[index].SetActive(false);
        }
        for (int index = 0; index < AOE.Length; index++)
        {
            AOE[index] = Instantiate(AOEPrefab);
            AOE[index].SetActive(false);
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
            case "LaserPivot":
                targetPool = LaserPivot;
                break;
            case "BigBarrage":
                targetPool = BigBarrage;
                break;
            case "AOE":
                targetPool = AOE;
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
