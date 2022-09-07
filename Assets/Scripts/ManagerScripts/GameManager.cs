using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MainPlayer Player;
    public bool _isEndGame;    
    private void Awake()
    {
        instance = this;
        _isEndGame = true;
        Player = FindObjectOfType<MainPlayer>();
    }
    private void Start()
    {
        StartGame();    
    }    
    public void StartGame()
    {
        _isEndGame = false;
    }
    public void EndGame()
    {
        _isEndGame = true;
    }   
}
