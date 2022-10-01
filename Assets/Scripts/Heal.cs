using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private int _heal = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Health health = collider.GetComponent<Health>();
            //Debug.Log($"hit");
            if (health._health < health.MaxHealth)
            {
                health.Heal(_heal);
                Destroy(gameObject);
            }
        }
    }
}
