using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Vino.Devs
{
    [System.Serializable]
    public struct CharacterResource
    {
        public List<Mesh> Characters;
        public List<Material> StandardMat;
        public Transform DefaultCharacter, ProCharacter;
        public int Index;
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        // 
        public MainPlayer Player;
        public Transform HeliCopter;
        public Transform EndPos;
        public CharacterResource CResource;
        [HideInInspector]public int indexLogin;
        //
        public enum GameState
        {
            IsMenu,
            LoopGame,
            EndGame
        }
        public GameState gameState;

        public bool _isEndGame, _isStartGame, _isMenu;
        public bool OnRealGame;
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
        public EnemyCar[] StarterEnemyCars;

        private void Awake()
        {
            instance = this;
            _isStartGame = false;
            _isEndGame = false;
            CheckLoadPlayer();
            OnGameOver += EndingGame;
            gameState = GameState.IsMenu;
            indexLogin++;
            PlayerPrefs.SetInt("Login", indexLogin);
        }
        private void Start() => ChangePLayerForAll();
        private void Update() => StateAdmin();

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
                    _isStartGame = false;
                    _isEndGame = true;
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
        #region HelperMethod
        public float GetDistanceToEnd() {
            if (Player != null)
                return Vector3.Distance(Player.transform.position, EndPos.position);
            else return 0;
        }
        public void ChangePLayerForAll()
        {
            launchingProjectiles.ins.ThisObject = Player.transform;
            CameraManagement.instance.virtualCameras[1]
                .GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = Player.transform;
            CameraManagement.instance.virtualCameras[2]
                .GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = Player.transform;
        }
        private void CheckLoadPlayer()
        {
            var p = PlayerPrefs.GetInt("Player");
            if (p < 1)
            {
                CResource.DefaultCharacter.gameObject.SetActive(true);
                CResource.ProCharacter.gameObject.SetActive(false);
                Player = FindObjectOfType<MainPlayer>();
                Player.mymodel.material = CResource.StandardMat[PlayerPrefs.GetInt("PlayerIndex")];
            }
            else
            {
                CResource.ProCharacter.gameObject.SetActive(true);
                CResource.DefaultCharacter.gameObject.SetActive(false);
                Player = FindObjectOfType<MainPlayer>();
                Player.mymodel.sharedMesh = CResource.Characters[PlayerPrefs.GetInt("PlayerIndex")];
            }
        }
        #endregion
        private void StartGame()
        {
            CameraManagement.instance.virtualCameras[0].SetActive(true);
            CameraManagement.instance.virtualCameras[2].SetActive(false);
            OnRealGame = true;
            Player.Anim.Play("Flip");
            Player.StartGame();
        }
        private void EndingGame()
        {
            gameState = GameState.EndGame;
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