using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class WaveManager : MonoBehaviour
{
    #region Initialize Variables
    [Header("Add these from MOST powerful to LEAST powerful (measured by according enemyStrength)")]
    public GameObject[] enemies;
    public int[] enemyStrength;
    public GameObject[] spawnPositionsWave1;
    public GameObject[] spawnPositionsWave3;
    public GameObject[] spawnPositionsWave6;
    private GameObject[] currentSpawnPositions;
    public TMP_Text waveNumbertxt;
    //[HideInInspector]
    public static int waveCounter;

    [Header("Unavailable lanes")]
    public GameObject[] unavailableLanes;

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
    #endregion

    // Start is called before the first frame update
    void Start()
    {

        //initiate stuff
        currentWaveStrength = startingWaveStrength;
        currentSpawnRate = spawnRate;
        waveCounter = 1;
        waveNumbertxt.text = waveCounter.ToString();
        currentSpawnPositions = spawnPositionsWave1;

        InvokeRepeating("SpawnEnemies", firstSpawnDelay, currentSpawnRate);
    }

    private void SpawnEnemies ()
    {
        //update the wave counter text
        waveNumbertxt.text = waveCounter.ToString();

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

                //calculate a random amount of enemies, fitting within the set wave strength
                randomAmount = Random.Range(0, currentWaveStrength / enemyStrength[i]);

                if (i == enemies.Length - 1)
                {
                    randomAmount = tempWaveStrength;
                }

                //reduce the tempWaveStrength according to the number of enemies summoned
                tempWaveStrength -= randomAmount * enemyStrength[i];

                //for each enemy
                for (int j = 0; j < randomAmount; j++)
                {
                    //select a random spawn location
                    int randomSpawn = Random.Range(0, currentSpawnPositions.Length);

                    //randomize Z coordinate in order for enemies to not spawn in the same pixel
                    Vector3 spawnPos = currentSpawnPositions[randomSpawn].transform.position;
                    float randomZ = Random.Range(currentSpawnPositions[randomSpawn].transform.position.z - spawnPositionSpread, currentSpawnPositions[randomSpawn].transform.position.z + spawnPositionSpread);
                    Vector3 newSpawnPos = new Vector3(spawnPos.x, spawnPos.y, randomZ);

                    //instantiate an appropriate enemy there
                    Instantiate(enemies[i], newSpawnPos, enemies[i].transform.rotation);
                }
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

        if (waveCounter == 3)
        {
            currentSpawnPositions = spawnPositionsWave3;
            Destroy(unavailableLanes[1]);
            Destroy(unavailableLanes[2]);
        }

        if (waveCounter == 6)
        {
            currentSpawnPositions = spawnPositionsWave6;
            Destroy(unavailableLanes[0]);
            Destroy(unavailableLanes[3]);
        }

        //increase wave strength
        currentWaveStrength += waveStrengthIncrease;
        waveCounter++;
    }
}
