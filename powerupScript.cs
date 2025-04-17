using UnityEngine;

//Script applied to coin/pickup objects
public class powerupScript : MonoBehaviour
{
    public float rotationSpeed;
    public float bobHeight;
    public float bobSpeed;
    public GameObject obtainEffect;
    public AudioSource collectSound;
    private float startHeight;
    public logicScript logic;
    public int numCoinsOnPickup = 1;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
        collectSound = GameObject.FindGameObjectWithTag("collectSound").GetComponent<AudioSource>();
    }

    //On every frame, rotate the object on its Y axis 
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    //When an object enters the coin's trigger area
    private void OnTriggerEnter(Collider other)
    {
        //if the object is the player
        if (other.CompareTag("Player"))
        {
            //destroy the coin object, instantiate a particle effect, play a sound, and add the coin value to the player's total coins
            Destroy(gameObject);
            Instantiate(obtainEffect, transform.position, transform.rotation);
            collectSound.Play();
            logic.addCoins(numCoinsOnPickup);
        }
    }
}
