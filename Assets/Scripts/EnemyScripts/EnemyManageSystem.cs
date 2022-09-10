using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs {
    public class EnemyManageSystem
    {
        public enum EnemyLevelState
        {
            Easy,
            Normal,
            Hard
        }
        public EnemyLevelState enemyLevel;
        private MainEnemyAI myenem;
        public EnemyManageSystem(MainEnemyAI my)
        {
            myenem = my;
            SettingUpEnemy(5, 10, 20);
            ManagementSystem();
        }
        public void SettingUpEnemy(float maxEasy, float maxNormal, float maxHard)
        {
            if (GameManager.instance.Xp <= maxEasy)
            {
                enemyLevel = EnemyLevelState.Easy;
                Debug.Log(enemyLevel);
            }
            else if (GameManager.instance.Xp > maxEasy && GameManager.instance.Xp <= maxNormal)
            {
                enemyLevel = (EnemyLevelState)Random.Range((int)EnemyLevelState.Easy, (int)EnemyLevelState.Normal);
                Debug.Log(enemyLevel);
            }
            else if (GameManager.instance.Xp > maxNormal && GameManager.instance.Xp <= maxHard)
            {
                enemyLevel = (EnemyLevelState)Random.Range((int)EnemyLevelState.Easy, (int)EnemyLevelState.Hard);                
            }
        }

        public void ManagementSystem()
        {
            switch (enemyLevel)
            {
                case EnemyLevelState.Easy:
                    myenem.gun.CodeGun = 0;
                    break;
                case EnemyLevelState.Normal:
                    myenem.gun.CodeGun = 1;
                    break;
                case EnemyLevelState.Hard:
                    myenem.gun.CodeGun = Random.Range(2 , 3);
                    break;
                default:
                    break;
            }
        }
    }

}