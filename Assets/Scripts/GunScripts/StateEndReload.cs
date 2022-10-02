using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

public class StateEndReload : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<MainEnemyAI>())
            animator.GetComponent<MainEnemyAI>().SetReload();
        else if (animator.GetComponent<MainPlayer>())
            animator.GetComponent<MainPlayer>().SetReload();
    }
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if (animator.GetComponent<MainEnemyAI>()) animator.GetComponent<MainEnemyAI>().gun.ReloadGun();
    //    else if (animator.GetComponent<MainPlayer>()) animator.GetComponent<MainPlayer>().gun.ReloadGun();

    //    animator.SetBool("isReload", false);
    //    animator.SetLayerWeight(layerIndex, 0);
    //}
}
