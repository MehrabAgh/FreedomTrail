using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class AnimAITruck : StateMachineBehaviour
    {
        private bool Ex;        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("Drift"))
            {
                if (stateInfo.normalizedTime > .999 && !Ex)
                {
                    launchingProjectiles.ins.TargetObject = GameManager.instance.PathEnds[0];
                    launchingProjectiles.ins.Launch();
                    Ex = true;
                }
            }                        

        }
    }
}
