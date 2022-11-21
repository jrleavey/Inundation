using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ActiveWeapon
    {
        None,
        Handgun,
        Shotgun,
        SMG,
        Rifle
    }
    [SerializeField]
    public ActiveWeapon _ActiveWeapon;

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
    [SerializeField]
    private int healthKits;

    [SerializeField]
    private int magazineCapacity = 6;
    [SerializeField]
    private int currentAmmo = 6;

    [SerializeField]
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
    [SerializeField]
    private float gunDamage;
    private float reloadtime;


    [SerializeField]
    private bool isAiming;
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool isReloading;
    [SerializeField]
    private bool aButton;
    [SerializeField]
    public bool isInvincible = false;
    [SerializeField]
    private bool didIPause = false;
    private bool StartLevel = true;
    [SerializeField]
    private bool isShootingTMP = false;


    [SerializeField]
    private GameObject _playerEyes;
    [SerializeField]
    private GameObject[] RaycastHolders;
    [SerializeField]
    private GameObject[] _Weapons;
    [SerializeField]
    private int _coins;

    [SerializeField]
    private Animator _revolverAnim;
    [SerializeField]
    private Animator _shotgunAnim;
    [SerializeField]
    private Animator _TMPAnim;
    [SerializeField]
    private Animator _rifleAnim;

    [SerializeField]
    private GameObject _bloodSplatter;


    [SerializeField] ParticleSystem shotgunffect;
    [SerializeField] ParticleSystem smgEffect;
    [SerializeField] ParticleSystem handgunEffect;
    [SerializeField] ParticleSystem rifleEffect;

    [SerializeField]
    private AudioSource _TMPAudioShoot;

    

    [SerializeField]
    private GameObject _footStepHolder;

    [SerializeField]
    private CharacterController _controller;
    float gravity = -9.81f;
    Vector3 Velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float grounddistance = .4f;
    public LayerMask groundmask;

    private void Awake()
    {
        
    }
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _currentHealth = 4;
        _speed = 7;
        didIPause = false;
        _playerControls = new PlayerControls();
        _playerControls.Controller.Enable();
        InputSetup();
    }

    void Update()
    {
        MovementController();

        WeaponControls();
        if (isMoving == true && didIPause == false && StartLevel == false)
        {
            _footStepHolder.SetActive(true);
        }
        else
        {
            _footStepHolder.SetActive(false);
        }

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

        isGrounded = Physics.CheckSphere(groundCheck.position, grounddistance, groundmask);

        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }    

        Vector3 move = transform.transform.right * leftStick.x + transform.forward * leftStick.y;

        _controller.Move(move * _speed * Time.deltaTime);

        Velocity.y += gravity * Time.deltaTime;

        _controller.Move(Velocity * Time.deltaTime);





        if (leftStick.x > .05 || leftStick.x < -.05 ||leftStick.y > .05 || leftStick.y < -.05)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

     


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
                if (isShootingTMP == true)
                {
                    _TMPAudioShoot.gameObject.SetActive(true);
                    _TMPAnim.SetBool("isShooting", true);
                }
                else
                {
                    _TMPAudioShoot.gameObject.SetActive(false);
                    _TMPAnim.SetBool("isShooting", false);
                }
                break;
            case ActiveWeapon.Rifle: // Rifle
                RifleLogic();
                break;
            case ActiveWeapon.None:
                break;
        }
    }
    
    public void PickedUpItem(int _itemID)
    {
        if (didIPause == false)
        {
            switch (_itemID)
            {
                case 0: // Handgun Bullets
                    handgunAmmo += 10;
                    AudioManager.Instance.Play("PickupAmmo");
                    break;
                case 1: // Shotgun Bullets
                    shotgunAmmo += 5;
                    AudioManager.Instance.Play("PickupAmmo");

                    break;
                case 2: // SMG Bullets
                        //NOT USED
                    smgAmmo += 50;

                    break;
                case 3: // Rifle Bullets
                    rifleAmmo += 4;
                    AudioManager.Instance.Play("PickupAmmo");

                    break;
                case 4: // Health
                    healthKits++;
                    AudioManager.Instance.Play("PickupHealth");
                    break;
                case 5:
                    SwapToRevolver();
                    currentAmmo = 6;
                    AudioManager.Instance.Play("Equip");
                    UIManager.Instance.FoundWeapon(_itemID);
                    UIManager.Instance.UpdateRevolverAmmo(currentAmmo);

                    break;
                case 6:
                    SwapToShotgun();
                    currentAmmo = 5;
                    AudioManager.Instance.Play("Equip");
                    UIManager.Instance.FoundWeapon(_itemID);
                    UIManager.Instance.UpdateShotgunAmmo(currentAmmo);
                    break;
                case 7:
                    SwapToRifle();
                    currentAmmo = 4;
                    AudioManager.Instance.Play("Equip");
                    UIManager.Instance.FoundWeapon(_itemID);
                    UIManager.Instance.UpdateRifleAmmo(currentAmmo);
                    break;
                case 8:
                    _coins += Random.Range(1, 10) * 100;
                    UIManager.Instance.UpdateCoins(_coins);
                    break;
                case 9:
                    SwapToTMP();
                    currentAmmo = 30;
                    AudioManager.Instance.Play("Equip");
                    UIManager.Instance.FoundWeapon(_itemID);
                    UIManager.Instance.UpdateTMPAmmo(currentAmmo);
                    break;
                case 10:
                    smgAmmo += Random.Range(5,25);
                    AudioManager.Instance.Play("PickupAmmo");
                    break;
            }
            UIManager.Instance.UpdateAmmoReserves(handgunAmmo, shotgunAmmo, rifleAmmo, smgAmmo, healthKits);
        }      
    }

    private void PistolLogic()
    {
        magazineCapacity = 6;
        reloadtime = 2f;
        fireRate = 1.3f;
        gunDamage = Random.Range(1, 3);
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire && isReloading == false)
        {
            handgunEffect.Play();
            currentAmmo--;
            AudioManager.Instance.Play("Revolver Shoot");
            UIManager.Instance.UpdateRevolverAmmo(currentAmmo);
            _revolverAnim.SetTrigger("Shoot");
            StartCoroutine(FireAnimTimers());
            aButton = false;
            RaycastHit hit;
            nextFire = Time.time + fireRate;

            if (Physics.Raycast(RaycastHolders[0].transform.position, RaycastHolders[0].transform.forward, out hit, Mathf.Infinity))
            {
                hit.collider.SendMessage("Damage", gunDamage);
                Debug.DrawLine(RaycastHolders[0].transform.position, hit.point, Color.red, 1f);
                if (hit.transform.tag == "Enemy")
                {
                    Instantiate(_bloodSplatter, hit.point, Quaternion.identity);
                }
            }
        }
        else if (isAiming == true && aButton == true && currentAmmo == 0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            AudioManager.Instance.Play("Out of Ammo");
           return;

            
        }
    }
    private void ShotgunLogic()
    {
        magazineCapacity = 5;
        reloadtime = 3f;
        fireRate = 2.2f;
        gunDamage = Random.Range(1, 2);
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire)
        {
            shotgunffect.Play();
            currentAmmo--;
            AudioManager.Instance.Play("Shotgun Shoot");
            UIManager.Instance.UpdateShotgunAmmo(currentAmmo);
            _shotgunAnim.SetTrigger("Shoot");
            StartCoroutine(FireAnimTimers());
            aButton = false;
            nextFire = Time.time + fireRate;
            int amountOfProjectiles = 8;
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                ShotgunRay();
            }
        }
        else if (isAiming == true && aButton == true && currentAmmo == 0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            AudioManager.Instance.Play("Out of Ammo");
            return;


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
            if (hit.transform.tag == "Enemy")
            {
                Instantiate(_bloodSplatter, hit.point, Quaternion.identity);
            }

            if (Physics.Raycast(RaycastHolders[1].transform.position, direction, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(RaycastHolders[1].transform.position, hit.point, Color.red, 1f);
            }
        }
    }
    private void SMGLogic()
    {
        magazineCapacity = 30;
        reloadtime = 2.3f;
        fireRate = .1f;
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire)
        {
            isShootingTMP = true;

                StartCoroutine(SMGFiringLogic());
                UIManager.Instance.UpdateTMPAmmo(currentAmmo);
                StartCoroutine(FireAnimTimers());
                aButton = false;
            nextFire = Time.time + fireRate;
            
        }
        else if (isAiming == true && aButton == true && currentAmmo == 0 && Time.time > nextFire)
        {
            isShootingTMP = false;
            nextFire = Time.time + fireRate;
            AudioManager.Instance.Play("Out of Ammo");
            return;
        }
    }
    private IEnumerator SMGFiringLogic()
    {
        yield return new WaitForSeconds(.1f);
        currentAmmo--;
        int amountOfProjectiles = 1;
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            SMGSpread();
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
        magazineCapacity = 4;
        reloadtime = 6f;
        fireRate = 3f;
        gunDamage = Random.Range(8, 16);
        if (isAiming == true && aButton == true && currentAmmo >= 1 && Time.time > nextFire && isReloading == false)
        {
            handgunEffect.Play();
            currentAmmo--;
            AudioManager.Instance.Play("Rifle Shoot");
            UIManager.Instance.UpdateRifleAmmo(currentAmmo);
            _rifleAnim.SetTrigger("Shoot");
            StartCoroutine(FireAnimTimers());
            aButton = false;
            RaycastHit hit;
            nextFire = Time.time + fireRate;

            if (Physics.Raycast(RaycastHolders[0].transform.position, RaycastHolders[0].transform.forward, out hit, Mathf.Infinity))
            {
                hit.collider.SendMessage("Damage", gunDamage);
                Debug.DrawLine(RaycastHolders[0].transform.position, hit.point, Color.red, 1f);
                if (hit.transform.tag == "Enemy")
                {
                    Instantiate(_bloodSplatter, hit.point, Quaternion.identity);
                }
            }
        }
        else if (isAiming == true && aButton == true && currentAmmo == 0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            AudioManager.Instance.Play("Out of Ammo");
            return;


        }
    }

    public void TakeDamage()
    {
        if (isInvincible == true)
        {
            return;
        }
        else
        {
            if (_currentHealth != 1)
            {
                AudioManager.Instance.Play("PlayerDamage");
            }
            _currentHealth--;
            _speed = 9;
            StartCoroutine(BecomeInvincible());
            CMCameraShake.Instance.ShakeCamera(1f, .5f);
            UIManager.Instance.BloodImageVisible();
            UIManager.Instance.UpdateHealth(_currentHealth);
            if (_currentHealth == 0)
            {
                Die();
                return;
            }
            isInvincible = true;
        }
    }
    private IEnumerator BecomeInvincible()
    {
        yield return new WaitForSeconds(2.5f);
        isInvincible = false;
        _speed = 7;
    }
    public void Heal()
    {
        if (healthKits == 0 || _currentHealth >= 4)
        {
            return;
        }
        else if (healthKits >= 1)
        {
            healthKits--;
            _currentHealth+= 2;
            if (_currentHealth > 4)
            {
                _currentHealth = 4;
            }
            AudioManager.Instance.Play("Heal");
            UIManager.Instance.UpdateHealth(_currentHealth);
        }
    }
    public void Die()
    {
        AudioManager.Instance.Play("PlayerDeath");
        UIManager.Instance.Die();
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
        if (didIPause == false)
        {
            isAiming = !isAiming;
            UIManager.Instance.SwapCrosshair();

            if (isAiming == true)
            {
                switch (_ActiveWeapon)
                {
                    case ActiveWeapon.Handgun: //Pistol
                        _revolverAnim.SetBool("isAiming", true);
                        break;
                    case ActiveWeapon.Shotgun: //Shotgun
                                               //Ready Shotgun
                        _shotgunAnim.SetBool("isAiming", true);

                        break;
                    case ActiveWeapon.SMG: //SMG
                        _TMPAnim.SetBool("isAiming", true);
                        break;
                    case ActiveWeapon.Rifle: //Rifle
                                             //Ready Rifle
                        _rifleAnim.SetBool("isAiming", true);

                        break;
                }
            }
            else
            {
                _revolverAnim.SetBool("isAiming", false);
                _shotgunAnim.SetBool("isAiming", false);
                _TMPAnim.SetBool("isAiming", false);
                _rifleAnim.SetBool("isAiming", false);

            }
        }
    }
    private void RightStickClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        switch (_ActiveWeapon)
        {
            case ActiveWeapon.Handgun: // Pistol
                if (currentAmmo != 6 && handgunAmmo >= 1)
                {
                    isReloading = true;
                    AudioManager.Instance.Play("Revolver Reload");
                    _revolverAnim.SetBool("isReloading", true);
                    StartCoroutine(ReloadTimers());
                }
                break;
            case ActiveWeapon.Shotgun: // Shotgun
                if (currentAmmo != 5 && shotgunAmmo >= 1)
                {
                    isReloading = true;
                    AudioManager.Instance.Play("Shotgun Reload");
                    _shotgunAnim.SetBool("isReloading", true);
                    StartCoroutine(ReloadTimers());
                }
                break;
            case ActiveWeapon.SMG: // SMG
                if (currentAmmo != 30 && smgAmmo >= 1)
                {
                    isReloading = true;
                    AudioManager.Instance.Play("TMP Reload");
                    _TMPAnim.SetBool("isReloading", true);
                    StartCoroutine(ReloadTimers());
                }
                break;
            case ActiveWeapon.Rifle: // Rifle
                if (currentAmmo != 4 && rifleAmmo >= 1)
                {
                    isReloading = true;
                    AudioManager.Instance.Play("Rifle Reload");
                    _rifleAnim.SetBool("isReloading", true);
                    StartCoroutine(ReloadTimers());
                }
                break;
        }

        StartCoroutine(RStickFailsafeEnd());
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        aButton = true;

        if (StartLevel == true)
        {
            StartLevel = false;
            UIManager.Instance.GameTimeStart();
        }


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
        yield return new WaitForSecondsRealtime(2f);
        isReloading = false;

    }
    private void Menu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        didIPause = !didIPause;

            UIManager.Instance.GetComponent<UIManager>().PauseMenu();
            UpdateAmmoReserves();
        
    }

    private IEnumerator ReloadTimers()
    {
        int missingbullets;
        switch (_ActiveWeapon)
        {
            case ActiveWeapon.Handgun: // Pistol
                missingbullets = Mathf.Abs(magazineCapacity - currentAmmo);
                yield return new WaitForSeconds(reloadtime);
                _revolverAnim.SetBool("isReloading", false);
                if ( currentAmmo + handgunAmmo >= magazineCapacity)
                {
                    currentAmmo = magazineCapacity;
                    handgunAmmo -= missingbullets;
                }
                else if (currentAmmo + handgunAmmo < magazineCapacity)
                {
                    currentAmmo = currentAmmo + handgunAmmo;
                    handgunAmmo = 0;
                }
                isReloading = false;
                UIManager.Instance.UpdateRevolverAmmo(currentAmmo);
                break;

            case ActiveWeapon.Shotgun: // Shotgun 
                missingbullets = Mathf.Abs(magazineCapacity - currentAmmo);
                yield return new WaitForSeconds(reloadtime);
                _shotgunAnim.SetBool("isReloading", false);
                if (currentAmmo + shotgunAmmo >= magazineCapacity)
                {
                    currentAmmo = magazineCapacity;
                    shotgunAmmo -= missingbullets;
                }
                else if (currentAmmo + shotgunAmmo < magazineCapacity)
                {
                    currentAmmo = currentAmmo + shotgunAmmo;
                    shotgunAmmo = 0;
                }
                isReloading = false;
                UIManager.Instance.UpdateShotgunAmmo(currentAmmo);

                break;

            case ActiveWeapon.SMG: // SMG
                missingbullets = Mathf.Abs(magazineCapacity - currentAmmo);
                yield return new WaitForSeconds(reloadtime);
                _TMPAnim.SetBool("isReloading", false);
                if (currentAmmo + smgAmmo >= magazineCapacity)
                {
                    currentAmmo = magazineCapacity;
                    smgAmmo -= missingbullets;
                }
                else if (currentAmmo + shotgunAmmo < magazineCapacity)
                {
                    currentAmmo = currentAmmo + smgAmmo;
                    smgAmmo = 0;
                }
                isReloading = false;
                UIManager.Instance.UpdateTMPAmmo(currentAmmo);

                break;


            case ActiveWeapon.Rifle: // Rifle
                missingbullets = Mathf.Abs(magazineCapacity - currentAmmo);
                yield return new WaitForSeconds(reloadtime);
                _rifleAnim.SetBool("isReloading", false);
                if (currentAmmo + rifleAmmo >= magazineCapacity)
                {
                    currentAmmo = magazineCapacity;
                    rifleAmmo -= missingbullets;
                }
                else if (currentAmmo + rifleAmmo < magazineCapacity)
                {
                    currentAmmo = currentAmmo + rifleAmmo;
                    rifleAmmo = 0;
                }
                isReloading = false;
                UIManager.Instance.UpdateRifleAmmo(currentAmmo);
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
                yield return new WaitForSeconds(1.2f);
                _shotgunAnim.ResetTrigger("Shoot");
                break;
            case ActiveWeapon.SMG: // SMG
                yield return new WaitForSeconds(.1f);
                _shotgunAnim.ResetTrigger("Shoot");
                break;
            case ActiveWeapon.Rifle: // Rifle
                yield return new WaitForSeconds(2f);
                _shotgunAnim.ResetTrigger("Shoot");
                break;
        }
    }
    public void SwapToRevolver()
    {
        _shotgunAnim.SetBool("isReloading", false);
        _TMPAnim.SetBool("isReloading", false);
        _rifleAnim.SetBool("isReloading", false);
        isReloading = false;

        if (_ActiveWeapon == ActiveWeapon.Handgun)
        {
            handgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Shotgun)
        {
            shotgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.SMG)
        {
            smgAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Rifle)
        {
            rifleAmmo += currentAmmo;
        }


        if (handgunAmmo >= 6)
        {
            handgunAmmo -= 6;
            currentAmmo = 6;
        }
        else
        {
            currentAmmo = handgunAmmo;
            handgunAmmo = 0;
        }
        _ActiveWeapon = ActiveWeapon.Handgun;
        UIManager.Instance.UpdateRevolverAmmo(currentAmmo);
        isAiming = false;
        _Weapons[0].SetActive(true);
        _Weapons[1].SetActive(false);
        _Weapons[2].SetActive(false);
        _Weapons[3].SetActive(false);

    }
    public void SwapToShotgun()
    {
        _revolverAnim.SetBool("isReloading", false);
        _TMPAnim.SetBool("isReloading", false);
        _rifleAnim.SetBool("isReloading", false);
        isReloading = false;
        if (_ActiveWeapon == ActiveWeapon.Handgun)
        {
            handgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Shotgun)
        {
            shotgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.SMG)
        {
            smgAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Rifle)
        {
            rifleAmmo += currentAmmo;
        }


        if (shotgunAmmo >= 5)
        {
            shotgunAmmo -= 5;
            currentAmmo = 5;
        }
        else
        {
            currentAmmo = shotgunAmmo;
            shotgunAmmo = 0;

        }
        _ActiveWeapon = ActiveWeapon.Shotgun;
        UIManager.Instance.UpdateShotgunAmmo(currentAmmo);

        isAiming = false;
        _Weapons[0].SetActive(false);
        _Weapons[1].SetActive(true);
        _Weapons[2].SetActive(false);
        _Weapons[3].SetActive(false);



    }
    public void SwapToTMP()
    {
        _revolverAnim.SetBool("isReloading", false);
        _shotgunAnim.SetBool("IsReloading", false); 
        _rifleAnim.SetBool("isReloading", false);
        isReloading = false;
        if (_ActiveWeapon == ActiveWeapon.Handgun)
        {
            handgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Shotgun)
        {
            shotgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.SMG)
        {
            smgAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Rifle)
        {
            rifleAmmo += currentAmmo;
        }


        if (smgAmmo >= 30)
        {
            shotgunAmmo -= 30;
            currentAmmo = 30;
        }
        else
        {
            currentAmmo = smgAmmo;
            smgAmmo = 0;

        }
        _ActiveWeapon = ActiveWeapon.SMG;
        UIManager.Instance.UpdateTMPAmmo(currentAmmo);

        isAiming = false;
        _Weapons[0].SetActive(false);
        _Weapons[1].SetActive(false);
        _Weapons[2].SetActive(true);
        _Weapons[3].SetActive(false);
        PickedUpItem(9);
        UIManager.Instance.CloseStore();
    }
    public void SwapToRifle()
    {
        _shotgunAnim.SetBool("isReloading", false);
        _revolverAnim.SetBool("isReloading", false);
        isReloading = false;
        if (_ActiveWeapon == ActiveWeapon.Handgun)
        {
            handgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Shotgun)
        {
            shotgunAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.SMG)
        {
            smgAmmo += currentAmmo;
        }
        else if (_ActiveWeapon == ActiveWeapon.Rifle)
        {
            rifleAmmo += currentAmmo;
        }


        if (rifleAmmo >= 4)
        {
            rifleAmmo -= 4;
            currentAmmo = 4;
        }
        else
        {
            currentAmmo = rifleAmmo;
            rifleAmmo = 0;
        }
        _ActiveWeapon = ActiveWeapon.Rifle;
        UIManager.Instance.UpdateRifleAmmo(currentAmmo);
        isAiming = false;

        _Weapons[0].SetActive(false);
        _Weapons[1].SetActive(false);
        _Weapons[2].SetActive(false);
        _Weapons[3].SetActive(true);
    }
    public void UpdateAmmoReserves()
    {
        UIManager.Instance.UpdateAmmoReserves(handgunAmmo, shotgunAmmo, rifleAmmo, smgAmmo, healthKits);
    }
   
    public void UnpauseConsistency()
    {
        didIPause = false;
    }
    public void UnpauseConsistency2()
    {
        didIPause = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pickup")
        {

            if (aButton == true && isAiming == false)
            {
                other.GetComponent<PickUp>().ItemLogic();
                Destroy(other.gameObject);
                UIManager.Instance.ClearPickupPrompt();
            }
        }
        if (other.tag == "Store")
        {
            UIManager.Instance.StorePrompt();
            if (aButton == true && isAiming == false)
            {
                UIManager.Instance.Openstore();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "End")
        {
            didIPause = true;
            UIManager.Instance.EndGame();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Store")
        {
            UIManager.Instance.ClearPickupPrompt();
        }
    }
    public void ResetInputSystem()
    {
        _playerControls = new PlayerControls();
        _playerControls.Controller.Enable();
        InputSetup();
    }
    public void NotAiming()
    {
        _revolverAnim.SetBool("isAiming", false);
        _shotgunAnim.SetBool("isAiming", false);
        _TMPAnim.SetBool("isAiming", false);
        _rifleAnim.SetBool("isAiming", false);

    }

    public void BoughtHandgunAmmo()
    {
        handgunAmmo += 10;
        _coins -= 600;
        UIManager.Instance.UpdateCoins(_coins);
        UpdateAmmoReserves();
        UIManager.Instance.UpdateStoreCoins();

    }
    public void BoughtShotgunAmmo()
    {
        shotgunAmmo += 5;
        _coins -= 800;
        UIManager.Instance.UpdateCoins(_coins);
        UpdateAmmoReserves();
        UIManager.Instance.UpdateStoreCoins();
    }
    public void BoughtTMPAmmo()
    {
        smgAmmo += 20;
        _coins -= 4000;
        UIManager.Instance.UpdateCoins(_coins);
        UpdateAmmoReserves();
        UIManager.Instance.UpdateStoreCoins();
    }
    public void BoughtRifleAmmo()
    {
        rifleAmmo += 4;
        _coins -= 1200;
        UIManager.Instance.UpdateCoins(_coins);
        UpdateAmmoReserves();
        UIManager.Instance.UpdateStoreCoins();
    }

    public void BoughtTMPWeapon()
    {
        SwapToTMP();
        _coins -= 12000;
    }
    public void BoughtHealthkit()
    {
        healthKits++;
        _coins -= 1500;
        UpdateAmmoReserves();
        UIManager.Instance.UpdateCoins(_coins);
        UIManager.Instance.UpdateStoreCoins();
    }
}

