using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void Quit()
    {
        Application.Quit();
    }
}
