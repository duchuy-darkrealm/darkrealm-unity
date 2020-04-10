using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class DInventory : NetworkBehaviour
{
    private const int SLOT_NUMBER = 7;

    DItem[] items;
    int[] quantities;
    GameObject[] imageObjs;

    public GameObject spawnObj;

    private void Start()
    {
        if (!isLocalPlayer) return;
        items = new DItem[SLOT_NUMBER];
        quantities = new int[SLOT_NUMBER];
        imageObjs = GameObject.Find("Inventory").GetComponent<DInventorySpawn>().imageObjs;
        for (int i = 0; i < imageObjs.Length;i++)
        {
            imageObjs[i].GetComponent<DItemHolder>().inventory = this;
        }
    }

    public bool Add(DItem item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                quantities[i] += 1;
                imageObjs[i].GetComponent<DItemHolder>().quantity = quantities[i];
                imageObjs[i].GetComponent<DItemHolder>().quanityText.text = quantities[i].ToString();
                return true;
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                quantities[i] = 1;
                imageObjs[i].GetComponent<Image>().sprite = item.sprite;
                imageObjs[i].GetComponent<DItemHolder>().item = item;
                imageObjs[i].GetComponent<DItemHolder>().quantity = quantities[i];
                imageObjs[i].GetComponent<DItemHolder>().quanityText.text = quantities[i].ToString();
                return true;
            }
        }

        return false;
    }

    public bool CheckItemSlot(DItem item)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i] == item)
                return true;

        for (int i = 0; i < items.Length; i++)
            if (items[i] == null)
                return true;

        return false;
    }

    public void Remove(int index)
    {
        quantities[index] -= 1;
        if (quantities[index] <= 0)
        {
            items[index] = null;
            quantities[index] = 0;
        }
    }

    [Command]
    public void CmdDestroy(GameObject gameObject)
    {
        NetworkIdentity id = GetComponent<NetworkIdentity>();
        id.AssignClientAuthority(connectionToClient);

        if (id.AssignClientAuthority(connectionToClient) == true)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    [Command]
    public void CmdSpawnObject(Vector3 position, Quaternion quaternion)
    {
        GameObject spawn = Instantiate(spawnObj, position, quaternion);
        NetworkServer.Spawn(spawn);
    }
}
