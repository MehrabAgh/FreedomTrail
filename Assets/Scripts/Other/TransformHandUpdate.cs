using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

public class TransformHandUpdate : StateMachineBehaviour
{
    public List<Vector3> PositionsLeft, PositionsRight;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var character = animator.GetComponent<CharacterMain>();
        if (character.gameObject.CompareTag("Player"))
        {
            var isPlayer = animator.GetComponent<MainPlayer>()._isPro;
            if (!isPlayer)
            {
                character.HandLeft.localPosition = PositionsLeft[0];
                character.HandRight.localPosition = PositionsRight[0];
            }
            else if (isPlayer)
            {
                character.HandLeft.localPosition = PositionsLeft[1];
                character.HandRight.localPosition = PositionsRight[1];
            }
        }
        else
        {
            if (character)
            {
                character.HandLeft.localPosition = PositionsLeft[0];
                character.HandRight.localPosition = PositionsRight[0];
            }
        }
             
    }

}
