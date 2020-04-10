using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DInteractableObject : NetworkBehaviour, IInteract
{
    public float INTERACT_DISTANCE = 0.5f;
    public Sprite interactSprite;

    public virtual void Interact()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
               
                if (Vector3.Distance(transform.position, player.transform.position) < INTERACT_DISTANCE)
                {
                    if (interactSprite == null)
                        interactSprite = GetComponentInChildren<SpriteRenderer>().sprite;
                    DGameSystem.interactButton.Regist(this, interactSprite);
                    return;
                }
            }
        }
        DGameSystem.interactButton.UnRegist(this);
    }
}

