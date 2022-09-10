using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Vino.Devs
{
    [RequireComponent(typeof(launchingProjectiles))]
    [RequireComponent(typeof(AmmoPooling))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        // 
        public MainPlayer Player;
        public Transform HeliCopter;
        //
        public bool _isEndGame, _isStartGame;
        public Transform StartPos, EndPos;
        //
        public float Xp;
        //

        private void Awake()
        {
            instance = this;
            _isStartGame = true;
            _isEndGame = false;
            Player = FindObjectOfType<MainPlayer>();
            var animHeli = HeliCopter.GetComponent<AnimAIHelicopter>();
            animHeli.animState = AnimAIHelicopter.HeliAnimState.StartJumper;            
        }

        private void Start()
        {
            Invoke("StartGame", 2);
        }

        private void Update()
        {
            if (_isEndGame)
            {
                EndGame();
            }
        }
            

        private void StartGame()
        {
            Player.StartGame();
        }
        public void EndGame()
        {
            HeliCopter.GetComponent<AnimAIHelicopter>()
                .animState = AnimAIHelicopter.HeliAnimState.EndClimber;
            CameraManagement.instance.virtualCameras[0].SetActive(false);
            CameraManagement.instance.virtualCameras[1].SetActive(true);
            Player.GetComponent<LookAtCharacter>().target = HeliCopter;
            Player.transform.rotation = Quaternion.Euler(0, 180, 0);
            //launchingProjectiles.ins.TargetObject.position = HeliCopter.transform.GetChild(1).position;
            Player.Anim.SetBool("isCover", false);            
        }
    }
}