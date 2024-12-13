using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetNextWaypoint(); // Se llama para iniciar el movimiento
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime; // Va actualizando el tiempo recorrido

        float elapsedPercentage = _elapsedTime / _timeToWaypoint; // Obtenemos el porcentaje del camino recorrido, diviendo el tiempo recorrido por el tiempo total calculado anteriormente
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage); // Modifica el progreso para que este no se mueva con velocidad constante si no que suaviza el moviemiento al inicio y al final
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage); // Calcula la posicion entre dos puntos basada en un porcentaje de "progreso" (elapsedPercentage)

        // Si el porcentaje ha llegado a 1 o es mayor a 1, significa que ya ha llegado a su punto destino por lo cual
        // llamamos a TargetNextWaypoint para seguir la ruta.
        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    // Obtiene el siguiente punto al cual debe de avanzar la plataforma
    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex); // Se actualiza para que el punto actual, ahora sea guardado como el punto anterior
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex); // Se actualiza el punto actual para que tenga el indice del siguiente punto
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex); // Se actualiza el game object con el punto al que ahora se dirije


        _elapsedTime = 0; // Se reinicia el tiempo recorrido

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position); // Se calcula la distancia entre el punto actual y el siguiente
        _timeToWaypoint = distanceToWaypoint / _speed; // Se calcula el tiempo total que toma ir de previousWaypoint al targetWaypoint | tiempo = distancia / velocidad



    }

}