using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DredgeAI : MonoBehaviour
{
    private enum AIState
    {
        Passive,
        Hostile,
        Dying
    }

    [SerializeField]
    private AIState _AIState;


    private NavMeshAgent _navMeshAgent;


    public float radius = 20f;
    public float angle = 90f;


    [SerializeField]
    private float _currentHp;
    [SerializeField]
    private float _currentPoise;


    [SerializeField]
    private bool _IAmWaiting;
    private bool _amIAttacking;
    public bool _isChasingPlayer;
    public bool canSeePlayer;
    private bool isDying = false;
    private bool isStaggered = false;
    private bool attackRooted = false;
    private bool isRoared;
    private bool canPlayAttackSound = true;
    private bool canPlayDeathSound = true;
    private bool droppedanItem = false;


    public GameObject _player;


    [SerializeField]
    private AudioSource _audioSource;
    private Animator _anim;


    public LayerMask targetMask;
    public LayerMask ObstructionMask;


    [SerializeField]
    private GameObject[] _hitboxes;
    private BoxCollider _boxcollider;

    [SerializeField]
    private AudioClip[] _audioclips;


    [Range(0, 500)] public float walkRadius;

    private int[] itemDropChange =
    {
        50,
        15,
        13,
        10,
        12,
    };

    public GameObject[] items;


    private void Awake()
    {
        
    }
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _boxcollider = GetComponent<BoxCollider>();
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player");
        StartCoroutine(CheckForPlayer());
        _currentHp = Random.Range(12, 20);
        _currentPoise = Random.Range(6, 12);
    }
    private void Update()
    {
        switch (_AIState)
        {
            case AIState.Passive:
                Wander();
                break;
            case AIState.Hostile:
                StopCoroutine(RandomWaitTimer());               
                ChasePlayer();               
                break;
            case AIState.Dying:
                Die();
                break;
        }

        if (canSeePlayer == true)
        {
            _AIState = AIState.Hostile;
            if (isRoared == false)
            {
                AudioSource.PlayClipAtPoint(_audioclips[0], transform.position);
                isRoared = true;
            }
        }

        if (_IAmWaiting == false)
        {
            _anim.SetBool("isWalking", true);
        }
        if (_IAmWaiting == true)
        {
            _anim.SetBool("isWalking", false);
        }
        else if (_IAmWaiting == false && _amIAttacking == false)
        {
            _anim.SetBool("isWalking", true);

        }
        if (isDying == true)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 0.5f);
        }
    }

    private void Wander()
    {
        if (_navMeshAgent != null && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && _IAmWaiting == false && canSeePlayer == false)
        {
            _navMeshAgent.SetDestination(RandomNavMeshLocation());
            _IAmWaiting = true;
            StartCoroutine(RandomWaitTimer());
       
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
        }
    }
    private IEnumerator CheckForPlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }

    }
    IEnumerator RandomWaitTimer()
    {
        int wait_time = Random.Range(3, 7);
        _navMeshAgent.speed = 0;
        yield return new WaitForSeconds(wait_time);
        _navMeshAgent.speed = 3f;
        print("I waited for " + wait_time + "sec");
        _IAmWaiting = false;
    }
    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    private void ChasePlayer()
    {
        if (_navMeshAgent.isActiveAndEnabled)
        {
            _isChasingPlayer = true;
            _navMeshAgent.destination = _player.transform.position;

            if (_navMeshAgent.remainingDistance < 2f)
            {
                attackRooted = true;
                StartCoroutine(AttackRootTimer());
                _navMeshAgent.speed = 0;
                _anim.SetBool("isAttacking", true);
                _hitboxes[0].SetActive(true);
                _hitboxes[1].SetActive(true);
                if (canPlayAttackSound == true)
                {
                    AudioSource.PlayClipAtPoint(_audioclips[1], transform.position);
                    canPlayAttackSound = false;
                    StartCoroutine(SoundTimer());
                }
            }
            else if (_navMeshAgent.remainingDistance > 2f)
            {
                if (attackRooted == false && isStaggered == false)
                {
                    _navMeshAgent.speed = 5.5f;
                }
                _anim.SetBool("isAttacking", false);
                _hitboxes[0].SetActive(false);
                _hitboxes[1].SetActive(false);

            }

            if (_navMeshAgent.remainingDistance > 10f && _AIState == AIState.Hostile && isStaggered == false && attackRooted == false)
            {
                _navMeshAgent.speed = 8;
                _anim.SetBool("isRunning", true);
                _anim.SetBool("isWalking", false);
            }
            else if (_navMeshAgent.remainingDistance < 10f && _AIState == AIState.Hostile && isStaggered == false && attackRooted == false|| _AIState == AIState.Passive || _AIState == AIState.Dying)
            {
               
                _navMeshAgent.speed = 5.5f;
                _anim.SetBool("isRunning", false);
                _anim.SetBool("isWalking", true);
            }
        }
    }
    private void Damage(int gundamage)
    {
        _currentHp -= gundamage;
        AudioSource.PlayClipAtPoint(_audioclips[2], transform.position);

        _currentPoise -= Random.Range(1, 3);
        if (_currentHp >= 1 && _AIState == AIState.Passive)
        {
            _AIState = AIState.Hostile;
        }
        else if (_currentHp <= 0)
        {
            _AIState = AIState.Dying;
        }

        if (_currentPoise <= 0)
        {
            StartCoroutine(Stagger());
        }

    }
    private IEnumerator Stagger()
    {
        isStaggered = true;
        _anim.SetBool("isRunning", false);
        _anim.SetBool("isWalking", false);
        _anim.SetTrigger("Stagger");
        _navMeshAgent.speed = 0;
        AudioSource.PlayClipAtPoint(_audioclips[3], transform.position);

        yield return new WaitForSeconds(2f);
        isStaggered = false;
        _anim.ResetTrigger("Stagger");
        _currentPoise = Random.Range(6, 12);
    }
    private void Die()

    {
        if (_currentHp <= 0)
        {
            if (canPlayDeathSound == true)
            {
                AudioSource.PlayClipAtPoint(_audioclips[4], transform.position);
                canPlayDeathSound = false;
            }

            _boxcollider.enabled = false;
            _navMeshAgent.enabled = false;
            _anim.SetTrigger("Die");
            StartCoroutine(DespawnTimer());
        }
    }
    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(2.5f);
        isDying = true;
        StartCoroutine(DropItems());
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    private IEnumerator AttackRootTimer()
    {
        yield return new WaitForSeconds(1f);
        attackRooted = false;
    }
    private IEnumerator SoundTimer()
    {
        yield return new WaitForSeconds(2f);
        canPlayAttackSound = true;
    }
    private IEnumerator DropItems()
    {
        if (droppedanItem == false)
        {
            droppedanItem = true;
            Debug.Log("CalledDropItems");
            int randomItem = randomTable();
            GameObject newItem = Instantiate(items[randomItem], this.transform.position, Quaternion.identity);
            newItem.transform.parent = null;
            yield return new WaitForSeconds(3f);
        }
    }
    int randomTable()
    {
        int rng = Random.Range(1, 101);
        for (int i = 0; i < itemDropChange.Length; i++)
        {
            if (rng <= itemDropChange[i])
            {
                return i;
            }
            else
            {
                rng -= itemDropChange[i];
            }
        }
        return 0;
    }
}
