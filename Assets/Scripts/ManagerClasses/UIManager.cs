using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool isTheGamePaused = false;
    public GameObject _pausemenu;
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
    }

    public void PauseMenu()
    {
        isTheGamePaused = !isTheGamePaused;
    }
    private void Update()
    {
        if (isTheGamePaused == true)
        {
            Time.timeScale = 0;
            _pausemenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _pausemenu.SetActive(false);

        }
    }
}
