using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    public Transform[] WayPoints => wayPoints;
    private void OnDrawGizmos()
    {
        if (wayPoints != null && wayPoints.Length > 1)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < wayPoints.Length - 1; i++)
            {
                Transform from = wayPoints[i];
                Transform to = wayPoints[i + 1];
                Gizmos.DrawLine(from.position, to.position);
            }
            Gizmos.DrawLine(wayPoints[0].position, wayPoints[wayPoints.Length - 1].position);
        }
    }
}
