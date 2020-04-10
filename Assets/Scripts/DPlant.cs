using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DPlant", menuName = "DPlant")]
public class DPlant : ScriptableObject
{
    public string namePlant;
    public Sprite[] ground;
    public Sprite[] small;
    public Sprite[] medium;
    public Sprite[] large;
    public float growTime = 10;
    public int price = 200;
    public int coinValue = 10;
    public float coinSpawn = 10; //seconds
    public float coinSize = 0.5f;
}
