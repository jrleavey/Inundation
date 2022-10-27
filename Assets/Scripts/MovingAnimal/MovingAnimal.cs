using UnityEngine;
using UnityEngine.AI;

public class MovingAnimal : MonoBehaviour
{
    [SerializeField] MovingPath movingPath;
    [SerializeField] float tolerance = 0.5f;
    [SerializeField] float waitingTime = 3f;
    float timer = Mathf.Infinity;

    int currentIndex = 0;
    int randomIndex = 0;
    NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimal();
        timer += Time.deltaTime;
    }
    private void MoveAnimal()
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

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < tolerance;
    }
    private void CycleWaypoint()
    {
        currentIndex = movingPath.NextIndex(randomIndex);
    }
    private Vector3 GetCurrentWaypoint()
    {
        return movingPath.GetWayPoint(randomIndex);
    }
}
