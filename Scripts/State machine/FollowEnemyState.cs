using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

namespace ReflectionExample
{
    public class FollowEnemyState : IEnemyState
    {
        private EnemyAI enemyAI;

        public void EnterState(EnemyAI _enemyAI)
        {
            enemyAI = _enemyAI;
        }

        public void UpdateState()
        {
            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer < enemyAI.followRange && distToPlayer > enemyAI.attackRange)
            {
                Follow();
            }
            else if (distToPlayer > enemyAI.followRange)
            {
                enemyAI.SetState<FollowEnemyState>();
            }
            else
            {
                enemyAI.SetState<AttackEnemyState>();
            }
        }

        private void Follow()
        {
            enemyAI.MoveTowards(enemyAI.playerTransform.position);
            enemyAI.LookAt(enemyAI.playerTransform.position);
        }
    }
}