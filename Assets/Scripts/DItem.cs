using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DItem", menuName = "DItem")]
public class DItem : ScriptableObject
{
    public string name;
    [Multiline(15)]
    public string detail;
    [SerializeField]
    public int priceGold = 200;
    public int priceDiamond = 0;
    public GameObject gameObject;
    public Sprite sprite;
}
