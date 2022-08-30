using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class PlayerState
    {
        public enum playerState
        {
            IDLE,
            ATTACK,
            COVER,
            DEATH,
            RELOAD
        }
        public static playerState myState { get; set; }

        public void EventStates(playerState state)
        {
            switch (state)
            {
                case playerState.IDLE:
                    break;
                case playerState.ATTACK:
                    break;
                case playerState.COVER:
                    break;
                case playerState.DEATH:
                    break;
                case playerState.RELOAD:
                    break;
                default:
                    break;
            }
        }
        public playerState GetState()
        {
            return myState;
        }
    }
}
