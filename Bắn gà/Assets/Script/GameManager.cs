using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int enemiesKilled = 0;

    [SerializeField] private Text killCountText;

    // Event cho enemy bị tiêu diệt
    public event Action OnEnemyKilled;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        OnEnemyKilled += UpdateKillCount;
    }

    private void OnDestroy()
    {
        OnEnemyKilled -= UpdateKillCount; // Hủy đăng ký event khi GameManager bị destroy
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        OnEnemyKilled?.Invoke(); // Kích hoạt event
    }

    private void UpdateKillCount()
    {
        killCountText.text = ": " + enemiesKilled.ToString();
        //Debug.Log("Enemies Killed: " + enemiesKilled);
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }
}
