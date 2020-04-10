using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DHitObject : MonoBehaviour
{
    public DHitParam hit;
    public virtual void ReceiveParam(DHitParam hitParam)
    {
        hit = hitParam;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != hit.owner && hit.targetTags.Contains(collision.tag))
        {
            collision.gameObject.SendMessage("GetHit", hit, SendMessageOptions.DontRequireReceiver);
        }
    }
}
