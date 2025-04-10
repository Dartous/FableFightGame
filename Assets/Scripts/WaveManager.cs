using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    #region Initialize Variables
    [Header("Add these from MOST powerful to LEAST powerful (measured by according enemyStrength)")]
    public GameObject[] enemies;
    public int[] enemyStrength;
    public GameObject[] spawnPositions;

    [Header("Wave Stats")]
    public float firstSpawnDelay = 2;
    public float spawnRate = 5;
    public float spawnRateIncrease = -0.5f;
    public float minSpawnRate = 2;
    public int startingWaveStrength = 1;
    public int waveStrengthIncrease = 1;
    public float spawnPositionSpread = 1.5f;

    [Header("For TESTING only")]
    public float currentSpawnRate;
    public int currentWaveStrength;
    //public GameObject knight;
    //public GameObject peasant;
    //public GameObject blacksmith;
    //public GameObject dog;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentWaveStrength = startingWaveStrength;
        currentSpawnRate = spawnRate;

        InvokeRepeating("SpawnEnemies", firstSpawnDelay, currentSpawnRate);
    }

    private void SpawnEnemies ()
    {
        //set temp wave strength for the loop
        int tempWaveStrength = currentWaveStrength;

        //while temp wave strength is above zero
        while (tempWaveStrength > 0)
        {
            //for every enemy in the list
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemyStrength[i] > tempWaveStrength)
                {
                    continue;
                }

                int randomAmount = 0;
                //try 
                //{
                    //calculate a random amount of enemies, fitting within the set wave strength
                    randomAmount = Random.Range(0, currentWaveStrength / enemyStrength[i]);

                if (i == enemies.Length - 1)
                {
                    randomAmount = tempWaveStrength;
                }
                //}
                //catch
                //{
                //    print("enemyStr is more than tempWaveStr");
                //}

                //if (randomAmount > 0) 
                //{
                    //reduce the tempWaveStrength according to the number of enemies summoned
                    tempWaveStrength -= randomAmount * enemyStrength[i];

                    //for each enemy
                    for (int j = 0; j < randomAmount; j++)
                    {
                        //select a random spawn location
                        int randomSpawn = Random.Range(0, spawnPositions.Length);

                        //randomize Z coordinate in order for enemies to not spawn in the same pixel
                        Vector3 spawnPos = spawnPositions[randomSpawn].transform.position;
                        float randomZ = Random.Range(spawnPositions[randomSpawn].transform.position.z - spawnPositionSpread, spawnPositions[randomSpawn].transform.position.z + spawnPositionSpread);
                        Vector3 newSpawnPos = new Vector3(spawnPos.x, spawnPos.y, randomZ);

                        //instantiate an appropriate enemy there
                        Instantiate(enemies[i], newSpawnPos, enemies[i].transform.rotation);
                    }
                //}
                
            }
        }

        //set the new spawn rate
        if (currentSpawnRate > minSpawnRate)
        {
            currentSpawnRate += spawnRateIncrease;
        }
        
        //make sure it can't go below the threshold
        if (currentSpawnRate < minSpawnRate)
        {
            currentSpawnRate = minSpawnRate;
        }

        //increase wave strength
        currentWaveStrength += waveStrengthIncrease;
    }
}
