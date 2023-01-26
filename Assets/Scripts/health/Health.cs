using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
    currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
    currentHealth -= _damage;

    if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");

        }
        else
        {
            anim.SetTrigger("die");
            GetComponent<PlayerController>().enabled = false;
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void Respawn()
    {
        GetComponent<PlayerController>().enabled = true;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
    }
}

