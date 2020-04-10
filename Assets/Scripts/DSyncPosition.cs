using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DSyncPosition : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;
    [SyncVar]
    private string syncState;

    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 15;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.fixedDeltaTime * lerpRate);
            GetComponent<DMovement>().state = syncState;
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos,string state)
    {
        syncPos = pos;
        syncState = state;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            string state = GetComponent<DMovement>().state;
            CmdProvidePositionToServer(myTransform.position,state);
        }
        
    }

}
