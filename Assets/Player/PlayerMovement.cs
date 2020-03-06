using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Player;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public int maxMspd = 10;
        public int mspdAcc = 8;
        public float currMspd = 0;
        public float gravity;
        public float vertSpd;
        public float jumpSpd;
        public int maxFallSpd;
        float inputX;
        private int currDirection = 1;

        CharacterController controller;
        Animator animator;

        Rigidbody playerRb;
        AttackController attackController;

        public PlayerStateMachine fsm = new PlayerStateMachine();

        void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            attackController = GetComponent<AttackController>();
            playerRb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            GetInput();
            if (controller.isGrounded)
            {
                if (vertSpd != -1)
                {
                    SetCurrState(PlayerStateMachine.playerState.idleState);
                    vertSpd = -1;
                }
            }
            else
            {
                vertSpd -= gravity;
                controller.Move(new Vector3(currMspd * currDirection * Time.deltaTime, vertSpd * Time.deltaTime, 0));
            }
            // if (!controller.isGrounded)
            // {
            // }
            // if (fsm.currState == PlayerStateMachine.playerState.jumpState)
            // {
            //     vertSpd -= gravity;
            //     // if (vertSpd < maxFallSpd) vertSpd = maxFallSpd;
            //     controller.Move(new Vector3(currMspd * currDirection * Time.deltaTime, vertSpd * Time.deltaTime, 0));

            // }
        }

        void GetInput()
        {
            // Move
            inputX = Input.GetAxis("Horizontal");
            if (inputX != 0)
            {
                if (
                    fsm.currState != PlayerStateMachine.playerState.runState &&
                    fsm.currState != PlayerStateMachine.playerState.jumpState &&
                    fsm.currState != PlayerStateMachine.playerState.attackState
                )
                {
                    SetCurrState(PlayerStateMachine.playerState.runState);
                }
                if (fsm.currState == PlayerStateMachine.playerState.runState)
                {
                    currMspd += mspdAcc;
                    if (currMspd > maxMspd) currMspd = maxMspd;
                    if (inputX > 0)
                    {
                        currDirection = 1;
                    }
                    else
                    {
                        currDirection = -1;
                    }
                    transform.rotation = Quaternion.Euler(0, 90 * currDirection, 0);

                }
            }
            else
            {
                currMspd = 0;
                if (
                    fsm.currState == PlayerStateMachine.playerState.runState
                )
                {
                    SetCurrState(PlayerStateMachine.playerState.idleState);
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (
                    fsm.currState == PlayerStateMachine.playerState.idleState ||
                    fsm.currState == PlayerStateMachine.playerState.runState
                )
                {
                    vertSpd = jumpSpd;
                    controller.Move(new Vector3(currMspd * currDirection * Time.deltaTime, vertSpd * Time.deltaTime, 0));
                    Jump();
                }
            }
            // Attack
            if (
                Input.GetMouseButton(0) &&
                fsm.currState != PlayerStateMachine.playerState.attackState &&
                fsm.currState != PlayerStateMachine.playerState.jumpState
            )
            {
                Attack();
            }
            // if (inputX != 0)
            // {
            //     currMspd += mspdAcc;
            //     if (currMspd > maxMspd) currMspd = maxMspd;
            //     float movementX = currMspd * inputX * Time.deltaTime;
            //     if (inputX > 0)
            //     {
            //         currDirection = 1;
            //     }
            //     else
            //     {
            //         currDirection = -1;
            //     }
            //     transform.rotation = Quaternion.Euler(0, 90 * currDirection, 0);
            //     if (fsm.currState != PlayerStateMachine.playerState.attackState)
            //     {
            //         SetCurrState(PlayerStateMachine.playerState.runState);
            //     }
            // }
            // else
            // {
            //     currMspd = 0;
            //     if (fsm.currState != PlayerStateMachine.playerState.attackState)
            //     {
            //         SetCurrState(PlayerStateMachine.playerState.idleState);
            //     }
            // }
            // // Jump
            // if (
            //     Input.GetKey(KeyCode.Space) &&
            //     (fsm.currState == PlayerStateMachine.playerState.runState ||
            //     fsm.currState == PlayerStateMachine.playerState.idleState)
            // )
            // {
            //     Debug.Log("jump");
            //     vertSpd = jumpSpd;
            //     Jump();
            // }

        }

        void SetCurrState(PlayerStateMachine.playerState newState)
        {
            fsm.SetCurrState(newState, animator);
        }

        void Jump()
        {
            // StartCoroutine(JumpPeriod());
            SetCurrState(PlayerStateMachine.playerState.jumpState);
        }

        IEnumerator JumpPeriod()
        {
            SetCurrState(PlayerStateMachine.playerState.jumpState);
            yield return new WaitForSeconds(0.7f);
        }

        void Attack()
        {
            attackController.ClearHitEnemyList();
            StartCoroutine(AttackPeriod());
        }

        IEnumerator AttackPeriod()
        {
            if (fsm.currState == PlayerStateMachine.playerState.jumpState)
            {
                vertSpd = 0;
                gravity = 0;
            }
            SetCurrState(PlayerStateMachine.playerState.attackState);
            yield return new WaitForSeconds(1);
            SetCurrState(PlayerStateMachine.playerState.idleState);
            gravity = 5;
        }
    }
}


