using UnityEngine;

//Script appiled to any object considered an enemy
public class enemyScript : MonoBehaviour
{
    public AudioSource squashNoise;
    public GameObject targetObject;
    public float enemyMoveSpeed;
    public float detectDistance;
    public logicScript logic;
    public int numSoulsOnDefeat = 1;
    public float enemyDamage = 1;
    private playerMovement playerMovementScript;
    public GameObject player;
    public MeshCollider enemyCollider;
    public CapsuleCollider playerLeftArm;
    public CapsuleCollider playerRightArm;
    public AudioSource monsterDeath;
    public AudioSource enemyDetect;
    public float randomPitch;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
        squashNoise = GameObject.FindGameObjectWithTag("squashNoise").GetComponent<AudioSource>();
        targetObject = GameObject.FindGameObjectWithTag("lookAt");
        player = GameObject.FindGameObjectWithTag("Player");
        playerLeftArm = GameObject.FindGameObjectWithTag("leftArm").GetComponent<CapsuleCollider>();
        playerRightArm = GameObject.FindGameObjectWithTag("rightArm").GetComponent<CapsuleCollider>();
        monsterDeath = GameObject.FindGameObjectWithTag("monsterDeath").GetComponent<AudioSource>();
        playerMovementScript = player.GetComponent<playerMovement>();
        enemyCollider = gameObject.GetComponent<MeshCollider>();
        //make enemies ignore the collision box of the player's arms
        Physics.IgnoreCollision(enemyCollider, playerRightArm);
        Physics.IgnoreCollision(enemyCollider, playerLeftArm);
        //set each enemies' sound to be a random pitch within a range
        randomPitch = Random.Range(0.5f, 1.5f);
        enemyDetect.pitch = randomPitch;
    }

    void Update()
    {
        //If the player gets within distance <= the enemies' detection distance
        if (Vector3.Distance(transform.position, targetObject.transform.position) <= detectDistance)
        {
            //set targetPos to be in the direction of the player
            Vector3 targetPos = new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z);
            //have the enemy look in the player's direction
            transform.LookAt(targetPos);
            //make the enemy move towards the player
            transform.position += enemyMoveSpeed * Time.deltaTime * transform.forward;
            //enable the enemy's detection noise
            enemyDetect.enabled = true;
        }
        //if the enemy loses detection, set the detection noise to false
        else
        {
            enemyDetect.enabled = false;
        }
    }
    //if the boulder collides with the enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Boulder"))
        {
            //destroy the enemy, play the squash noise, set the death noise to be a random pitch, and play the death noise
            Destroy(gameObject);
            squashNoise.Play();
            monsterDeath.pitch = randomPitch;
            monsterDeath.Play();
        }
    }

    //if the enemy collides with the player
    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            //the player's health should decrease over time according to the enemy's damage value
            playerMovementScript.currentHealth -= enemyDamage * Time.deltaTime;
            //the player's health bar should be synced visually
            playerMovementScript.healthBar.fillAmount = playerMovementScript.currentHealth / playerMovementScript.maxHealth;
        }
    }

    //adds the correct number souls to the player's total souls upon an enemy being defeated
    private void OnDestroy()
    {
        logic.addSouls(numSoulsOnDefeat);
    }
}
