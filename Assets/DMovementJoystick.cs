using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMovementJoystick : DMovement
{
    public override void UpdateMovement()
    {
        animator.spritesheet = ConvertStringToSprites(state);
        if (hatData != null)
        {
            animator.hatsheet = ConvertStringToSpritesHat(state);
        }

        rb2d.velocity = new Vector3(DGameSystem.joystick.Horizontal, DGameSystem.joystick.Vertical) * data.speed; 
    }
}
