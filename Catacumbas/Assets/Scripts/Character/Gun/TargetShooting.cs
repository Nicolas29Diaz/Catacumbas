using UnityEngine;

public class TargetShooting : MonoBehaviour
{
    public float health = 50f;
    public void TakeDamage(float amount)
    {
        health -= amount; 
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
