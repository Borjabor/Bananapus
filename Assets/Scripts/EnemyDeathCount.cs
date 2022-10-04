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
    [SerializeField]
    private float _endDelay = 4f;

    private void Start()
    {
        KillCount = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) KillCount++;
        if (KillCount >= _killGoal)
        {
            StartCoroutine(Victory());
        }

        //if (Input.GetKeyDown(KeyCode.Alpha2)) KillCount += 10;
    }

    private IEnumerator Victory()
    {
        Time.timeScale = 0f;
        _winMessage.SetActive(true);
        yield return StartCoroutine(WaitForRealSeconds(_endDelay));
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
