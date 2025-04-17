using UnityEngine;

//Script attatched to castle doors
public class doorDestruction : MonoBehaviour
{
    public AudioSource doorBreaking;
    private void OnCollisionEnter(Collision other)
    {
        //If the boulder collides with the door
        if (other.gameObject.name == "boulder")
        {
            //Then deactivate the door (destroy it) and play sound
            gameObject.SetActive(false);
            doorBreaking.Play();
        }
    }
}
