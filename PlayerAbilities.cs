using UnityEngine;

//Script behind how the player abilities work
public class PlayerAbilities : MonoBehaviour
{
    public GameObject boulder;
    public Rigidbody boulderRb;
    public float magnetForce;
    public GameObject boulderTarget;
    public bool hasMagnet;
    public bool hasWings;
    public float wingsForce;
    public playerMovement playerMovementScript;
    public Rigidbody playerRb;

    void Start()
    {
        boulder = GameObject.FindGameObjectWithTag("Boulder");
        boulderRb = boulder.GetComponent<Rigidbody>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        //If left click is held
        if (Input.GetMouseButton(0))
        {
            //if the player has magnet and has stamina > 0
            if (hasMagnet && playerMovementScript.currentStamina > 0)
            {
                //call MagnetAbility()
                MagnetAbility();
            }
            //if the player hasWings and has stamina > 0
            if (hasWings && playerMovementScript.currentStamina > 0)
            {
                //call WingsAbility()
                WingsAbility();
            }
        }
    }

    //MagnetAbility code
    public void MagnetAbility()
    {
        //Get the vector3 the is the direction pointing from the boulder towards the player
        Vector3 forceDir = boulderTarget.transform.position - boulder.transform.position;
        //Add force to the boulder in the direction of that vector3
        boulderRb.AddForce(forceDir * magnetForce * Time.deltaTime, ForceMode.Acceleration);
        //Decrease the player's stamina
        playerMovementScript.currentStamina -= 1 * Time.deltaTime;
        //Make the stamina bar update appropriately
        playerMovementScript.staminaBar.fillAmount = playerMovementScript.currentStamina / playerMovementScript.maxStamina;
    }
    //WingsAbility code
    public void WingsAbility()
    {
        //add upward force to the player
        playerRb.AddForce(Vector3.up * wingsForce * Time.deltaTime, ForceMode.Acceleration);
        //decrease the player's stamina
        playerMovementScript.currentStamina -= 1 * Time.deltaTime;
        //update the stamina bar appropriately
        playerMovementScript.staminaBar.fillAmount = playerMovementScript.currentStamina / playerMovementScript.maxStamina;
    }
}
