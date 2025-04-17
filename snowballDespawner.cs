using UnityEngine;

//Script attatched to an invisible barrier to handle despawning snowballs
public class snowballDespawner : MonoBehaviour
{

    //When something enters its trigger area
    private void OnTriggerEnter(Collider other)
    {
        //If the object's tag is "snowball"
        if (other.gameObject.tag == "snowball")
        {
            //Despawn the object
            other.gameObject.SetActive(false);
        }
    }
}
