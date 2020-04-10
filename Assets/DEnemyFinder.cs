using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEnemyFinder : MonoBehaviour
{
    public string[] enemyTags;
    public GameObject enemy;

    List<GameObject> enemies;

    public virtual GameObject FindEnemy(GameObject pivot, float findRadius)
    {
        if (enemy != null)
        {
            if (Vector3.Distance(pivot.transform.position, enemy.transform.position) < findRadius)
                return enemy;
            else
                enemy = null;
        }

        enemies = new List<GameObject>();

        for (int i = 0; i < enemyTags.Length; i++)
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag(enemyTags[i]);
            if (enemyList != null)
                enemies.AddRange(enemyList);
        }

        if (enemies.Count == 0)
            return null;

        GameObject nearest = enemies[0];

        float nearestDistance = Vector3.Distance(pivot.transform.position, nearest.transform.position);

        for (int j = 1; j < enemies.Count; j++)
        {
            float d = Vector3.Distance(transform.position, enemies[j].transform.position);

            if (d < nearestDistance)
            {
                nearest = enemies[j];
                nearestDistance = d;
            }
        }

        if (nearestDistance < findRadius)
            return nearest;
        else
            return null;
    }

}
