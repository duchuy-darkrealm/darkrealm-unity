using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DCamera : MonoBehaviour
{
    public GameObject target;
    public float maxDistance = 2f;
    public float speed = 25f;

    public GameObject background;

    private Vector3 vectorToTarget;
    
    // Time.deltaTime bug
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void Update()
    {
        if (target == null) return;

        GoToTargetPosition(target, maxDistance);
    }

    void GoToTargetPosition(GameObject target, float maxDistanceToTarget)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= maxDistanceToTarget)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f);
            return;
        }
        else
        {
            vectorToTarget = target.transform.position - transform.position;
            vectorToTarget = vectorToTarget * speed / distanceToTarget;
            GetComponent<Rigidbody2D>().velocity = vectorToTarget;
        }
    }

    public void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }
}
