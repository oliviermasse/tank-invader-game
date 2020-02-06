using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    List<Transform> waypoints; //enemy pathway
    WaveConfig waveConfig;
    int waypointIndex = 0;
    
    void Start()
    {
        waypoints = waveConfig.GetWaypoints(); //initial spawn point
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;  // next waypoint
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;                 
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame); 

            if (transform.position == targetPosition)
            {
                waypointIndex++;  // increment index when a waypoint is reached
            }
        }
        else
        {
            Destroy(gameObject); // upon arrival to final waypoint
        }
    }
}
