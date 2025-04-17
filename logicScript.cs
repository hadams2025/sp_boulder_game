using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//"Catch All" script that handles all background logic that is not specific to a single game object
public class logicScript : MonoBehaviour
{
    //UI
    public int coinBank;
    public int soulBank;
    public TMP_Text coinVal;
    public TMP_Text soulVal;
    public GameObject player;
    private playerMovement playerScript;
    public TMP_Text msVal;
    public TMP_Text hpVal;
    public TMP_Text stamVal;
    public GameObject inGameUI;
    public TMP_Text jumpVal;
    //Game over
    public GameObject gameOverScreen;
    //Game win
    public GameObject gameWinScreen;
    public AudioSource winSound;
    public AudioSource kingDeath;
    //Game lose
    public AudioSource loseSound;
    public GameObject sounds;
    private camScript cameraScript;

    void Start()
    {
        //Setting some variables, initial values, and the target framerate
        playerScript = player.GetComponent<playerMovement>();
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camScript>();
        Application.targetFrameRate = 144;
        coinBank = 0;
        coinVal.text = coinBank.ToString();
        soulBank = 0;
        soulVal.text = soulBank.ToString();
    }
    void Update()
    {
        //Setting the UI text to the variable value
        msVal.text = playerScript.currentMoveSpeed.ToString("N2");
        hpVal.text = playerScript.currentHealth.ToString("N2") + " / " + playerScript.maxHealth.ToString("N2");
        stamVal.text = playerScript.currentStamina.ToString("N2") + " / " + playerScript.maxStamina.ToString("N2");
        jumpVal.text = playerScript.jumpHeight.ToString("N2");
    }
    //adds coins to the player's total coins based on the coin's pickup value
    public void addCoins(int numCoinsOnPickup)
    {
        coinBank += numCoinsOnPickup;
        coinVal.text = coinBank.ToString();
    }
    //removes coins from the player's total coins based on the price of the item attempting to be purchased
    public bool removeCoins(int itemCost)
    {
        if (coinBank >= itemCost)
        {
            coinBank -= itemCost;
            coinVal.text = coinBank.ToString();
            return true;
        }
        else
        {
            Debug.Log("Not enough Gold to purchase");
            return false;
        }
    }
    //adds souls to the player's total souls based on the # of souls the enemy drops upon defeat
    public void addSouls(int numSoulsOnDefeat)
    {
        soulBank += numSoulsOnDefeat;
        soulVal.text = soulBank.ToString();
    }
    //removes souls from the player's total souls based on the soul cost of the ability attempting to be purchased
    public bool removeSouls(int itemCost)
    {
        if (soulBank >= itemCost)
        {
            soulBank -= itemCost;
            soulVal.text = soulBank.ToString();
            return true;
        }
        else
        {
            Debug.Log("Not enough Souls to purchase");
            return false;
        }
    }
    //game restart button goes to the title screen
    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }
    //when the player loses
    public void gameOver()
    {
        //activate the game over screen, turn off some UI, make the cursor visible, disable sounds, play a defeat sound, and keep the camera from moving
        gameOverScreen.SetActive(true);
        inGameUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        sounds.SetActive(false);
        loseSound.enabled = true;
        cameraScript.enabled = false;
    }
    //when the player wins
    public void gameWin()
    {
        //activate the game win screen, set the player to be dead, make the cursor visible, turn off some sounds, play the win and boss death sound, and keep the camera from moving
        gameWinScreen.SetActive(true);
        playerScript.isAlive = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        sounds.SetActive(false);
        winSound.enabled = true;
        kingDeath.enabled = true;
        cameraScript.enabled = false;
    }
}
