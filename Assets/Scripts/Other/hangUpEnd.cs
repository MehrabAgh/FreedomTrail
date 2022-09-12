using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

public class hangUpEnd : StateMachineBehaviour
{  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 0.45f)
        {
            animator.GetComponent<Rigidbody>().isKinematic = true;
            GameManager.instance.Player.transform.SetParent(GameManager.instance.HeliCopter);
            GameManager.instance.HeliCopter.GetComponent<AnimAIHelicopter>().animState = AnimAIHelicopter.HeliAnimState.EndExiting;
        }
    }
   
}
