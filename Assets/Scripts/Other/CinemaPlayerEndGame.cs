using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class CinemaPlayerEndGame : MonoBehaviour
    {
        public void SetPositionPlayer(MainPlayer player, Vector3 positions)
        {
            player.transform.LookAt(positions);
            player.transform.position = Vector3.Lerp(player.transform.position
                , positions, Time.deltaTime*2);           
        }

        public Transform ChangeTarget(List<Transform> TransPath)
        {
            foreach (var item in TransPath)
            {
                if(item.gameObject.activeSelf && item != null)
                    return item;
            }
            return null;
        }

        public int CheckEndMove(Transform path , MainPlayer player)
        {
            var Dist = Vector3.Distance(player.transform.position, path.position);            
            if(Dist < .4f) return 1;
            if (Dist > .4f) return 0;
            return -1;
        }

        private Transform CurrentPath;
        private int Status;
        private void Update()
        {            
            if (GameManager.instance.IsGameOver && GameManager.instance.endState == GameManager.EndState.TakeOff)
            {
                if(CurrentPath is null && Status is 0)
                    CurrentPath = ChangeTarget(GameManager.instance.PathEnds);

                if (CurrentPath != null)
                {
                    SetPositionPlayer(GameManager.instance.Player, CurrentPath.position);

                    Status = CheckEndMove(CurrentPath, GameManager.instance.Player);

                    if (Status is 1)
                    {
                        CurrentPath.gameObject.SetActive(false);
                        CurrentPath = ChangeTarget(GameManager.instance.PathEnds);
                    }

                    if (CurrentPath == GameManager.instance.PathEnds[^1] && Status is 1)
                    {
                        GameManager.instance._isEndGame = false;
                        GameManager.instance.Player.enabled = true;
                        GameManager.instance.Player.transform.SetParent(null);
                        GameManager.instance.Player.Anim.SetBool("CinemaEndRun", true);
                        GameManager.instance.Player.myCar.GetComponent<Animator>().SetBool("isAccess", false);
                    }
                }

            }
        }
    }
}