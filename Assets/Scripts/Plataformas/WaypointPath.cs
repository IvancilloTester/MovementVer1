using UnityEngine;

public class WaypointPath : MonoBehaviour
{

    // Regresa un Waypoint a partir de su indice, donde los Waypoints son los hijos del objeto que tiene este script
    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    // Regresa el indice del siguiente Waypoint en la ruta y se asegura de reiniciar la ruta una vez que se haya recorrido por completo.
    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }

}
