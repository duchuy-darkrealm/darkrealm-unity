using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTower : MonoBehaviour
{
    public GameObject attacker;

    DEnemyFinder enemyFinder;
    public GameObject enemy;

    public float ATTACK_RATE;
    public float DAME;
    public float RADIUS = 2f;
    public float BULLET_SPEED = 100f;

    float count = 0;

    private void Start()
    {
        enemyFinder = GetComponent<DEnemyFinder>();
    }

    private void Update()
    {
        enemy = enemyFinder.FindEnemy(gameObject, RADIUS);
        if (enemy == null)
            return;
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = ATTACK_RATE;
            GameObject bullet = DGameSystem.LoadPool("Bullet", attacker.transform.position);
            bullet.GetComponent<Rigidbody2D>().velocity = DGameSystem.GoToTargetVector(attacker.transform.position, enemy.transform.position, BULLET_SPEED);
        }
    }
}
