using UnityEngine;

[RequireComponent(typeof(Ennemy))]
public class EnnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    private Ennemy ennemy;
    public Transform directionToRotate;

    void Start() {
        target = WayPoints.points[0];
        ennemy = GetComponent<Ennemy>();
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * ennemy.speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(-dir);

        if(Vector3.Distance(transform.position, target.position) <= 0.3f){
            GetNextWayPoint();
        }

        ennemy.speed = ennemy.baseSpeed;
    }
    
    private void GetNextWayPoint(){

        if(waypointIndex >= WayPoints.points.Length -1){
            endPath();
            return;
        }
        waypointIndex++;
        target = WayPoints.points[waypointIndex];  
    }

    private void endPath(){
        PlayerStats.lives--;
        Destroy(gameObject);
    }
}
