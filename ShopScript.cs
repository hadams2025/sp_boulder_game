using UnityEngine;

//Script attatched to shopTrigger object that handles most of the shop function and logic
public class ShopScript : MonoBehaviour
{
    public GameObject shopButton;
    public GameObject shopPage;
    private playerMovement playerScript;
    public bool isShopping;
    public logicScript logic;
    public int item1Cost, item2Cost, item3Cost, item4Cost, item5Cost, item6Cost;
    private PlayerAbilities playerAbilitiesScript;
    private camScript cameraScript;

    void Start()
    {
        //Assign proper objects to variables
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        playerAbilitiesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camScript>();
    }

    //While the player is in the shop area, make the shop button visible and clickable with the cursor
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopButton.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //When the player exists the shop area, make the shop button invisible and hide the cursor
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopButton.SetActive(false);
            shopPage.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //when the shop button is clicked
    public void OpenShopPage()
    {
        shopButton.SetActive(false);
        shopPage.SetActive(true);
        isShopping = true;
        cameraScript.enabled = false;
    }

    //when the exit shop button is clicked
    public void ExitShopPage()
    {
        shopButton.SetActive(true);
        shopPage.SetActive(false);
        isShopping = false;
        cameraScript.enabled = true;
    }

    //The next functions are for when each different shop button is clicked

    //+0.1 MS, -10 gold
    public void BuyItem1()
    {
        //Can not buy MS past 7.5
        if (playerScript.baseMoveSpeed < 7.4f)
            //allows purchase if the player has coins >= the item cost
            if (logic.removeCoins(item1Cost))
            {
                //applies the appropriate upgrade
                playerScript.baseMoveSpeed += 0.1f;
            }
    }

    //The rest of the shop functions follow a similar formula to the first one
    // +0.2m jump height, -10 gold
    public void BuyItem2()
    {
        if (playerScript.jumpHeight < 8.8)
        {
            if (logic.removeCoins(item2Cost))
            {
                playerScript.jumpHeight += 0.2f;
            }
        }
    }

    // magnet ability 
    public void BuyItem3()
    {
        if (!playerAbilitiesScript.hasMagnet)
        {
            if (logic.removeSouls(item3Cost))
            {
                playerAbilitiesScript.hasMagnet = true;
            }
        }
    }

    // -1 max health
    public void BuyItem4()
    {
        if (playerScript.maxHealth != 1)
        {
            if (logic.removeCoins(item4Cost))
            {
                playerScript.maxHealth -= 1;
            }
        }
    }

    // -0.1 MS, +10 gold
    public void BuyItem5()
    {
        if (playerScript.baseMoveSpeed > 2.6f)
        {
            if (logic.removeCoins(item5Cost))
            {
                playerScript.baseMoveSpeed -= 0.1f;
            }
        }
    }

    // //-0.2m jump height, +10 gold
    public void BuyItem6()
    {
        if (playerScript.jumpHeight > 3.2)
        {
            if (logic.removeCoins(item6Cost))
            {
                playerScript.jumpHeight -= 0.2f;
            }
        }
    }

}
