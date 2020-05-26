using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour
{
    GameObject porta;
    GameObject player;
    GameObject spawn;
    GameObject canvas;
    GameObject soundGO;
    string spawnName = "";

    public bool playerRunPowerUp = false;
    public bool firstLoadPlanet = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        GameObject[] rootGOs = SceneManager.GetActiveScene().GetRootGameObjects();
        for (int i = 0; i < rootGOs.Length; i++)
        {
            if (rootGOs[i].name == "Canvas")
            {
                canvas = rootGOs[i];
            }
            else if (rootGOs[i].name == "BGM")
            {
                soundGO = rootGOs[i];
            }
        }
        canvas.GetComponent<PlanetCanvasScript>().gm = gameObject.GetComponent<GameManager>();
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(soundGO);
        StartCoroutine(LoadLevel("PlanetScene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTempleScene(string n)
    {
        spawnName = n;
        StartCoroutine(LoadLevel("TempleScene"));
    }

    public void LoadPlanetScene(string n)
    {
        spawnName = n;
        StartCoroutine(LoadLevel("PlanetScene"));
    }

    public void LoadWellScene(string n)
    {
        spawnName = n;
        StartCoroutine(LoadLevel("WellScene"));
    }

    public void LoadMenuScene()
    {
        Destroy(canvas);
        StartCoroutine(LoadLevel("MenuScene"));
    }

    IEnumerator LoadLevel(string name)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(name);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        canvas.transform.Find("Text").GetComponent<Text>().text = "";

        if (name == "TempleScene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerScript>().gm = gameObject.GetComponent<GameManager>();
            player.GetComponent<PlayerScript>().texto = canvas.transform.Find("Text").GetComponent<Text>();
        }
        else if(name == "PlanetScene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerScript>().gm = gameObject.GetComponent<GameManager>();
            player.GetComponent<PlayerScript>().texto = canvas.transform.Find("Text").GetComponent<Text>();
            canvas.GetComponent<PlanetCanvasScript>().firstLoad();
            GameObject[] rootGOs = SceneManager.GetSceneByName(name).GetRootGameObjects();
            for (int i = 0; i < rootGOs.Length; i++)
            {
                if (spawnName != "")
                {
                    if (rootGOs[i].name == spawnName)
                    {
                        spawnName = "";
                        player.transform.position = rootGOs[i].transform.position;
                    }
                }   
            }
        }
        else if (name == "WellScene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerScript>().gm = gameObject.GetComponent<GameManager>();
            player.GetComponent<PlayerScript>().texto = canvas.transform.Find("Text").GetComponent<Text>();
        }
        else if(name == "MenuScene")
        {
            Destroy(gameObject);
        }
        
    }

    public void changeCanvasText()
    {
        canvas.transform.Find("PauseMenu").Find("Controls right").GetComponent<TextMeshProUGUI>().text = "Jump: Space\nSprint: Shift\n\n\n";
    }
}
