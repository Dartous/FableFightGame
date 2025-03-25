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

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnEnemies ()
    {
        //set temp wave strength for the loop
        float tempWaveStrength = currentWaveStrength;

        //while temp wave strength is above zero
        while (tempWaveStrength > 0)
        {
            //for every enemy in the list
            for (int i = 0; i < enemies.Length; i++)
            {
                //calculate a random amount of enemies, fitting within the set wave strength
                int randomAmount = Random.Range(0, currentWaveStrength / enemyStrength[i]);

                //reduce the tempWaveStrength according to the number of enemies summoned
                tempWaveStrength -= randomAmount * enemyStrength[i];

                //for each enemy
                for (int j = 0; j < randomAmount; j++)
                {
                    //select a random spawn location
                    int randomSpawn = Random.Range(0, spawnPositions.Length);

                    //instantiate an appropriate enemy there
                    Instantiate(enemies[i], spawnPositions[randomSpawn].transform.position, enemies[i].transform.rotation);
                }
            }
        }

        //set the new spawn rate
        if (currentSpawnRate > minSpawnRate)
        {
            currentSpawnRate += spawnRateIncrease;
        }

        //increase wave strength
        currentWaveStrength += waveStrengthIncrease;
    }
}
