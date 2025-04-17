using UnityEngine;

//Script attatched to spawnObjects that spawns a raycast shooting in the -Y to determine the object's final position
public class spawnRaycast : MonoBehaviour
{
    public float heightOffset;
    public LayerMask layerMask;

    void Start()
    {
        //Spawn a raycast that shoots out on the -Y axis that returns "spawnLocation" vector3 coordinates
        Physics.Raycast(gameObject.transform.position, Vector3.down, out RaycastHit spawnLocation, Mathf.Infinity, ~layerMask);
        //Move the gameobject to the X, Y, and Z position where the raycast hit
        gameObject.transform.position = new Vector3(spawnLocation.point.x, spawnLocation.point.y + heightOffset, spawnLocation.point.z);
        //Randomly rotate the object on the Y axis
        transform.Rotate(0f, Random.Range(0f, 360f), 0f);
    }
}
