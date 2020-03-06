using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Player
{
    public class AttackController : MonoBehaviour
    {
        public Collider sword;
        public int ATK;
        private List<GameObject> hitEnemies = new List<GameObject>();

        PlayerMovement playerMovement;

        void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();            
        }

        void FixedUpdate()
        {
            if(playerMovement.fsm.currState == PlayerStateMachine.playerState.attackState){
                DetectAttack(sword);
            }
        }

        public void ClearHitEnemyList()
        {
            hitEnemies.Clear();
        }
        void DetectAttack(Collider swordHitbox)
        {
            Collider[] hurtboxes = Physics.OverlapBox(
                swordHitbox.bounds.center,
                swordHitbox.bounds.extents,
                swordHitbox.transform.rotation,
                LayerMask.GetMask("Enemy")
            );
            

            foreach (Collider hurtbox in hurtboxes)
            {
                GameObject enemy = hurtbox.gameObject;
                if (!hitEnemies.Contains(enemy))
                {
                    hitEnemies.Add(enemy);
                    enemy.SendMessageUpwards("TakeDamage", ATK);                    Debug.Log(enemy.name);
                }
            }
        }
    }
}