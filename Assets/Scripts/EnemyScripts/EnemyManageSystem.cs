using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class EnemyManageSystem
    {
        public enum EnemyLevelState
        {
            Easy,
            Normal,
            Hard
        }
        public EnemyLevelState enemyLevel;
        public enum EnemyType
        {
            general,
            boss
        }
        public EnemyType enemyType;
        public int SelectModeEnemy()
        {
            if (enemyType == EnemyType.general)
            {
                SettingUpEnemy(30, 60, 90);
                return ManagementSystem();
            }
            else
            {
                var x = Random.Range(1, 3);
                if ((x % 2) == 0)
                    return 0;
                else return Random.Range(2 , 3);
            }
        }

        private void SettingUpEnemy(float maxEasy, float maxNormal, float maxHard)
        {
            if (ScoreManager.instance.Xp <= maxEasy) enemyLevel = EnemyLevelState.Easy;
            else if (ScoreManager.instance.Xp > maxEasy && ScoreManager.instance.Xp <= maxNormal)
                enemyLevel = (EnemyLevelState)Random.Range((int)EnemyLevelState.Easy, (int)EnemyLevelState.Normal);
            else if (ScoreManager.instance.Xp > maxNormal && ScoreManager.instance.Xp <= maxHard)
                enemyLevel = (EnemyLevelState)Random.Range((int)EnemyLevelState.Easy, (int)EnemyLevelState.Hard);
        }

        private int ManagementSystem()
        {
            return enemyLevel switch
            {
                EnemyLevelState.Easy => 0,
                EnemyLevelState.Normal => 1,
                EnemyLevelState.Hard => Random.Range(2, 3),
                _ => 0,
            };
        }
    }

}