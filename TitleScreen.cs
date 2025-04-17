using UnityEngine;
using UnityEngine.SceneManagement;

//gives title screen UI buttons functionality
public class TitleScreen : MonoBehaviour
{
    //when play button is clicked, load the game scene
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    //when the quit button is clicked, quit the application
    public void Quit()
    {
        Application.Quit();
    }
}
