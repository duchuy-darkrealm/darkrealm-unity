using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DHitObjectDirectAttack : MonoBehaviour
{ 
    public virtual void ReceiveParam(DHitParam hitParam){
        if (hitParam.target!=null)
            hitParam.target.SendMessage("GetHit", hitParam, SendMessageOptions.DontRequireReceiver);
        gameObject.SetActive(false);
    }
}
