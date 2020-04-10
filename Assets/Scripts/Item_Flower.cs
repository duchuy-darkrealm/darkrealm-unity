using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Item_Flower : NetworkBehaviour, IInteract
{
    public float INTERACT_DISTANCE = 0.5f;
    // Inventory is set by player inventory
    public DInventory inventory;
    [SerializeField]
    public DItem item;
    public DInteractButton button;

    private void Start()
    {
        button = GameObject.Find("InteractButton").GetComponent<DInteractButton>();
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer) {
                inventory = player.GetComponent<DInventory>();
                if (Vector3.Distance(transform.position, player.transform.position) < INTERACT_DISTANCE)
                {
                    button.Regist(this, GetComponentInChildren<SpriteRenderer>().sprite);
                    return;
                }
            }
        }
        button.UnRegist(this);
    }

    public void Interact()
    {
        if (inventory.Add(item))
            inventory.CmdDestroy(gameObject);
    }
}
