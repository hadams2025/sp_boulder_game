using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for spawning snowballs in the third area
public class SnowballSpawner : MonoBehaviour
{
    public List<GameObject> spawnObjects;
    public GameObject spawnTrigger;
    private Vector3 spawnPos;
    public float zMin, zMax;
    public float xMin, xMax;
    public float spawnRate = 1;
    public Boolean isGameActive = true;

    //Begin coroutine
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    //Coroutine runs every "spawnRate" seconds and spawns a snowball at a position determined by RandomSpawnPos() 
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(spawnObjects[0], RandomSpawnPos(), transform.rotation);
        }

    }

    //Spawns the snowball randomly within the boudns of xMin, xMax, zMin, and zMax
    Vector3 RandomSpawnPos()
    {
        return spawnPos = new Vector3(UnityEngine.Random.Range(xMin, xMax), gameObject.transform.position.y, UnityEngine.Random.Range(zMin, zMax));
    }
}
