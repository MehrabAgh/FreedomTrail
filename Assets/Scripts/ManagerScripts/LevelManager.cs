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

        private void Awake() => instance = this;
        private void Start() => CheckLevelMode();

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

            if ((IndexLevel % 5) == 0)
            {
                UIManager.instance.ChangeLevelText("Boss");
                levelMode = LevelMode.Boss;
            }
            else if ((IndexLevel % 5) == 0)
            {
                UIManager.instance.ChangeLevelText("Bonus");
                levelMode = LevelMode.Bonus;
            }
        }
        private void SettingupLevel()
        {
            switch (levelMode)
            {
                case LevelMode.Normal:
                    break;
                case LevelMode.Boss:
                    break;
                case LevelMode.Bonus:
                    break;
                default:
                    break;
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