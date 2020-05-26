using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceCanvasScript : MonoBehaviour
{
    private TextMeshProUGUI texto;
    public static bool paused = false;
    public static bool gameOvered = false;
    public GameObject pauseMenuUI;
    public GameObject GameOverMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        texto = gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
        {
            if (!gameOvered)
            {
                if (paused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void GameOver()
    {
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameOvered = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SpaceScene");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
    
    IEnumerator ShowText()
    {
        texto.text = "Use 'wasd' para mover a camera";
        yield return new WaitForSeconds(7);
        texto.text = "Use 'i' para mover para frente e 'k' para mover para tras";
        yield return new WaitForSeconds(10);
        texto.text = "Use 'espaco' para parar";
        yield return new WaitForSeconds(7);
        texto.text = "Cuidado com os asteroides! Use 'j' para atirar";
        yield return new WaitForSeconds(10);
        texto.text = "Agora va para o planeta mais proximo";
        yield return new WaitForSeconds(5);
        texto.text = "";
    }
}
