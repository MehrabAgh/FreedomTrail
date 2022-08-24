using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public GameObject Player;
    public bool _isEndGame , _isPause;
    public List<CarAIControl> Cars;
    public EnemyCarTargetController[] Enemys;
    public GameObject UIGame, UIFinish, UIOver;
    private void Awake()
    {
        ins = this;
        _isEndGame = true;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        StartGame();
        Cars.Add(FindObjectOfType<CarAIControl>());        
    }
    public void Play()
    {       
        _isEndGame = false;
        EnableCar();
    }
    public void ResetLevel()
    {
        LevelManager.instance.indexDelayShoot = 2.6f;
        LevelManager.instance.EnemyDelayEdit();
        LevelManager.instance.indexLevel = 1;
        PlayerPrefs.SetInt("IndexLevel", 1);
        LevelManager.instance.nameLevel = "Level" + LevelManager.instance.indexLevel;
        PlayerPrefs.SetString("Level", LevelManager.instance.nameLevel);
        SceneManager.LoadScene("SampleScene");
    }
    public void EnableCar()
    {
        foreach (var item in Cars)
        {
            item.enabled = true;
        }
    }
    public void DisableCar()
    {
        foreach (var item in Cars)
        {
            item.enabled = false;
        }
    }

    public void StartGame()
    {
        _isPause = false;
    }
    public void EndGame()
    {
        _isPause = true;
    }
    private void Update()
    {             
        if (_isEndGame)
        {
            //Gameover
            // PlayerPrefs.SetInt("Coin", ScoreManager.instance.Coin);
            // Time.timeScale = 0;
            DisableCar();            
        }
        else
        {            
            Time.timeScale = 1;
        }
        Enemys = FindObjectsOfType<EnemyCarTargetController>();
    }
}
