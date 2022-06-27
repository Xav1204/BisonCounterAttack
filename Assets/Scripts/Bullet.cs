using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public GameObject impactEffect;

    public float speed = 50f;
    public float bulletDamage = 50;

    public float explosionRadius = 0f;

    public void Seek(Transform _target){
        target = _target;
    }

    void HitTarget(){
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        if(explosionRadius > 0){
            explode();
        }
        else{
            damage(target);
        }

        Destroy(gameObject);
    }

    void explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders){
            if(collider.tag == "Ennemy"){
                damage(collider.transform);
            }
        }
    }

    void damage(Transform ennemy){
        Ennemy e = ennemy.GetComponent<Ennemy>();
        if(e != null){
            e.takeDamage(bulletDamage);
        }
        else{
            Debug.LogError("Pas de Compoment Ennemy");
        }
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void Update() {
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
}
