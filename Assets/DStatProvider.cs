using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DStatProvider : MonoBehaviour
{
    public DStat stat;

    private void Awake()
    {
        if (GetComponent<DMovement>() != null)
            GetComponent<DMovement>().data = stat;
        if (GetComponent<DBattle>() != null)
            GetComponent<DBattle>().stat = stat;
        if (GetComponent<DFollow>() != null)
            GetComponent<DFollow>().data = stat;
    }

}
