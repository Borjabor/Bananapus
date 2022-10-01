using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeathCount : MonoBehaviour
{
    public static int KillCount = 0;
    [SerializeField] 
    private int _killGoal = 10;
    [SerializeField] 
    private GameObject _winMessage;
    
    
    void Start()
    {
        
    }
    void Update()
    {
        if (KillCount >= _killGoal || Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Victory());
        }
    }

    private IEnumerator Victory()
    {
        Time.timeScale = 0f;
        _winMessage.SetActive(true);
        yield return StartCoroutine(WaitForRealSeconds(4f));
        SceneManager.LoadScene("EndScreen");

    }
    
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
