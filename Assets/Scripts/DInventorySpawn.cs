using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DInventorySpawn : MonoBehaviour
{
    private const int SLOT_NUMBER = 7;
    public int SIZE = 150;
    public GameObject[] imageObjs;

    void Start()
    {
        imageObjs = new GameObject[SLOT_NUMBER];
        Image virtualItem = GameObject.Find("VirtualItem").GetComponent<Image>();

        for (int i = 0; i < imageObjs.Length; i++)
        {
            imageObjs[i] = Instantiate(Resources.Load<GameObject>("InventorySlot") as GameObject, transform);
            imageObjs[i].GetComponent<DItemHolder>().vitualItem = virtualItem;
            imageObjs[i].GetComponent<DItemHolder>().inventoryIndex = i;
        }

        //for (int i = 0; i < imageObjs.Length; i++)
        //{
        //    imageObjs[i] = new GameObject();
        //    imageObjs[i].AddComponent<RectTransform>();
        //    imageObjs[i].GetComponent<RectTransform>().sizeDelta = new Vector2(SIZE, SIZE);
        //    imageObjs[i].AddComponent<CanvasRenderer>();
        //    imageObjs[i].AddComponent<Image>();
        //    imageObjs[i].AddComponent<DItemHolder>();
        //    imageObjs[i].GetComponent<DItemHolder>().inventoryIndex = i;
        //    imageObjs[i].GetComponent<DItemHolder>().vitualItem = virtualItem;
        //    imageObjs[i].transform.SetParent(transform);
        //}
    }
}
