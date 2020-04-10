using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class DAnimatorLight : DAnimator
{
    public override void DrawNextFrame()
    {
        frameshow++;

        if (frameshow >= spritesheet.Length)
        {
            if (DoEndOfAnimation()) return;
            frameshow = 0;
        }
        GetComponent<SpriteRenderer>().sprite = spritesheet[frameshow];

        if (hatsheet.Length != 0)
            hatRenderer.sprite = hatsheet[frameshow];
    }
}
