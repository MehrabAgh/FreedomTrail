using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        private int IndexLevel;        
        public enum LevelMode { Normal, Boss, Bonus }
        public LevelMode levelMode;

        private void Awake() { 
            instance = this;
            //Invoke(nameof(StateLevels), 5);
        }
        private void Start()
        {          
            CheckLevelMode();
            StateLevels();
        }

        private void StateLevels()
        {
            switch (levelMode)
            {
                case LevelMode.Normal:                    
                    EnemySpawner.instance.enabled = true;
                    ResourceSpawner.instance.enabled = true;
                    SetParent(GameManager.instance.Player);
                    break;
                case LevelMode.Boss:
                    ChangeStartPoint(GameManager.instance.Player);
                    EnemySpawner.instance.enabled = false;
                    ResourceSpawner.instance.enabled = false;
                    CameraManagement.instance.virtualCameras[3].SetActive(true);
                    CameraManagement.instance.virtualCameras[2].SetActive(false);
                    break;
                case LevelMode.Bonus:
                    EnemySpawner.instance.enabled = false;
                    ResourceSpawner.instance.enabled = true;
                    SetParent(GameManager.instance.Player);
                    break;
                default:
                    break;
            }
        }
        private void ChangeStartPoint(MainPlayer _player)
        {            
            _player.transform.position = GameManager.instance.PathEnds[2].position;
        }        

        private void SetParent(MainPlayer _player)
        {
            _player.transform.SetParent(GameManager.instance.HeliCopter);
        }


        private void CheckLevelMode()
        {
            IndexLevel = GetLevel();
            if (IndexLevel == 0)
            {
                IndexLevel = 1;
                PlayerPrefs.SetInt("Level", IndexLevel);
            }
            UIManager.instance.ChangeLevelText(IndexLevel.ToString());
            levelMode = LevelMode.Normal;
            if (IndexLevel > 1)
            {
                if (((IndexLevel - 1) % 5) == 0)
                {
                    UIManager.instance.ChangeLevelText("Bonus");
                    levelMode = LevelMode.Bonus;
                }
                if ((IndexLevel % 5) == 0)
                {
                    UIManager.instance.ChangeLevelText("Boss");
                    levelMode = LevelMode.Boss;                   
                }                               
            }
        }       
        public void NextLevel()
        {
            IndexLevel++;
            PlayerPrefs.SetInt("Level", IndexLevel);
        }
        public int GetLevel()
        {
            return PlayerPrefs.GetInt("Level");
        }
    }
}