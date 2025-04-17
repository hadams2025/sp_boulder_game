using UnityEngine;

//Script applied to boulder object that handles some of its functionality
public class BoulderBehavior : MonoBehaviour
{
    public Transform boulder;
    public Transform player;
    public bool boulderLock;
    private Rigidbody boulderRb;
    public GameObject logicManger;
    private logicScript logic;
    public AudioSource boulderSound;
    public GameObject boulderNoise;

    void Start()
    {
        logic = logicManger.GetComponent<logicScript>();
        boulderRb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if the boulder is moving > 2m/s, enable the bounder rolling sound
        if (boulderRb.velocity.magnitude > 2)
        {
            boulderSound.enabled = true;
        }
        //else disable the sound
        else
        {
            boulderSound.enabled = false;
        }
        //make the noise "3D" by setting the noise object's position to be the same as the boulder's position
        boulderNoise.transform.position = boulderRb.position;
    }
    //If the boulder collides with a game object having the tag "goal", then call gameWin()
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("goal"))
        {
            logic.gameWin();
        }
    }

}
