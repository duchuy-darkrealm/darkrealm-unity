using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DCoin : MonoBehaviour,IPointerDownHandler
{
    public int value = 0;
    public GameObject owner;

    public void OnPointerDown(PointerEventData eventData)
    {
        CollectCoin();
    }

    void Update()
    {
        if (DGameSystem.player == null) return;

        if (Vector3.Distance(transform.position, DGameSystem.player.transform.position) < 0.5f)
            CollectCoin();
    }

    private void CollectCoin()
    {
        DGameSystem.LoadPool("ParticleStar", transform.position);
        DGameSystem.AddMoney(value);
        if (owner != null)
            owner.SendMessage("CoinCollected", SendMessageOptions.DontRequireReceiver);
        gameObject.SetActive(false);
    }
}
