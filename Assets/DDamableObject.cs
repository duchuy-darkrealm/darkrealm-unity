using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DDamableObject : MonoBehaviour
{
    public int maxHit = 3;
    public int hitCount = 3;
    public float respawn_time = 3f;
    public GameObject[] dropItems;
    public float[] dropRates;

    private void OnEnable()
    {
        hitCount = maxHit;
    }
    public void GetHit(DHitParam hit)
    {
        GameObject flyingtext = DGameSystem.LoadPool("TextDame", transform.position + new Vector3(0, 0.5f));
        flyingtext.GetComponent<TextMeshPro>().text = Convert.ToInt32(hit.dame).ToString();

        hitCount -= 1;


        if (hitCount <= 0)
        {
            float random = UnityEngine.Random.Range(0, 100);
            Debug.Log("ramdom: " + random);
            float pivot = 0;
            for (int i = 0; i < dropRates.Length; i++)
            {
                if (pivot < random && random < pivot + dropRates[i])
                {
                    DGameSystem.LoadPool("Ghost", transform.position);
                }
                pivot += dropRates[i];
            }

            if (respawn_time < 0)
                Destroy(gameObject);
            else
                Invoke("Respawn", respawn_time);

            gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        hitCount = maxHit;
    }
}
