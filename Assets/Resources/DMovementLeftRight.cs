using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMovementLeftRight : DMovement
{
    string currentDirection;
    float VERTICAL_RANDOM;
    float randomVerticalValue;
    float randomCount;

    public override void Start()
    {
        base.Start();
        currentDirection = "left";
        VERTICAL_RANDOM = data.speed / 2;
    }

    public override Sprite[] ConvertStringToSprites(string state)
    {
        int pos = state.IndexOf("_");
        string first = state.Substring(0, pos);
        string direction = DCommonUtils.GetLeftRightDirection(GetComponent<Rigidbody2D>().velocity);

        if (direction != "vertical")
            currentDirection = direction;

        return base.ConvertStringToSprites(first + "_" + currentDirection);
    }

    public override Vector3 ConvertDirection(string state, float value)
    {
        
        string direction = state.Substring(state.IndexOf("_") + 1);
        if (direction == "up") return new Vector3(0, value);
        else if (direction == "down") return new Vector3(0, -value);
        else if (direction == "left") return new Vector3(-value, randomVerticalValue);
        else if (direction == "right") return new Vector3(value, randomVerticalValue);
        else return new Vector3(0f, 0f);
    }

    public override void Update()
    {
        base.Update();
        RandomVertical();
    }

    public void RandomVertical()
    {
        randomCount -= Time.deltaTime;
        if (randomCount < 0)
        {
            randomCount = 3f;
            randomVerticalValue = Random.Range(-VERTICAL_RANDOM / 2, VERTICAL_RANDOM / 2);
        }
        
    }
}
