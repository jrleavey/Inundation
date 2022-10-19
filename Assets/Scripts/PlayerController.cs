using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum ActiveWeapon
    {
        Handgun,
        Shotgun,
        SMG,
        Rifle
    }
    [SerializeField]
    private ActiveWeapon _ActiveWeapon;

    private PlayerControls _playerControls;


    private Vector3 playerVelocity;

    [SerializeField]
    private int handgunAmmo;
    [SerializeField]
    private int shotgunAmmo;
    [SerializeField]
    private int smgAmmo;
    [SerializeField]
    private int rifleAmmo;

    private int magazineCapacity = 6;
    [SerializeField]
    private int currentAmmo = 6;

    private int _currentHealth;


    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotateSpeed;
    private float _eyeRot;
    [SerializeField]
    private float fireRate = 5f;
    [SerializeField]
    private float nextFire = -1f;
    private float gunDamage = 1;


    [SerializeField]
    private bool isAiming;
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool isReloading;
    [SerializeField]
    private bool aButton;


    [SerializeField]
    private GameObject _playerEyes;
    [SerializeField]
    private GameObject[] RaycastHolders;
    [SerializeField]
    private GameObject[] _Weapons;

    [SerializeField]
    private Animator _revolverAnim;

    



    private void Awake()
    {
        
    }
    void Start()
    {
        _currentHealth = 3;
        _playerControls = new PlayerControls();
        _playerControls.Controller.Enable();
        InputSetup();
    }

    void Update()
    {
        MovementController();
        WeaponControls();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void MovementController()
    {
        Vector2 leftStick = _playerControls.Controller.LeftStickMovement.ReadValue<Vector2>();
        Vector2 rightStick = _playerControls.Controller.RightStickLook.ReadValue<Vector2>();

        float turnspeed = rightStick.x * _rotateSpeed * Time.deltaTime;
        float updownspeed = -rightStick.y * _rotateSpeed * Time.deltaTime;

        transform.Rotate(0, turnspeed, 0);

        _eyeRot += updownspeed;

        _eyeRot = Mathf.Clamp(_eyeRot, -45f, 45f);
        _playerEyes.transform.localRotation = Quaternion.Euler(_eyeRot, 0f, 0f);
        


        transform.Translate(leftStick.x * Time.deltaTime * _speed, 0, leftStick.y * Time.deltaTime * _speed);


    }
    private void WeaponControls()
    {
        switch (_ActiveWeapon)
        {
            case ActiveWeapon.Handgun: // Pistol
                PistolLogic();
                break;
            case ActiveWeapon.Shotgun: // Shotgun
                ShotgunLogic();
                break;
            case ActiveWeapon.SMG: // SMG
                SMGLogic();
                break;
            case ActiveWeapon.Rifle: // Rifle
                RifleLogic();
                break;
        }
    }
    
    public void PickedUpItem(int _itemID)
    {
        switch (_itemID)
        {
            case 0: // Handgun Bullets
                handgunAmmo += 10;
                Debug.Log("+10 Handgun Ammo");
                break;
            case 1: // Shotgun Bullets
                shotgunAmmo += 5;
                Debug.Log("+5 Shotgun Ammo");
                break;
            case 2: // SMG Bullets
                smgAmmo += 50;
                Debug.Log("+50 SMG Ammo");
                break;
            case 3: // Rifle Bullets
                rifleAmmo += 4;
                Debug.Log("+3 Rifle Ammo");
                break;
            case 4: // Health
                Debug.Log("This is health");
                break;
        }
    }

    private void PistolLogic()
    {
        
        magazineCapacity = 6;

        fireRate = 1.3f;
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire && isReloading == false)
        {
            
            _revolverAnim.SetTrigger("Shoot");
            StartCoroutine(FireAnimTimers());
            aButton = false;
            RaycastHit hit;
            nextFire = Time.time + fireRate;

            if (Physics.Raycast(RaycastHolders[0].transform.position, RaycastHolders[0].transform.forward, out hit, Mathf.Infinity))
            {
                hit.collider.SendMessage("Damage", gunDamage);
                Debug.DrawLine(RaycastHolders[0].transform.position, hit.point, Color.red, 1f);
                Debug.Log("Fired Raycast");
                currentAmmo--;

            }
        }
    }
    private void ShotgunLogic()
    {
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire)
        {
            aButton = false;
            nextFire = Time.time + fireRate;
            int amountOfProjectiles = 8;
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                ShotgunRay();
            }
        }
    }
    private void ShotgunRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(RaycastHolders[1].transform.position, RaycastHolders[1].transform.forward, out hit, Mathf.Infinity))
        {
            Vector3 direction = RaycastHolders[1].transform.forward;
            Vector3 spread = Vector3.zero;
            spread += RaycastHolders[1].transform.up * Random.Range(-.05f, .05f);
            spread += RaycastHolders[1].transform.right * Random.Range(-.05f, .05f);
            direction += spread.normalized * Random.Range(0f, 0.2f);
            hit.collider.SendMessage("Damage", gunDamage);

            if (Physics.Raycast(RaycastHolders[1].transform.position, direction, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(RaycastHolders[1].transform.position, hit.point, Color.red, 1f);
            }
        }
    }
    private void SMGLogic()
    {
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            int amountOfProjectiles = 1;
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                SMGSpread();
            }
        }
    }
    private void SMGSpread()
    {
        RaycastHit hit;
        if (Physics.Raycast(RaycastHolders[2].transform.position, RaycastHolders[1].transform.forward, out hit, Mathf.Infinity))
        {
            Vector3 direction = RaycastHolders[2].transform.forward;
            Vector3 spread = Vector3.zero;
            spread += RaycastHolders[1].transform.up * Random.Range(-.01f, .01f);
            spread += RaycastHolders[1].transform.right * Random.Range(-.01f, .01f);
            direction += spread.normalized * Random.Range(0f, 0.1f);
            hit.collider.SendMessage("Damage", gunDamage);

            if (Physics.Raycast(RaycastHolders[2].transform.position, direction, out hit, Mathf.Infinity))
            {
                currentAmmo--;
                Debug.DrawLine(RaycastHolders[2].transform.position, hit.point, Color.red, 1f);
            }
        }
    }

    private void RifleLogic()
    {
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire)
        {
            aButton = false;
            RaycastHit hit;
            nextFire = Time.time + fireRate;

            if (Physics.Raycast(RaycastHolders[0].transform.position, RaycastHolders[0].transform.forward, out hit, Mathf.Infinity))
            {
                hit.collider.SendMessage("Damage", gunDamage);
                Debug.DrawLine(RaycastHolders[0].transform.position, hit.point, Color.red, 1f);
                Debug.Log("Fired Raycast");
            }
            currentAmmo--;
        }
    }

    public void TakeDamage()
    {
        _currentHealth--;
    }
    public void Heal()
    {
        _currentHealth++;
    }
    public void SwapToRevolver()
    {

    }
    public void SwapToShotgun()
    {
        if (_ActiveWeapon == ActiveWeapon.Handgun)
        {
            handgunAmmo += currentAmmo;
        }
    }






















    private void InputSetup()
    {
        _playerControls.Controller.LeftStickClick.performed += LeftStickClick_performed;
        _playerControls.Controller.RightStickClick.performed += RightStickClick_performed;
        _playerControls.Controller.Interact.performed += Interact_performed;
        _playerControls.Controller.Interact.canceled += Interact_canceled;
        _playerControls.Controller.Menu.performed += Menu_performed;

    }
    private void LeftStickClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // Need to finish inventory system to make weapon switching work
        isAiming = !isAiming;

        if (isAiming == true)
        {
            switch (_ActiveWeapon)
            {
                case ActiveWeapon.Handgun: //Pistol
                    //Ready Pistol
                    _Weapons[0].SetActive(true);
                    break;
                case ActiveWeapon.Shotgun: //Shotgun
                        //Ready Shotgun
                    _Weapons[1].SetActive(true);
                    break;
                case ActiveWeapon.SMG: //SMG
                        //Ready SMG
                    _Weapons[2].SetActive(true);
                    break;
                case ActiveWeapon.Rifle: //Rifle
                        //Ready Rifle
                    _Weapons[3].SetActive(true);
                    break;
            }
        }
        else
        {
            //Lower Equipped Weapon
        }
    }
    private void RightStickClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isReloading = true;

        switch (_ActiveWeapon)
        {
            case ActiveWeapon.Handgun: // Pistol
                _revolverAnim.SetBool("isReloading", true);
                StartCoroutine(ReloadTimers());

                break;
            case ActiveWeapon.Shotgun: // Shotgun

                break;
            case ActiveWeapon.SMG: // SMG

                break;
            case ActiveWeapon.Rifle: // Rifle

                break;
        }

        StartCoroutine(RStickFailsafeEnd());
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        aButton = true;
        //StartCoroutine(AButtonFailsafeEnd());
        Debug.Log("Interact");

    }
    private void Interact_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        aButton = false;

    }
    private IEnumerator AButtonFailsafeEnd()
    {
        yield return new WaitForSecondsRealtime(.1f);
        aButton = false;
    }
    private IEnumerator RStickFailsafeEnd()
    {
        Debug.Log("Reloading");
        yield return new WaitForSecondsRealtime(2f);
        isReloading = false;
        Debug.Log("Finished Reload");

    }
    private void Menu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        UIManager.Instance.GetComponent<UIManager>().PauseMenu();
    }

    private IEnumerator ReloadTimers()
    {
        switch (_ActiveWeapon)
        {
            case ActiveWeapon.Handgun: // Pistol
                yield return new WaitForSeconds(2f);
                _revolverAnim.SetBool("isReloading", false);
                break;
            case ActiveWeapon.Shotgun: // Shotgun
 
                break;
            case ActiveWeapon.SMG: // SMG

                break;
            case ActiveWeapon.Rifle: // Rifle
  
                break;
        }
    }
    private IEnumerator FireAnimTimers()
    {
        switch (_ActiveWeapon)
        {
            case ActiveWeapon.Handgun: // Pistol
                yield return new WaitForSeconds(1.2f);
                _revolverAnim.ResetTrigger("Shoot");
                break;
            case ActiveWeapon.Shotgun: // Shotgun

                break;
            case ActiveWeapon.SMG: // SMG

                break;
            case ActiveWeapon.Rifle: // Rifle

                break;
        }
    }
}

