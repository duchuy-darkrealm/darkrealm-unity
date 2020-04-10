using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCommonUtils : MonoBehaviour
{
    public static string GetLeftRightDirection(Vector3 velocity)
    {
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            if (velocity.x < 0) return "left";
            else
                return "right";
        }
        return "vertical";
    }

    public static string GetLeftRightFacingToTarget(Transform current, Transform target)
    {
        if (current.position.x > target.position.x) return "left";
        else
            return "right";
    }

    public static Sprite[] GetSpriteSheet(DStat data, string movementState)
    {
        Sprite[] result = null;

        if (movementState == "stand_up")
            result = data.stand_up;
        else if (movementState == "stand_down")
            result = data.stand_down;
        else if (movementState == "stand_left")
            result = data.stand_left;
        else if (movementState == "stand_right")
            result = data.stand_right;

        else if (movementState == "go_up")
            result = data.go_up;
        else if (movementState == "go_down")
            result = data.go_down;
        else if (movementState == "go_left")
            result = data.go_left;
        else if (movementState == "go_right")
            result = data.go_right;

        else if (movementState == "attack_up")
            result = data.attack_up;
        else if (movementState == "attack_down")
            result = data.attack_down;
        else if (movementState == "attack_left")
            result = data.attack_left;
        else if (movementState == "attack_right")
            result = data.attack_right;

        else if (movementState == "run_up")
            result = data.run_up;
        else if (movementState == "run_down")
            result = data.run_down;
        else if (movementState == "run_left")
            result = data.run_left;
        else if (movementState == "run_right")
            result = data.run_right;

        return result;
    }

    public static Vector3 GetVectorToTarget(Transform current, Transform target, float stopDistance)
    {
        float distanceToTarget = Vector3.Distance(current.position, target.position);
        if (distanceToTarget <= stopDistance)
        {
            return new Vector3(0f, 0f);
        }
        else
        {
            Vector3 vectorToTarget = target.position - current.position;
            return vectorToTarget / distanceToTarget;
        }
    }
}
