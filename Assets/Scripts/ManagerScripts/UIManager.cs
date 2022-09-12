using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Vino.Devs
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public Button StartGame;
        #region Standard Events

        private void Awake() => instance = this;

        private void Start()
        {
            StartGame.onClick.AddListener(StartGameEvent);
        }

        #endregion

        #region Custom Events
        private void StartGameEvent() => GameManager.instance.gameState = GameManager.GameState.LoopGame;
        #endregion
    }
}
