using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystemRenderer))]
public class DOrderFixerParticle : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<ParticleSystemRenderer>().sortingOrder = (int)(-transform.position.y * 10);
    }
}
