using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DInteractButton : MonoBehaviour
{
    IInteract interactScript;
    Image interactImage;

    private void Start()
    {
        interactImage = GameObject.Find("InteractImage").GetComponent<Image>();
    }

    public void InteractWithObject()
    {
        if (interactScript != null)
        {
            interactScript.Interact();
            interactScript = null;
            interactImage.sprite = null;
        }
        else
        {
            if (DGameSystem.player != null)
            {
                DGameSystem.player.GetComponent<DBattle>().Attack();
            }
        }
       
    }

    public void Regist(IInteract interact, Sprite image) {
        interactScript = interact;
        interactImage.sprite = image;
    }

    public void UnRegist(IInteract interact) {
        if (interact == interactScript)
        {
            interactScript = null;
            interactImage.sprite = null;
        }
            
    }

}
