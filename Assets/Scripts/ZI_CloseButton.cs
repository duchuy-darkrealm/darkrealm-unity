using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZI_CloseButton : MonoBehaviour
{
    public GameObject closeObject;

    public void Close()
    {
        DGameSystem.canvasControl.SetActive(true);
        closeObject.SetActive(false);
    }
}
