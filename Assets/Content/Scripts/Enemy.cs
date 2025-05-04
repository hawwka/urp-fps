using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();

        health.OnHealthEnd += Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}