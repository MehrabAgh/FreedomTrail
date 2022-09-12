using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Vino.Devs
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        // 
        public MainPlayer Player;
        public Transform HeliCopter;
        public Transform EndPos;
        //
        public enum GameState
        {
            IsMenu ,
            LoopGame ,
            EndGame
        }
        public GameState gameState;

        public bool _isEndGame , _isStartGame , _isMenu;
        private bool startingWork;
        //
        public delegate void EndGame();
        public event EndGame OnGameOver;
        public bool IsGameOver
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
            Player.transform.rotation = Quaternion.Euler(0, 180, 0);
            Player.Anim.SetBool("isCover", false);
            Player.Anim.SetLayerWeight(1, 0);
            Player.Anim.SetLayerWeight(2, 0);
            Player.enabled = false;
            //other part this event into hangUpEnd.cs & AnimAIHelicopter
        }
    }
}