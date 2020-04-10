using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMovementExecutorRandom : DMovementExecutor
{
    public string[] randomMovements;
    public float[] randomMovementDurations;

    private float countMovement;

    private void Start()
    {
        int random = Random.Range(0, randomMovements.Length);
        countMovement = randomMovementDurations[random];
        GetComponent<DMovement>().state = randomMovements[random];
    }

    public override void Update()
    {
        countMovement -= Time.deltaTime;
        if (countMovement < 0)
        {
            NextMovement();
        }
    }

    public override void NextMovement()
    {
        int random = Random.Range(0, randomMovements.Length);
        countMovement = randomMovementDurations[random];
        GetComponent<DMovement>().state = randomMovements[random];
    }
}
