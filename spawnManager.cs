using System.Collections.Generic;
using UnityEngine;

//Script attatched to empty game object that is responsible for generating objects
public class spawnManager : MonoBehaviour
{
    public List<GameObject> spawnObjects;
    private Vector3 spawnPos;
    public int obstacleCount;
    public int entityCount;
    public int pickupCount;
    public float zMin, zMax;
    public float xMin, xMax;
    private int counter1, counter2, counter3 = 0;

    void Start()
    {
        // obstacleCount can be changed in Unity to allow for more obstacles to spawn
        while (counter1 != obstacleCount)
        {
            //spawn the obstacles objects and increase counter
            Instantiate(spawnObjects[Random.Range(0, spawnObjects.Count - 2)], RandomSpawnPos(), transform.rotation);
            counter1++;
        }
        // enittyCount can be changed in Unity to allow for more obstacles to spawn
        while (counter2 != entityCount)
        {
            //spawn the entity objects and increase counter
            Instantiate(spawnObjects[spawnObjects.Count - 2], RandomSpawnPos(), transform.rotation);
            counter2++;
        }
        // pickupCount can be changed in Unity to allow for more obstacles to spawn
        while (counter3 != pickupCount)
        {
            //spawn the pickup objects and increase counter
            Instantiate(spawnObjects[spawnObjects.Count - 1], RandomSpawnPos(), transform.rotation);
            counter3++;
        }
    }

    //Returns a random Vector3 coordinate within a boundary set by xMin, xMax, zMin, and zMax
    Vector3 RandomSpawnPos()
    {
        return spawnPos = new Vector3(Random.Range(xMin, xMax), gameObject.transform.position.y, Random.Range(zMin, zMax));
    }
}
