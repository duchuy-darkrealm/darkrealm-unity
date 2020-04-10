using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class DDoor : NetworkBehaviour
{
    public float destination_x;
    public float destination_y;

    private float INTERACT_DISTANCE = 0.5f;

    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < INTERACT_DISTANCE) {
                    player.transform.position = new Vector3(destination_x, destination_y,0);
                    GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                    Vector3 pos = camera.transform.position;
                    pos.x = destination_x;
                    pos.y = destination_y;
                    camera.transform.position = pos;
                }
            }
        }
    }
}
