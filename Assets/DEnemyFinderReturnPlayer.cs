using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEnemyFinderReturnPlayer : DEnemyFinder
{
    public override GameObject FindEnemy(GameObject pivot, float findRadius)
    {
        return DGameSystem.player;
    }

}
