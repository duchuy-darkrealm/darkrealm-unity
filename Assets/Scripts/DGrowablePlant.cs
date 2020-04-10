using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGrowablePlant : MonoBehaviour
{
    float RANDOM_RANGE = 3f;
    float RANDOM_COIN_DISTANCE = 0.5f;

    public DPlant plant;
    [HideInInspector]
    public string state;
    DAnimator anim;
    float countdown;
    float countdownCoin;
    bool spawnedCoin = false;

    private void OnEnable()
    {
        state = "ground";
        countdown = Random.Range(plant.growTime - RANDOM_RANGE, plant.growTime + RANDOM_RANGE);
        countdownCoin = Random.Range(plant.coinSpawn - RANDOM_RANGE, plant.coinSpawn + RANDOM_RANGE);
        anim = GetComponent<DAnimator>();
        anim.spritesheet = plant.ground;
    }

    public string changeToNextState(string state)
    {
        if (state == "ground")
        {
            return "small";
        }
        else if (state == "small")
        {
            return "medium";
        }
        else if (state == "medium")
        {
            return "large";
        }
        else
            return "large";
    }

    void Update()
    {
        if (state != "large")
        {
            countdown -= Time.deltaTime;
            if (countdown < 0)
            {
                countdown = Random.Range(plant.growTime - RANDOM_RANGE, plant.growTime + RANDOM_RANGE);
                state = changeToNextState(state);
            }
        }
        else
        {
            if (!spawnedCoin)
            {
                countdownCoin -= Time.deltaTime;
                if (countdownCoin < 0)
                {
                    spawnedCoin = true;
                    countdownCoin = Random.Range(plant.coinSpawn - RANDOM_RANGE, plant.coinSpawn + RANDOM_RANGE);
                    Vector3 position = transform.position;
                    position = new Vector3(Random.Range(position.x - RANDOM_COIN_DISTANCE, position.x + RANDOM_COIN_DISTANCE), Random.Range(position.y - RANDOM_COIN_DISTANCE, position.y + RANDOM_COIN_DISTANCE), 0);
                    GameObject coin = DGameSystem.LoadPool("Coin", position);
                    coin.GetComponent<DCoin>().value = plant.coinValue;
                    coin.GetComponent<DCoin>().owner = gameObject;
                    coin.transform.localScale = new Vector3(plant.coinSize, plant.coinSize);
                }
            }
        }

        if (state == "ground")
            anim.spritesheet = plant.ground;
        if (state == "small")
            anim.spritesheet = plant.small;
        if (state == "medium")
            anim.spritesheet = plant.medium;
        if (state == "large")
            anim.spritesheet = plant.large;
    }

    public void CoinCollected()
    {
        spawnedCoin = false;
        countdownCoin = plant.growTime;
    }
}
