using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class DItemHolder : NetworkBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public DItem item;
    public int quantity = 0;

    // inventoryIndex, virtualItem and inventory is set by player's inventory
    public int inventoryIndex;
    public Image vitualItem;
    public DInventory inventory;
    public TextMeshProUGUI quanityText;

    [SyncVar] GameObject spawnObj;

    private RectTransform rect;

    private void Start()
    {
        vitualItem.enabled = false;
        rect = vitualItem.GetComponent<RectTransform>();
        GameObject obj = Instantiate(Resources.Load<GameObject>("ItemQuantity") as GameObject, transform);
        quanityText = obj.GetComponent<TextMeshProUGUI>();
        quanityText.text = "";
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (quantity == 0) return;
        rect.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (quantity == 0) return;
        vitualItem.enabled = true;
        vitualItem.sprite = item.sprite;
        rect.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (quantity == 0) return;

        Vector3 position = Camera.main.ScreenToWorldPoint(eventData.position);
        position.z = 0;
        inventory.spawnObj = item.gameObject;
        inventory.CmdSpawnObject(position, Quaternion.identity);
        inventory.Remove(inventoryIndex);

        quantity -= 1;
        if (quantity == 0)
        {
            GetComponent<Image>().sprite = null;
            vitualItem.enabled = false;
            quanityText.text = "";
        }
        else
        {
            vitualItem.enabled = false;
            quanityText.text = quantity.ToString();
        }
    }


}
