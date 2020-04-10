using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEnemySpawner : MonoBehaviour
{
    float countdown = 10;
    float SPAWN_RATE = 3f;

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0) {
            //countdown = Random.Range(SPAWN_RATE * 0.5f, SPAWN_RATE*1.5f) ;
            countdown = 100;
            for (int i = 0; i < 40; i++) {
                GameObject enemy = DGameSystem.LoadPool("Enemy" + Random.Range(1, 4), transform.position);
            }
        }
    }

}
