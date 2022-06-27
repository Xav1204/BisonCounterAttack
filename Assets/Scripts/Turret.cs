using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Général")]
    public float range = 15f;
    private Transform target;
    private Ennemy targetEnnemy;

    [Header("Use Bullets")]
    public GameObject bulletPrefab;
    private float fireCountDown = 0f;
    public float fireRate = 1f;

    [Header("Use Laser")]
    public bool useLaser;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 25;
    public float slowAmount = 0.5f; //50%

    [Header("Unity Setup Fields")]
    public string ennemyTag = "Ennemy";
    public Transform partToRotate;
    private float turnSpeed = 5f;
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget(){
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(ennemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnnemy = null;

        foreach(GameObject ennemy in ennemies){
            float distanceToEnnemy = Vector3.Distance(transform.position, ennemy.transform.position);
            if(distanceToEnnemy < shortestDistance){
                shortestDistance = distanceToEnnemy;
                nearestEnnemy = ennemy;
            }
        }

        if(nearestEnnemy != null && shortestDistance <= range){
            target = nearestEnnemy.transform;
            targetEnnemy = target.GetComponent<Ennemy>();
        }
        else{
            target = null;
        }
    }

    void Update()
    {
        if(target == null){
            if(useLaser && lineRenderer.enabled){
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }

            return;
        }

        lockOnTarget();

        if(useLaser){
            Laser();
        }
        else{
            if(fireCountDown<= 0f){
                Shoot();
                fireCountDown = 1/fireRate;
            }
        fireCountDown -= Time.deltaTime;
        }
    }

    void lockOnTarget(){
        
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser(){
        targetEnnemy.takeDamage(damageOverTime * Time.deltaTime);
        targetEnnemy.Slow(slowAmount);

        if(lineRenderer.enabled == false){
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        impactEffect.transform.position = target.position + dir.normalized * 1.5f;
    }

    void Shoot(){
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null){
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
