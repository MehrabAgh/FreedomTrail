using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
namespace Vino.Devs
{
    public class AnimAIHelicopter : MonoBehaviour
    {
        public enum HeliAnimState
        {
            Idle,
            StartJumper,
            StartExiting,
            EndClimber,
            EndExiting
        }
        public HeliAnimState animState;
        [SerializeField] private Vector3 offset;
        [SerializeField] private List<ModelAnim> modelAnims;
        private bool aa;        
        private void FixedUpdate()
        {
            if(LevelManager.instance.levelMode != LevelManager.LevelMode.Boss) {
                switch (animState)
                {
                    case HeliAnimState.Idle:
                        transform.position = Vector3.Lerp(transform.position, modelAnims[4].Positions[0], Time.deltaTime * modelAnims[0].speed);
                        break;
                    case HeliAnimState.StartJumper:
                        transform.position = Vector3.Lerp(transform.position, modelAnims[0].Positions[0], Time.deltaTime * modelAnims[0].speed);
                        break;
                    case HeliAnimState.StartExiting:
                        transform.position = Vector3.Lerp(transform.position, modelAnims[1].Positions[0], Time.deltaTime * modelAnims[1].speed);
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(modelAnims[1].Rotations[0]), Time.deltaTime * modelAnims[1].speed);
                        break;
                    case HeliAnimState.EndClimber:
                        transform.position = Vector3.Lerp(transform.position, GameManager.instance.Player.transform.parent.position + offset, Time.deltaTime * modelAnims[2].speed);
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(modelAnims[2].Rotations[0]), Time.deltaTime * modelAnims[2].speed);
                        var dis = Vector3.Distance(transform.position, GameManager.instance.Player.transform.parent.position);
                        //part of end event
                        if (dis < 4.6f)
                        {
                            GameManager.instance.Player.Anim.SetBool("EndCinematic", true);
                            if (!aa)
                            {
                                launchingProjectiles.ins.Launch();
                                aa = true;
                            }

                        }
                        break;
                    case HeliAnimState.EndExiting:

                        transform.position = Vector3.Lerp(transform.position, GameManager.instance.EndPos.position + modelAnims[3].Positions[0], Time.deltaTime * modelAnims[3].speed);
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(modelAnims[3].Rotations[0]), Time.deltaTime * (modelAnims[3].speed + 0.5f));
                        //end Animate Camera
                        CameraManagement.instance.virtualCameras[1].GetComponent<CinemachineVirtualCamera>().Follow = null;
                        CameraManagement.instance.virtualCameras[1].GetComponent<CinemachineVirtualCamera>().LookAt = null;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    [Serializable]
    public class ModelAnim
    {
        public List<Vector3> Positions, Rotations;
        public bool IsCheck;
        public float speed;
    }
}