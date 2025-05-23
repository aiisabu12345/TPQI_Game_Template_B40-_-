using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainLogic : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    public int GetHP() => Mathf.Max(hp, 0);
    public float GetTimeRemaining() => timer;
    public int GetScore() => score;

    public int maxHP = 5;
    public float countdownTime = 120f;

    private float timer;
    private int score = 0;
    private int hp;

    private bool isPaused = false;
    private GameObject pauseUIInstance;

    void Start()
    {
        hp = maxHP;
        timer = countdownTime;
        Time.timeScale = 1f;
    }

    void Update()
    {
        //แสดงหน้าPauseเมื่อพบว่ากด Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        //ถ้าเกมไม่ได้Pause และเวลาหมดจะGameOver
        if (!isPaused)
        {
            if (timer > 0)
            {
                //นับเวลาถอยหลัง
                timer -= Time.unscaledDeltaTime;
                timerText.text = $"Time: {timer:F1}s";
            }
            else
            {
                ShowGameOverUI();
            }
        }
    }

    private int regenHp;
    public void AddScore()
    {
        if (hp < 5)
        {
            regenHp = Random.Range(1, 101);

            if (regenHp <= 25)
            {
                GetHp();
            }
        }

        //เพิ่มscore
        score += 1;
        Debug.Log($"Score: {score}");
        scoreText.text = "Score:" + score.ToString();
    }

    public void GetDamage()
    {
        //ลดhp
        hp -= 1;
        Debug.Log($"HP: {hp}");
        hpText.text = "HP:" + hp.ToString();

        if (hp < 0)
        {
            ShowGameOverUI();
        }
    }
    
    public void GetHp()
    {
        //เพิ่มhp
        hp += 1;
        Debug.Log($"HP: {hp}");
        hpText.text = "HP:" + hp.ToString();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        //ถ้าPauseเกมอยู่ จะหยุดเกมและขึ้นหน้า Pause
        //ถ้าไม่ได้Pause จะเล่นเกมต่อและลบหน้า Pause
        if (isPaused)
        {
            Time.timeScale = 0f;

            GameObject pauseUIPrefab = Resources.Load<GameObject>("UI/Pause");
            if (pauseUIPrefab != null)
            {
                pauseUIInstance = Instantiate(pauseUIPrefab);
            }
            else
            {
                Debug.LogWarning("ไม่พบ Pause UI ใน Resources/UI/Pause");
            }
        }
        else
        {
            Time.timeScale = 1f;

            if (pauseUIInstance != null)
            {
                Destroy(pauseUIInstance);
            }
        }
    }

    private void ShowGameOverUI()
    {
        //หยุดเกมและขึ้นหน้า GameOver
        Time.timeScale = 0f;

        GameObject goUI = Resources.Load<GameObject>("UI/GameOver");
        if (goUI != null)
        {
            Instantiate(goUI);
        }
        else
        {
            Debug.LogWarning("ไม่พบ GameOver UI ใน Resources/UI/GameOver");
        }

        enabled = false;
    }
}
