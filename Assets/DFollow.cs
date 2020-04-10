using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DMovementLeftRight))]
[RequireComponent(typeof(DMovementExecutor))]
[RequireComponent(typeof(DEnemyFinder))]
public class DFollow : MonoBehaviour
{
    public GameObject owner;
    public DStat data;
    public float MAX_DISTANCE = 2f;
    public float MIN_DISTANCE = 0.5f;

    float MIN_RANDOM_LOW = 0.5f;
    float MIN_RANDOM_HIGH = 2.5f;
    float MAX_RANDOM_LOW = 0.5f;
    float MAX_RANDOM_HIGH = 1.5f;
    float ATTACK_DURATION = 2f;
    float ATTACK_STOP_DURATION = 2f;

    public float maxDistance;
    public float minDistance;

    public string direction = "left";

    string state;
    Vector3 vectorToTarget;

    float RANDOM_TIME = 5f;
    float count;

    DEnemyFinder enemyFinder;
    public GameObject enemy;

    bool isAttacking = true;
    float attackCount = 0;

    private void Start()
    {
        maxDistance = Random.Range(MAX_DISTANCE * MAX_RANDOM_LOW, MAX_DISTANCE * MAX_RANDOM_HIGH);
        minDistance = Random.Range(MIN_DISTANCE * MIN_RANDOM_LOW, MIN_DISTANCE * MIN_RANDOM_HIGH);
        enemyFinder = GetComponent<DEnemyFinder>();
        attackCount = ATTACK_DURATION;
    }

    private void Update()
    {
        GetComponent<DAnimator>().attacking = false;

        RandomMinMaxDistance();
        if (owner == null)
            owner = DGameSystem.player;
        if (owner == null) return;

        float distanceToOwner = Vector3.Distance(transform.position, owner.transform.position);
        
        enemy = enemyFinder.FindEnemy(owner, MAX_DISTANCE);

        if (distanceToOwner < MIN_DISTANCE)
        {
            GetComponent<DMovement>().enabled = true;
            GetComponent<DMovementExecutor>().enabled = true;
            state = "stand";
        }
        else if (distanceToOwner > MAX_DISTANCE)
        {
            GetComponent<DMovement>().enabled = false;
            GetComponent<DMovementExecutor>().enabled = false;
            vectorToTarget = owner.transform.position - transform.position;
            vectorToTarget = vectorToTarget * data.speed * 1.5f / distanceToOwner;
            GetComponent<Rigidbody2D>().velocity = vectorToTarget;
            state = "follow";
            direction = DCommonUtils.GetLeftRightFacingToTarget(transform, owner.transform);
            if (direction != "vertical")
                GetComponent<DAnimator>().spritesheet = DCommonUtils.GetSpriteSheet(data, "run_" + direction);
        }

        if (state != "follow")
        {
            enemy = enemyFinder.FindEnemy(owner, MAX_DISTANCE);
            if (enemy != null)
            {
                if (isAttacking)
                {
                    Attack();
                    attackCount -= Time.deltaTime;
                    if (attackCount < 0)
                    {
                        isAttacking = false;
                        attackCount = ATTACK_STOP_DURATION;
                    }
                }
                else
                {
                    GetComponent<DMovement>().enabled = true;
                    GetComponent<DMovementExecutor>().enabled = true;
                    attackCount -= Time.deltaTime;
                    if (attackCount < 0)
                    {
                        isAttacking = true;
                        attackCount = ATTACK_DURATION;
                    }
                }
            }
            else
            {
                Debug.Log("not found enemy!");
                GetComponent<DMovement>().enabled = true;
                GetComponent<DMovementExecutor>().enabled = true;
            }
        }

        void Attack()
        {
            GetComponent<DMovement>().enabled = false;
            GetComponent<DMovementExecutor>().enabled = false;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy > data.attackRange)
            {
                vectorToTarget = enemy.transform.position - transform.position;
                vectorToTarget = vectorToTarget * data.speed * 1.5f / distanceToEnemy;
                GetComponent<Rigidbody2D>().velocity = vectorToTarget;
                direction = DCommonUtils.GetLeftRightFacingToTarget(transform, enemy.transform);
                GetComponent<DAnimator>().attacking = true;
                GetComponent<DAnimator>().spritesheet = DCommonUtils.GetSpriteSheet(data, "run_" + direction);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
                direction = DCommonUtils.GetLeftRightFacingToTarget(transform, enemy.transform);
                GetComponent<DAnimator>().attacking = true;
                GetComponent<DAnimator>().spritesheet = DCommonUtils.GetSpriteSheet(data, "attack_" + direction);
            }  

        }
    }

    private void RandomMinMaxDistance()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = RANDOM_TIME;
            maxDistance = Random.Range(MAX_DISTANCE * MAX_RANDOM_LOW, MAX_DISTANCE * MAX_RANDOM_HIGH);
            minDistance = Random.Range(MIN_DISTANCE * MIN_RANDOM_LOW, MIN_DISTANCE * MIN_RANDOM_HIGH);
        }
    }

    public void ExecuteAttack()
    {

    }
}
