using System;
using UnityEngine;


public class Health : MonoBehaviour
{
    public int StartHealthPoints = 100;

    public event System.Action OnHealthEnd;

    private int currentHealthPoints;
    private bool isAlive;
    
    
    private void Start()
    {
        currentHealthPoints = StartHealthPoints;
        isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive) return;
        
        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            isAlive = false;
            
            OnHealthEnd?.Invoke();
        }
    }
}