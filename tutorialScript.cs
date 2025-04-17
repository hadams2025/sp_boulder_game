using UnityEngine;

//Script attatched to tutorial UI
public class tutorialScript : MonoBehaviour
{
    public GameObject[] hints;
    public int index = 0;
    public GameObject tutorialUI;
    private bool hintsActive = true;

    void Update()
    {
        //If the player presses Enter key and is not at the 8th hint
        if (Input.GetKeyDown(KeyCode.Return) && index != 8)
        {
            //change the UI to show the next hint
            hints[index].SetActive(false);
            hints[index + 1].SetActive(true);
            index++;
            Debug.Log(index);
        }
        //if the player presses Enter key and is on the 8th hint
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //turn off all the tutorial UI
            tutorialUI.SetActive(false);
        }

        //if the player presses the alt key and and are before the 8th hint
        if ((Input.GetKeyDown(KeyCode.RightAlt) || Input.GetKeyDown(KeyCode.LeftAlt)) && index <= 8)
        {
            //toggle the visibility of the hints
            if (hintsActive)
            {
                tutorialUI.SetActive(false);
                hintsActive = false;
            }
            else
            {
                tutorialUI.SetActive(true);
                hintsActive = true;
            }
        }
    }
}
