using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private int _killThreshold = 20;
    
    void Update()
    {
        if (EnemyDeathCount.KillCount >= _killThreshold)
        {
            Destroy(gameObject);
        }
    }
}
