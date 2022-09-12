using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Vino.Devs
{
<<<<<<< HEAD
    public static GameManager ins;
    public GameObject Player;
    public bool _isEndGame , _isPause;
//    public List<CarAIControl> Cars;
//    public EnemyCarTargetController[] Enemys;
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
  //      Cars.Add(FindObjectOfType<CarAIControl>());        
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
    {/*
        foreach (var item in Cars)
=======
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        // 
        public MainPlayer Player;
        public Transform HeliCopter;
        public Transform EndPos;
        //
        public enum GameState
>>>>>>> Mehrab_Branch
        {
            IsMenu ,
            LoopGame ,
            EndGame
        }
<<<<<<< HEAD
   */ }
    public void DisableCar()
    {
     /*   foreach (var item in Cars)
=======
        public GameState gameState;

        public bool _isEndGame , _isStartGame , _isMenu;
        private bool startingWork;
        //
        public delegate void EndGame();
        public event EndGame OnGameOver;
        public bool IsGameOver
>>>>>>> Mehrab_Branch
        {
            set
            {
                _isEndGame = value;
                if (_isEndGame & OnGameOver != null)
                {
                    OnGameOver();
                }
            }
            get
            {
                return _isEndGame;
            }
        }
<<<<<<< HEAD
    */}
=======
>>>>>>> Mehrab_Branch


        private void Awake()
        {
            instance = this;
            _isStartGame = false;
            _isEndGame = false;
            Player = FindObjectOfType<MainPlayer>();
            OnGameOver += EndingGame;
        }

        private void Update()
        {
            StateAdmin();
            if (CheckInEndingGame()) IsGameOver = true;
        }

        private void StateAdmin()
        {
            switch (gameState)
            {
                case GameState.IsMenu:
                    _isMenu = true;
                    _isStartGame = false;
                    break;
                case GameState.LoopGame:
                    _isStartGame = true;
                    _isMenu = false;

                    if (!startingWork)
                    {
                        var animHeli = HeliCopter.GetComponent<AnimAIHelicopter>();
                        animHeli.animState = AnimAIHelicopter.HeliAnimState.StartJumper;
                        Invoke(nameof(StartGame), 2);
                        startingWork = true;
                    }
                    break;
                case GameState.EndGame:                    
                    IsGameOver = true;
                    break;
                default:
                    break;
            }
        }
        #region Check State
        public bool CheckInMenu()
        { return _isMenu && !_isStartGame && !_isEndGame; }

        public bool CheckInLoopGame()
        { return !_isMenu && _isStartGame && !_isEndGame; }

        public bool CheckInEndingGame()
        { return !_isMenu && !_isStartGame && _isEndGame; }      
        #endregion
        private void StartGame()
        {
            Player.Anim.Play("Flip");
            Player.StartGame();
        }
        private void EndingGame()
        {
            HeliCopter.GetComponent<AnimAIHelicopter>()
                .animState = AnimAIHelicopter.HeliAnimState.EndClimber;
            CameraManagement.instance.virtualCameras[0].SetActive(false);
            CameraManagement.instance.virtualCameras[1].SetActive(true);
            Player.GetComponent<LookAtCharacter>().target = HeliCopter;
            Player.transform.rotation = Quaternion.Euler(0, 180, 0);
            Player.Anim.SetBool("isCover", false);
            Player.Anim.SetLayerWeight(1, 0);
            //other part this event into hangUpEnd.cs & AnimAIHelicopter
        }
<<<<<<< HEAD
//        Enemys = FindObjectsOfType<EnemyCarTargetController>();
=======
>>>>>>> Mehrab_Branch
    }
}