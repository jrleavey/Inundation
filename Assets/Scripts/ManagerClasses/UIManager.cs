using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool isTheGamePaused = false;
    public GameObject _pausemenu;

    public GameObject _mainFirstButton;
    public GameObject _optionsFirstButton;
    public GameObject _optionsClosedButton;
    public GameObject _creditsFirstButton;
    public GameObject _creditsClosedButton;

    public GameObject _optionsMenuSlide;
    public GameObject _creditsMenuSlide;

    [SerializeField]
    private Slider VolumeSlider;
    [SerializeField]
    private Slider BrightnessSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;

    public GameObject _bloodImage;
    public Color _bloodAlpha;

    private GameObject _player;

    [SerializeField]
    private GameObject handgunUI;
    [SerializeField]
    private GameObject ShotgunUI;
    [SerializeField]
    private GameObject RifleUI;
    [SerializeField]
    private Text handgunUIText;
    [SerializeField]
    private Text ShotgunUIText;
    [SerializeField]
    private Text RifleUIText;

    [SerializeField]
    private Text[] _ammoAmounts;
    [SerializeField]
    private GameObject[] _healthStates;

    [SerializeField]
    private Text _pickupItemText;

    [SerializeField]
    private GameObject _deathScreen;

    private void Start()
    {
        Instance = this;
        Time.timeScale = 1;
        isTheGamePaused = false;
        _player = GameObject.Find("Player");
    }

    public void PauseMenu()
    {
        isTheGamePaused = !isTheGamePaused;


        if (isTheGamePaused == true)
        {
            Time.timeScale = 0;
            _pausemenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_mainFirstButton);
        }
        else
        {
            Time.timeScale = 1;
            _pausemenu.SetActive(false);
        }
    }
    public void Resume()
    {
        PauseMenu();
        _player.GetComponent<PlayerController>().UnpauseConsistency();
    }
    private void Update()
    {
        
    }
    public void AdjustVolume(float value)
    {
        AudioListener.volume = VolumeSlider.value;
        Save();
    }
    public void AdjustBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.5f;
        }
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", VolumeSlider.value);

    }
    public void Load()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }
    public void ChangeScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void OptionsMenu()
    {
        _optionsMenuSlide.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_optionsFirstButton);
    }
    public void CloseOptionsMenu()
    {
        _optionsMenuSlide.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_optionsClosedButton);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RetryGame()
    {
        // SceneManager.LoadScene
        //Load Restart the game
    }

    public void BloodImageVisible()
    {
        float height = Screen.height * .75f;
        float width = Screen.width * .75f;

        float randomHeight = Random.Range(height * -1, height);
        float randomWidth = Random.Range(width * -1, width);

        GameObject image = Instantiate(_bloodImage, Vector3.zero, Quaternion.identity, this.transform);

        image.transform.localPosition = new Vector3(randomWidth, randomHeight, 0);
    }

    public void UseRevolver()
    {
        _player.GetComponent<PlayerController>().SwapToRevolver();
        ShotgunUI.SetActive(false);
        RifleUI.SetActive(false);
        handgunUI.SetActive(true);

        AudioManager.Instance.Play("Equip");
        PauseMenu();
        _player.GetComponent<PlayerController>().UnpauseConsistency();

    }
    public void UseShotgun()
    {
        _player.GetComponent<PlayerController>().SwapToShotgun();
        handgunUI.SetActive(false);
        RifleUI.SetActive(false);
        ShotgunUI.SetActive(true);

        AudioManager.Instance.Play("Equip");
        PauseMenu();
        _player.GetComponent<PlayerController>().UnpauseConsistency();

    }
    public void UseRifle()
    {
        _player.GetComponent<PlayerController>().SwapToRifle();
        handgunUI.SetActive(false);
        ShotgunUI.SetActive(false);
        RifleUI.SetActive(true);

        AudioManager.Instance.Play("Equip");

        PauseMenu();
        _player.GetComponent<PlayerController>().UnpauseConsistency();

    }
    public void UseHealthkit()
    {
        _player.GetComponent<PlayerController>().Heal();
        PauseMenu();
        _player.GetComponent<PlayerController>().UnpauseConsistency();

    }
    public void UpdateRevolverAmmo(int currentammo)
    {
        handgunUIText.text = "" + currentammo;
    }
    public void UpdateShotgunAmmo(int currentammo)
    {
        ShotgunUIText.text = "" + currentammo;
    }
    public void UpdateRifleAmmo(int currentammo)
    {
        RifleUIText.text = "" + currentammo;
    }
    public void UpdateAmmoReserves(int handgunAmmo, int Shotgunammo, int rifleammo, int healthkits)
    {
        _ammoAmounts[0].text = "" + handgunAmmo;
        _ammoAmounts[1].text = "" + Shotgunammo;
        _ammoAmounts[2].text = "" + rifleammo;
        _ammoAmounts[3].text = "" + healthkits;

    }
    public void UpdateHealth(int currenthealth)
    {
        switch(currenthealth)
        {
            case 1:
                _healthStates[0].SetActive(true);
                _healthStates[1].SetActive(false);
                _healthStates[2].SetActive(false);
                _healthStates[3].SetActive(false);
                break;
            case 2:
                _healthStates[0].SetActive(false);
                _healthStates[1].SetActive(true);
                _healthStates[2].SetActive(false);
                _healthStates[3].SetActive(false);
                break;
            case 3:
                _healthStates[0].SetActive(false);
                _healthStates[1].SetActive(false);
                _healthStates[2].SetActive(true);
                _healthStates[3].SetActive(false);
                break;
            case 4:
                _healthStates[0].SetActive(false);
                _healthStates[1].SetActive(false);
                _healthStates[2].SetActive(false);
                _healthStates[3].SetActive(true);
                break;
        }
    }
    public void Die()
    {
        GameObject blood = GameObject.FindGameObjectWithTag("Blood");
        Destroy(blood.gameObject);
        _deathScreen.SetActive(true);
        Time.timeScale = 0;
        isTheGamePaused = true;

    }
    public void PickupItemPrompt(int itemID)
    {
        switch (itemID)
        {
            case 0:
                _pickupItemText.text = "Take the Revolver Ammo?";
                break;
            case 1:
                _pickupItemText.text = "Take the Shotgun Ammo?";
                break;
            case 2:
                _pickupItemText.text = "Take the SMG Ammo?";
                break;
            case 3:
                _pickupItemText.text = "Take the Rifle Ammo?";
                break;
            case 4:
                _pickupItemText.text = "Take the Health Kit?";
                break;
        }
    }
    public void ClearPickupPrompt()
    {
        _pickupItemText.text = "";
    }
}
