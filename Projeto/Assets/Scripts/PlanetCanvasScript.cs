using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetCanvasScript : MonoBehaviour
{
    private Text texto;
    public GameManager gm;
    public GameObject pauseMenuUI;
    public static bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        texto = gameObject.transform.Find("Text").GetComponent<Text>();
        pauseMenuUI = gameObject.transform.Find("PauseMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
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

    public void QuitGame()
    {
        Time.timeScale = 1f;
        gm.LoadMenuScene();
    }

    public void firstLoad()
    {
        if (gm.firstLoadPlanet)
        {
            StartCoroutine(ShowText());
        }
    }


    IEnumerator ShowText()
    {
        gm.firstLoadPlanet = false;
        texto.text = "Use 'wasd' para mover";
        yield return new WaitForSeconds(7);
        texto.text = "Use 'espaco' para pular";
        yield return new WaitForSeconds(7);
        texto.text = "";

    }
}
