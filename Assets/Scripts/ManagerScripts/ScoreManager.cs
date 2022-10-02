using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Vino.Devs
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
       [HideInInspector] public int CurrCoin, CurrKill;
       [HideInInspector] public float CurrXp;
        public float maxEasy, maxNormal, maxHard;
        public ResourceStorage storage;

        public int Coin
        {
            get { return storage._coin; }
            set
            {
                storage._coin = value;
                PlayerPrefs.SetInt("Coin_", storage._coin);
            }
        }
        public int Key
        {
            get { return storage._key; }
            set
            {
                storage._key = value;
                PlayerPrefs.SetInt("Key_", storage._key);
            }
        }
        public float Xp
        {
            get { return storage._xp; }
            set
            {
                storage._xp = value;
                PlayerPrefs.SetFloat("XP_", storage._xp);
            }
        }

        public int KillCounter(int count)
        {
            return CurrKill += count;
        }
        public float XpEnder(float count)
        {
            return CurrXp += count;
        }
        public void SaveResource() => Coin += CurrCoin;
        public void XpCollectsEnd()
        {
            Xp += CurrKill *0.5f;
            Xp += 0.25f;           
        }
        public void XpUpdate()
        {
            if (Xp <= maxEasy)
            {
                UIManager.instance.XPUiUpdated(maxEasy);
                UIManager.instance.ChangeStateLevel("Easy");
            }
            else if (Xp > maxEasy && Xp <= maxNormal)
            {
                UIManager.instance.LevelUpMenu.gameObject.SetActive(true);
                StartCoroutine(UIManager.instance.LevelUpAnimShower(1f));
                UIManager.instance.XPUiUpdated(maxNormal);
                UIManager.instance.ChangeStateLevel("Normal");
            }
            else if (Xp > maxNormal && Xp <= maxHard)
            {
                UIManager.instance.LevelUpMenu.gameObject.SetActive(true);
                StartCoroutine(UIManager.instance.LevelUpAnimShower(1));
                UIManager.instance.XPUiUpdated(maxHard);
                UIManager.instance.ChangeStateLevel("Hard");
            }
        }

        private void Awake() => instance = this;
        private void Start()
        {
            Xp = PlayerPrefs.GetFloat("XP_");
            Coin = PlayerPrefs.GetInt("Coin_");
            Key = PlayerPrefs.GetInt("Key_");
            XpUpdate();
           
        }
    }
}