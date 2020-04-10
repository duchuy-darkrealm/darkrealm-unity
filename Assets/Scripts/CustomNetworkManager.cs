using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Android;

public class CustomNetworkManager : NetworkManager
{
    public Text networkAddressText;

    public void Start()
    {
        base.Start();
    }


    public void StartClient()
    {
        networkAddress = networkAddressText.text;
        base.StartClient();
    }

    public void StartHost()
    {
        base.StartHost();
        DGameSystem.networkMenu.SetActive(false);
    }

}
