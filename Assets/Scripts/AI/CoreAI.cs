using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoreAI : MonoBehaviour
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
    private bool _IAmWaiting;
    private bool _amIAttacking;
    public bool _isChasingPlayer;
    public bool canSeePlayer;
    private bool isDying = false;



    public GameObject _player;


    [SerializeField]
    private AudioSource _audioSource;
    private Animator _anim;


    public LayerMask targetMask;
    public LayerMask ObstructionMask;


    [SerializeField]
    private GameObject[] _hitboxes;
    private BoxCollider _boxcollider;


    [Range(0, 500)] public float walkRadius;


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
        _currentHp = Random.Range(8, 12);
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
            _navMeshAgent.speed = 3;
        }

        if (_IAmWaiting == false)
        {
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
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
        if (_navMeshAgent != null)
        {
            _isChasingPlayer = true;
            _navMeshAgent.destination = _player.transform.position;

            if (_navMeshAgent.remainingDistance < 2f)
            {
                _anim.SetBool("isAttacking", true);
                _hitboxes[0].SetActive(true);
            }
            else
            {
                _anim.SetBool("isAttacking", false);
                _hitboxes[0].SetActive(false);

            }
        }
    }
    private void Damage()
    {
        _currentHp--;
        if (_currentHp >= 1)
        {
            _AIState = AIState.Hostile;
        }
        else if (_currentHp <= 0)
        {
            _AIState = AIState.Dying;
        }

    }
    private void Die()
    {
        if (_currentHp <= 0)
        {
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
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
