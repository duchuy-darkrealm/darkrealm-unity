using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DMovement))]
public class DMovePC : MonoBehaviour
{

    public DStat stat;

    [HideInInspector]
    public DCamera camera;
    Joystick joystick;
    DMovement playerMovement;
    Rigidbody2D rigid;

    private void Start()
    {
        playerMovement = GetComponent<DMovement>();
        rigid = GetComponent<Rigidbody2D>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DCamera>();

        GameObject joystickObj = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickObj.GetComponent<Joystick>();
        camera.target = this.gameObject;
    }

    void Update()
    {
        rigid.velocity = new Vector3(joystick.Horizontal, joystick.Vertical);

        // Joystick controller
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        if (x == 0 && y == 0)
        {
            playerMovement.Stand();
            return;
        }

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0) playerMovement.state = "go_right";
            else playerMovement.state = "go_left";
        }
        else
        {
            if (y > 0) playerMovement.state = "go_up";
            else playerMovement.state = "go_down";
        }
    }
}

