using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartPosition : MonoBehaviour
{
    public float range = 2;
    void Start()
    {
        transform.position = new Vector3(Random.RandomRange(-range, range), Random.RandomRange(-range, range));
    }
}
