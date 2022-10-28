using UnityEngine;

public class MovingPath : MonoBehaviour
{
    const float radiusSize = .5f;
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = NextIndex(i);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(GetWayPoint(i), radiusSize);
            Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
        }
    }

    public int NextIndex(int i)
    {
        return (i + 1) % transform.childCount;
    }

    public Vector3 GetWayPoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
