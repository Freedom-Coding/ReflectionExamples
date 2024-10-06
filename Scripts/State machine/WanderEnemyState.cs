using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

namespace ReflectionExample
{
    public class WanderEnemyState : IEnemyState
    {
        private EnemyAI enemyAI;
        private float wanderTimeMin = 2;
        private float wanderTimeMax = 5;
        private float wanderDistance = 2;

        public void EnterState(EnemyAI _enemyAI)
        {
            enemyAI = _enemyAI;
        }

        public void UpdateState()
        {
            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer > enemyAI.followRange)
            {
                Wander();
            }
            else
            {
                enemyAI.SetState<AttackEnemyState>();
            }
        }

        float wanderTimer = 0;
        Vector2 randomPoint;
        private void Wander()
        {
            wanderTimer -= Time.deltaTime;
            enemyAI.MoveTowards(randomPoint, 0.5f);
            enemyAI.LookAt(randomPoint, 0.5f);

            if (wanderTimer <= 0)
            {
                GetNewWanderPosition();
            }
        }

        private void GetNewWanderPosition()
        {
            wanderTimer = Random.Range(wanderTimeMin, wanderTimeMax);
            randomPoint = Random.insideUnitCircle * wanderDistance + (Vector2)enemyAI.transform.position;
        }
    }
}