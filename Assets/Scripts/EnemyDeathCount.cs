using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeathCount : MonoBehaviour
{
    public static int KillCount = 0;
    [SerializeField] 
    private int _killGoal = 100;
    [SerializeField] 
    private GameObject _winMessage;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) KillCount++;
        if (KillCount >= _killGoal)
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
