using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script for pause menu and other UI functionality
public class menuScript : MonoBehaviour
{
    public GameObject MenuUI;
    public bool menuActive;
    private camScript cameraScript;
    public playerMovement playerMovementScript;
    public GameObject extraStats;
    public bool extraStatsActive;
    private ShopScript shopScript;
    public UnityEngine.UI.Slider sensSlider;
    public TMP_Text actualSens;

    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camScript>();
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        shopScript = GameObject.FindGameObjectWithTag("shop").GetComponent<ShopScript>();
        sensSlider.value = 200;
    }

    void Update()
    {
        //If the escape key is pressed and the pause menu is not active and the player is alive and not shopping
        if (Input.GetKeyDown(KeyCode.Escape) && !menuActive && playerMovementScript.isAlive.Equals(true) && !shopScript.isShopping)
        {
            //set the pause menu to active, show the cursor, turn off moving the camera, pause the game, and pause audio
            MenuUI.SetActive(true);
            menuActive = true;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            cameraScript.enabled = false;
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        //If the escape key is pressed while the menu is active
        else if (Input.GetKeyDown(KeyCode.Escape) && menuActive)
        {
            //Undo all the things pressing the escape button does initially
            MenuUI.SetActive(false);
            menuActive = false;
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            cameraScript.enabled = true;
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
        //If tab is pressed and the extraStats is not visible
        if (Input.GetKeyDown(KeyCode.Tab) && !extraStatsActive)
        {
            //Make extraStats visible
            extraStats.SetActive(true);
            extraStatsActive = true;
        }
        //If tab is pressed and the extraStats is visible
        else if (Input.GetKeyDown(KeyCode.Tab) && extraStatsActive)
        {
            //Make extraStats not visible
            extraStats.SetActive(false);
            extraStatsActive = false;
        }
        //Sensitivity slider functionality
        actualSens.text = sensSlider.value.ToString();
        cameraScript.turnSpeed = sensSlider.value;
    }
    //Main Menu button loads the menu scene when clicked
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
