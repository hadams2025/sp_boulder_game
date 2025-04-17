using UnityEngine;
using UnityEngine.UI;

//Script that handles the majority of the player's movement and attributes (movespeed, health, stamina, etc...)
public class playerMovement : MonoBehaviour
{
    // camera
    public GameObject cameraTurn;
    // velocity
    public float baseMoveSpeed = 5;
    public float currentMoveSpeed;
    // jumping
    public Rigidbody jumpObject;
    public float jumpHeight;
    private bool canJump;
    //boulderlock
    public GameObject boulder;
    private BoulderBehavior boulderScript;
    //gameOver logic
    public logicScript logic;
    public GameObject enemyLookAt;
    //health
    public bool isAlive;
    public float maxHealth, currentHealth, bonusHealth;
    //stamina
    public float maxStamina, currentStamina, bonusStamina;
    public Image healthBar, staminaBar;
    //Shop logic
    private ShopScript shopScript;
    private menuScript MenuScript;
    public PlayerAbilities playerAbilitiesScript;

    //On game start, set a bunch of the default values and set objects to variables
    void Start()
    {
        currentMoveSpeed = baseMoveSpeed;
        isAlive = true;
        maxHealth = 5 + bonusHealth;
        currentHealth = maxHealth;
        maxStamina = 5 + bonusStamina;
        currentStamina = maxStamina;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
        shopScript = GameObject.FindGameObjectWithTag("shop").GetComponent<ShopScript>();
        boulderScript = boulder.GetComponent<BoulderBehavior>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MenuScript = GameObject.FindGameObjectWithTag("menu").GetComponent<menuScript>();
        playerAbilitiesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
    }


    void Update()
    {
        //WASD movement
        if (isAlive && !shopScript.isShopping && !MenuScript.menuActive)
        {
            //Vertical axis is WS or up and down arrows
            //Horzontal axis is AD or left and right arrows
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            //If there is directional input
            if (verticalInput != 0 || horizontalInput != 0)
            {
                //move the player according to that input * movespeed * direction
                transform.Translate(currentMoveSpeed * verticalInput * Time.deltaTime * Vector3.forward);
                transform.Translate(currentMoveSpeed * horizontalInput * Time.deltaTime * Vector3.right);
            }

            //if spacebar is pressed make the player jump on a cooldown
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                jumpObject.velocity = Vector3.up * jumpHeight;
                transform.Translate(Time.deltaTime * jumpHeight * Vector3.up);
                canJump = false;
            }

            //Increase player movespeed if shift is held
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentMoveSpeed *= 1.5f;
            }

            //Drain stamina if the player is holding shift
            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
            {
                currentStamina -= 1 * Time.deltaTime;
                staminaBar.fillAmount = currentStamina / maxStamina;
            }
            else
            {
                currentMoveSpeed = baseMoveSpeed;
            }
            // if the player is not full on stamina, is not holding shift, and is not holding left click or has the magnet
            if (currentStamina < maxStamina && !Input.GetKey(KeyCode.LeftShift) && (!Input.GetMouseButton(0) || !playerAbilitiesScript.hasMagnet))
            {
                //set movespeed to default and gradually refill the stamina bar
                currentMoveSpeed = baseMoveSpeed;
                currentStamina += 1 * Time.deltaTime;
                staminaBar.fillAmount = currentStamina / maxStamina;
            }
        }
        //If player is shopping then set currentMS to baseMS
        if (shopScript.isShopping)
        {
            currentMoveSpeed = baseMoveSpeed;
        }
        //If currentHealth > MaxHealth, then set current to be max
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        //If currentStamina > maxStamina, then set current to be max
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        //If the player health goes to 0 or less, set isAlive to false, set the health to -999, no longer make the enemies target the player, despawn the player, and call gameOver()
        if (currentHealth <= 0)
        {
            isAlive = false;
            currentHealth = -999;
            enemyLookAt.transform.position = new Vector3(0, 0, 0);
            gameObject.SetActive(false);
            logic.gameOver();
        }
    }

    // jump 'cooldown'
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            canJump = true;
        }
    }
}

