using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DParticleSystem : MonoBehaviour
{
    public float existTime = 2f;

    private void OnEnable()
    {
        GetComponent<ParticleSystem>().Play();
        Invoke("DisableGameObject", existTime);
    }

    void DisableGameObject() {
        gameObject.SetActive(false);
    }
}
