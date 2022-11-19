using UnityEngine;
using UnityEngine.AI;

public class BoatRide : MonoBehaviour
{
    GameObject player;
    NavMeshAgent nav;
    Transform defaultPlayerTransform;

    [SerializeField] GameObject invisibleHandles;
    [SerializeField] Transform destination;
    [SerializeField] float boatSpeed = 3.5f;
    [SerializeField] float detectingBoatRange = 1f;
    [SerializeField] float detectingBoatDestinationRange = .1f;
    bool BoatIsDriving
    {
        get { return _isDriving; }
        set
        {
            if (_isDriving != value)
            {
                player.transform.parent = gameObject.transform;
                nav.SetDestination(destination.position);
                invisibleHandles.SetActive(!_isDriving);
            }
        }
    }
    bool BoatHasArrived
    {
        get { return _hasArrived; }
        set
        {
            if (_hasArrived != value)
            {
                player.transform.parent = defaultPlayerTransform;
                invisibleHandles.SetActive(_hasArrived);
            }
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        nav.speed = boatSpeed;
        defaultPlayerTransform = player.transform.parent;
    }

    bool IsPlayerClose() => Vector3.Distance(player.transform.position, gameObject.transform.position) < detectingBoatRange;

    bool CheckDestination() => Vector3.Distance(gameObject.transform.position, destination.position) < detectingBoatDestinationRange;

    private void Update()
    {
        if (IsPlayerClose() && Input.GetKeyDown(KeyCode.E))
        {
            this.BoatIsDriving = !BoatIsDriving;
        }

        if (CheckDestination())
        {
            this.BoatHasArrived = !BoatHasArrived;
        }
    }
    static bool _isDriving = false;
    static bool _hasArrived = false;
}
