using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public enum playerState
        {
            idleState,
            runState,
            jumpState,
            fallState,
            attackState,
            hitState,

        }

        public playerState currState;
        public void SetCurrState(playerState newState, Animator animator)
        {
            currState = newState;
            Debug.Log(currState);
            switch (currState)
            {
                case playerState.idleState:
                    animator.SetInteger("state", 0);
                    break;
                case playerState.runState:
                    animator.SetInteger("state", 1);
                    break;
                case playerState.jumpState:
                    animator.SetInteger("state", 2);
                    break;
                case playerState.attackState:
                    animator.SetInteger("state", 4);
                    break;

            }

        }

        public playerState GetCurrState()
        {
            return currState;
        }
    }
}
