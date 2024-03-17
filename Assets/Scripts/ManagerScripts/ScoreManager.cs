using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
namespace Vino.Devs
{
    [Serializable]
    public class DATA
    {
        public bool _isLevelUpped;
    }

    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
       [HideInInspector] public int CurrCoin, CurrKill;
       [HideInInspector] public float CurrXp;
        public float maxEasy, maxNormal, maxHard;
        public ResourceStorage storage;
        public int _isLevelUpped;
        //private DATA GetDATA;
        //private FileStream GetFile;
        //private BinaryFormatter GetBF;

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

        public void SaveDATA(DATA data ,FileStream file , BinaryFormatter formatter)
        {
            data._isLevelUpped = true;                    
            formatter.Serialize(file, data);
            file.Close();
        }
        public void LoadDATA(DATA data, FileStream file , BinaryFormatter formatter)
        {
            file = File.Open("save.dat", FileMode.Open);
            data = (DATA)formatter.Deserialize(file);
            file.Close();
        }
        public bool GetCheckLevelUpped(DATA data)
        {
            return data._isLevelUpped;
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
                UIManager.instance.XPUiUpdated(maxNormal);
                UIManager.instance.ChangeStateLevel("Normal");
                if (_isLevelUpped is 0)
                {
                    UIManager.instance.LevelUpMenu.gameObject.SetActive(true);
                    StartCoroutine(UIManager.instance.LevelUpAnimShower(1f));
                    PlayerPrefs.SetInt("LevelUpped" , 1);
                }
                //if (!GetCheckLevelUpped(GetDATA))
                //{
                //    UIManager.instance.LevelUpMenu.gameObject.SetActive(true);
                //    StartCoroutine(UIManager.instance.LevelUpAnimShower(1f));
                //    SaveDATA(GetDATA, GetFile, GetBF);
                //}

            }
            else if (Xp > maxNormal && Xp <= maxHard)
            {
                UIManager.instance.XPUiUpdated(maxHard);
                UIManager.instance.ChangeStateLevel("Hard");
                if (_isLevelUpped is 1)
                {
                    UIManager.instance.LevelUpMenu.gameObject.SetActive(true);
                    StartCoroutine(UIManager.instance.LevelUpAnimShower(1f));
                    PlayerPrefs.SetInt("LevelUpped", 2);
                }
                //    if (!GetCheckLevelUpped(GetDATA)) {
                //    UIManager.instance.LevelUpMenu.gameObject.SetActive(true);
                //    StartCoroutine(UIManager.instance.LevelUpAnimShower(1));
                //    SaveDATA(GetDATA, GetFile, GetBF);
                //}            
            }
        }

        private void Awake()
        {
            instance = this;
            //GetDATA = new ();
            //GetBF = new();
            //GetFile = File.Create("save.dat");
        }
        private void Start()
        {
            Xp = PlayerPrefs.GetFloat("XP_");
            Coin = PlayerPrefs.GetInt("Coin_");
            Key = PlayerPrefs.GetInt("Key_");

            _isLevelUpped = PlayerPrefs.GetInt("LevelUpped");
            XpUpdate();
            //LoadDATA(GetDATA, GetFile, GetBF);
        }
    }
}