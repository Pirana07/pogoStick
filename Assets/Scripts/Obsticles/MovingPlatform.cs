using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] _waypoints;
    [SerializeField] float _speed = 2f;
     int _currentWaypointIndex = 0;

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform(){
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f) {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")){

            collision.transform.SetParent(null);
        }
    }
}
