using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private int _damage = 10;
    [SerializeField] 
    private Animator _strawbatAnimator;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            //Debug.Log($"hit");
            health.Damage(_damage);
            //_strawbatAnimator.SetTrigger("Attack");

        }
    }
}
