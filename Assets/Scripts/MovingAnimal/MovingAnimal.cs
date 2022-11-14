using UnityEngine;
using UnityEngine.AI;

public class MovingAnimal : MonoBehaviour
{
    [SerializeField] MovingPath movingPath;
    [SerializeField] float tolerance = 0.5f;
    [SerializeField] float waitingTime = 3f;

    Transform playerLocation;
    float allowedDistance = 10f;
    float timer = Mathf.Infinity;
    int currentIndex = 0;
    int randomIndex = 0;
    NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        if (playerLocation == null)
        {
            playerLocation = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDistance())
        {
            MoveAnimal();
        }
        else
        {
            Flee();
        }
        timer += Time.deltaTime;
    }

    void MoveAnimal()
    {
        int pathChildrenCount = movingPath.transform.childCount;

        Vector3 nextMove = transform.position;
        if (movingPath != null)
        {
            if (AtWaypoint())
            {
                CycleWaypoint();
                timer = 0;
                randomIndex = Random.Range(0, pathChildrenCount);
            }
            nextMove = GetCurrentWaypoint();
        }

        if (timer > waitingTime)
        {
            nav.SetDestination(nextMove);
        }
    }

    bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < tolerance;
    }
    void CycleWaypoint()
    {
        currentIndex = movingPath.NextIndex(randomIndex);
    }
    Vector3 GetCurrentWaypoint()
    {
        return movingPath.GetWayPoint(randomIndex);
    }

    bool CheckDistance()
    {
        float DistancePlayerAndAnimal = Vector3.Distance(transform.position, playerLocation.position);
        return allowedDistance < DistancePlayerAndAnimal;
    }
    void Flee()
    {
        Vector3 fleeDirection = transform.position - playerLocation.position;
        Vector3 fleeDestination = transform.position + fleeDirection;
        nav.SetDestination(fleeDestination);
    }

}
