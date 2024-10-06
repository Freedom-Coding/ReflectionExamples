using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ReflectionExample
{
    public class AttackEnemyState : IEnemyState
    {
        private EnemyAI enemyAI;

        public void EnterState(EnemyAI _enemyAI)
        {
            enemyAI = _enemyAI;
        }

        public void UpdateState()
        {
            float distToPlayer = enemyAI.GetDistanceToPlayer();

            if (distToPlayer < enemyAI.attackRange)
            {
                Attack();
            }
            else
            {
                enemyAI.SetState<AttackEnemyState>();
            }
        }

        float attackTimer = 0;
        private void Attack()
        {
            enemyAI.LookAt(enemyAI.playerTransform.position, 0.35f);
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = enemyAI.attackCooldown;
                GameObject bullet = GameObject.Instantiate(enemyAI.bulletPrefab, enemyAI.weaponTransform.position, enemyAI.weaponTransform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 6), ForceMode2D.Impulse);
                GameObject.Destroy(bullet, 5);
            }
        }
    }
}