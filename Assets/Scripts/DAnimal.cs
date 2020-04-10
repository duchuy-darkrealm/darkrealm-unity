using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DMovementExecutor))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DAnimator))]
[RequireComponent(typeof(DBattle))]
public class DAnimal : MonoBehaviour
{
    public GameObject owner;
    public DStat stat;

    public enum STATE {
        ATTACK,
        FREE
    }

    private string enemyTag;

    private GameObject enemies; //TODO: add multi target attack!
    private GameObject enemy;

    private STATE state = STATE.FREE;
    private float visionRange = 2f;

    private float attackCount = 0;

    private void OnEnable()
    {
        attackCount = stat.attackCountDown;
    }

    private void Update()
    {
        if (owner == null) enemyTag = "Player";
        else
            enemyTag = "Monster";

        if (enemy != null)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < visionRange)
                state = STATE.ATTACK;
            else
            {
                enemy = FindEnemy(enemyTag);
                if (enemy != null) state = STATE.ATTACK;
                else
                    state = STATE.FREE;
            }
        }
        else
        {
            enemy = FindEnemy(enemyTag);
            if (enemy != null) state = STATE.ATTACK;
            else
                state = STATE.FREE;
        }

        if (state == STATE.ATTACK)
        {
            GetComponent<DMovementExecutor>().enabled = false;
            GetComponent<DMovement>().enabled = false;

            if (Vector3.Distance(transform.position, enemy.transform.position) < stat.attackRange)
            {
                GetComponent<DAnimator>().spritesheet = getSprite("attack",enemy);
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                attackCount -= Time.deltaTime;
                if (attackCount < 0)
                {
                    attackCount = stat.attackCountDown;
                    GetComponent<DBattle>().ApplyDame(enemy);
                }
            }
            else
            {
                GetComponent<DAnimator>().spritesheet = getSprite("run", enemy);
                GetComponent<Rigidbody2D>().velocity = DGameSystem.GoToTargetVector(transform.position, enemy.transform.position, stat.speed *1.5f);
            } 
        }
        else // STATE == FREE
        {
            if (owner != null)
            {
                GetComponent<DAnimator>().spritesheet = getSprite("go", owner);
                GetComponent<Rigidbody2D>().velocity = DGameSystem.GoToTargetVector(transform.position, enemy.transform.position, stat.speed);
            }
            else
            {
                GetComponent<DMovementExecutor>().enabled = true;
                GetComponent<DMovement>().enabled = true;
            }
        }
            
    }

    public GameObject FindEnemy(string enemyTag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length == 0)
            return null;

        foreach (GameObject enem in enemies)
        {
            if (Vector3.Distance(transform.position, enem.transform.position) < visionRange)
            {
                return enem;
            }
        }

        return null;
    }

    private Sprite[] getSprite(string action, GameObject target)
    {
        if (action == "attack")
        {
            if (transform.position.x > target.transform.position.x)
                return stat.attack_left;
            else
                return stat.attack_right;
        }
        else if (action == "run")
        {
            if (transform.position.x > target.transform.position.x)
                return stat.run_left;
            else
                return stat.run_right;
        }
        else if (action == "go")
        {
            if (transform.position.x > target.transform.position.x)
                return stat.go_left;
            else
                return stat.go_right;
        }

        return null;
    }
}



