using UnityEngine;
using UnityEngine.UI;
public class Ennemy : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int valueEnnemy = 50;
    public GameObject deadEffect;
    public Image healthBar;

    private void Start()
    {
        speed = baseSpeed;
        health = startHealth;
    }

    public void takeDamage(float amount){
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0){
            Die();
        }
    }

    public void Slow(float amount){
        speed = baseSpeed * (1f - amount);
    }

    private void Die(){
        PlayerStats.money += valueEnnemy;
        GameObject effect = (GameObject)Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, 1f);
    }
}
