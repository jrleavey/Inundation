using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    //public GameObject mainFirstButton; //what is this for?

    public GameObject optionsFirstButton;
    public GameObject optionsClosedButton;

    public GameObject creditsFirstButton;
    public GameObject creditsClosedButton;

    public GameObject optionsMenuSlide;
    public GameObject creditsMenuSlide;


    public void Play() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void Quit()=>Application.Quit();

    public void OpenOptionsMenu()
    {
        optionsMenuSlide.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    public void CloseOptionsMenu()
    {
        optionsMenuSlide.SetActive(false);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);

    }
    public void CreditsMenu()
    {
        creditsMenuSlide.SetActive(true);
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
    }
    public void CloseCreditsMenu()
    {
        creditsMenuSlide.SetActive(false);
        EventSystem.current.SetSelectedGameObject(creditsClosedButton);
    }
}
