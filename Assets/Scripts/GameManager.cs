using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;
    public Text gameOverText;
    public Image fadeImage;
    public Canvas mainMenu;
    private Blade blade;
    private Spawner spawner;
    private int score;
    private bool gameOver = false;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindAnyObjectByType<Spawner>();
        blade.enabled = false;
        spawner.enabled = false;
        UpdateHiscore();
    }
    // private void Start()
    // {
    //     NewGame();
    // }
    public void NewGame()
    {
        mainMenu.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);

        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();
        ClearScene();
        if(gameOver)
        {
            StartCoroutine(DeFadeSequence());
            gameOver = false;
        } 
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;       

        StartCoroutine(FadeSequence());
    }

    public void GameOverScreen()
    {
        UpdateHiscore();
        gameOver = true;
        mainMenu.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator FadeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        GameOverScreen();   
    }

    private IEnumerator DeFadeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiScoreText.text = Mathf.FloorToInt(hiscore).ToString();
    }
}
