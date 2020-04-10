using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DMovement : MonoBehaviour
{
    public string state;

    public DStat data;
    public DStat hatData;

    [HideInInspector]
    public DAnimator animator;
    [HideInInspector]
    public Rigidbody2D rb2d;
    private float pauseTimePoint;

    public string Facing
    {
        get
        {
            if (state == "stand_up" || state == "go_up" || state == "run_up") return "up";
            else if (state == "stand_down" || state == "go_down" || state == "run_down") return "down";
            else if (state == "stand_left" || state == "go_left" || state == "run_left") return "left";
            else if (state == "stand_right" || state == "go_right" || state == "run_right") return "right";
            else return "down"; // Default
        }
    }
    public Vector3 ConvertRotation(string sourceDirection)
    {
        Hashtable degree = new Hashtable();
        degree.Add("down", 0f);
        degree.Add("right", 90f);
        degree.Add("up", 180f);
        degree.Add("left", 270f);

        float degreeObj = (float)degree[sourceDirection];
        float degreePlayer = (float)degree[Facing];
        return new Vector3(0f, 0f, degreePlayer - degreeObj);
    }

    public virtual void Awake()
    {
        animator = GetComponent<DAnimator>();
        state = "stand_down";
    }

    public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        pauseTimePoint = Time.time;
    }

    public virtual void Update()
    {
        if (animator == null) Debug.Log("DAminator not found, if player: DAnimator is game object component, if is monster: DAnimator is in child game object component");
        if (Time.time < pauseTimePoint) rb2d.velocity = new Vector3(0f, 0f);
        else
            UpdateMovement();
    }

    public virtual void UpdateMovement()
    {
        animator.spritesheet = ConvertStringToSprites(state);
        if (hatData != null)
        {
            animator.hatsheet = ConvertStringToSpritesHat(state);
        }

        string status = state.Substring(0, state.IndexOf("_"));

        if (status == "stand")
            rb2d.velocity = new Vector3(0f, 0f);
        else if (status == "run")
            rb2d.velocity = ConvertDirection(state, data.speed *1.5f);
        else
            rb2d.velocity = ConvertDirection(state, data.speed);
    }

    public virtual Vector3 ConvertDirection(string state, float value)
    {
        string direction = state.Substring(state.IndexOf("_") + 1);
        if (direction == "up") return new Vector3(0f, value);
        else if (direction == "down") return new Vector3(0f, -value);
        else if (direction == "left") return new Vector3(-value, 0f);
        else if (direction == "right") return new Vector3(value, 0f);
        else return new Vector3(0f, 0f);
    }

    public void Pause(float pausetime)
    {
        pauseTimePoint = Time.time + pausetime;
    }
    public void Stand()
    {
        if (state == "go_right") state = "stand_right";
        else if (state == "go_left") state = "stand_left";
        else if (state == "go_up") state = "stand_up";
        else if (state == "go_down") state = "stand_down";
    }

    public void FaceToGameObject(GameObject obj)
    {
        float player_posx, player_posy, npc_posx, npc_posy;

        player_posx = obj.transform.position.x;
        player_posy = obj.transform.position.y;
        npc_posx = transform.position.x;
        npc_posy = transform.position.y;

        float distance_x = Mathf.Abs(player_posx - npc_posx);
        float distance_y = Mathf.Abs(player_posy - npc_posy);

        if (distance_x < distance_y) //player and npc standing on vertical line
        {
            if (player_posy < npc_posy) //player is below the npc
            {
                obj.GetComponent<DMovement>().state = "stand_up";
                state = "stand_down";
            }
            else //player is above the npc
            {
                obj.GetComponent<DMovement>().state = "stand_down";
                state = "stand_up";
            }
        }
        else //player and npc standing on horizontal line
        {
            if (player_posx < npc_posx) //player is on the left of npc
            {
                obj.GetComponent<DMovement>().state = "stand_right";
                state = "stand_left";
            }
            else //player is on the right of npc
            {
                obj.GetComponent<DMovement>().state = "stand_left";
                state = "stand_right";
            }
        }
        obj.GetComponent<DMovement>().Update();
        Update();
    }

    public void Attack(Vector3 position, DStat data)
    {
        string attackDirection = Facing;
        Stand();
        if (attackDirection == "up") animator.StartPriorAnimation(data.attack_up);
        else if (attackDirection == "down") animator.StartPriorAnimation(data.attack_down);
        else if (attackDirection == "left") animator.StartPriorAnimation(data.attack_left);
        else if (attackDirection == "right") animator.StartPriorAnimation(data.attack_right);
        else animator.StartPriorAnimation(data.attack_up);
        Update();
        //Pause(data.pauseTimeAfterAttack);
    }

    public virtual Sprite[] ConvertStringToSprites(string input)
    {
        Sprite[] result = null;

        if (input == "stand_up")
            result = data.stand_up;
        else if (input == "stand_down")
            result = data.stand_down;
        else if (input == "stand_left")
            result = data.stand_left;
        else if (input == "stand_right")
            result = data.stand_right;

        else if (input == "go_up")
            result = data.go_up;
        else if (input == "go_down")
            result = data.go_down;
        else if (input == "go_left")
            result = data.go_left;
        else if (input == "go_right")
            result = data.go_right;

        else if (input == "attack_up")
            result = data.attack_up;
        else if (input == "attack_down")
            result = data.attack_down;
        else if (input == "attack_left")
            result = data.attack_left;
        else if (input == "attack_right")
            result = data.attack_right;

        else if (input == "run_up")
            result = data.run_up;
        else if (input == "run_down")
            result = data.run_down;
        else if (input == "run_left")
            result = data.run_left;
        else if (input == "run_right")
            result = data.run_right;

        return result;
    }


    public Sprite[] ConvertStringToSpritesHat(string input)
    {
        Sprite[] result = null;

        if (input == "stand_up")
            result = hatData.stand_up;
        else if (input == "stand_down")
            result = hatData.stand_down;
        else if (input == "stand_left")
            result = hatData.stand_left;
        else if (input == "stand_right")
            result = hatData.stand_right;

        else if (input == "go_up")
            result = hatData.go_up;
        else if (input == "go_down")
            result = hatData.go_down;
        else if (input == "go_left")
            result = hatData.go_left;
        else if (input == "go_right")
            result = hatData.go_right;

        else if (input == "attack_up")
            result = hatData.attack_up;
        else if (input == "attack_down")
            result = hatData.attack_down;
        else if (input == "attack_left")
            result = hatData.attack_left;
        else if (input == "attack_right")
            result = hatData.attack_right;

        else if (input == "run_up")
            result = hatData.run_up;
        else if (input == "run_down")
            result = hatData.run_down;
        else if (input == "run_left")
            result = hatData.run_left;
        else if (input == "run_right")
            result = hatData.run_right;

        return result;
    }

    // bool changePlayerFacing = false
    /*
    void AttackAtDirection(string attackDirection)
    {
        Stand();
        if (attackDirection == "up") animator.StartPriorAnimation(data.attack_up);
        else if (attackDirection == "down") animator.StartPriorAnimation(data.attack_down);
        else if (attackDirection == "left") animator.StartPriorAnimation(data.attack_left);
        else if (attackDirection == "right") animator.StartPriorAnimation(data.attack_right);
        else animator.StartPriorAnimation(data.attack_up);
        Update();
        Pause(data.pauseTimeAfterAttack);
    }


    if (!changePlayerFacing) { AttackAtDirection(Facing); return; }

    string direction = "down";

    float horizontal = Mathf.Abs(transform.position.x - position.x);
    float vertical = Mathf.Abs(transform.position.y - position.y);

    if (vertical > horizontal) 
    {
        if (position.y > transform.position.y) direction = "up";
        else direction = "down";               
    }
    else
    {
        if (position.x > transform.position.x) direction = "right";
        else direction = "left";            
    }
    AttackAtDirection(direction);
    */
}

