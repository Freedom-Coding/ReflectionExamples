using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReflectionExample
{
    public class EnemyAI : MonoBehaviour
    {        
        public float followRange = 7;
        public float attackRange = 4;
        public float attackCooldown = 2;
        public Transform weaponTransform;
        public GameObject bulletPrefab;

        [HideInInspector] public Transform playerTransform;

        [SerializeField] private float moveSpeed = 0.6f;
        [SerializeField] private float rotationSpeed = 50;

        private IEnemyState currentState;
        StateFactory stateFactory = new StateFactory();

        private void Start()
        {
            playerTransform = FindObjectOfType<ExampleShipControl>().transform;

            SetState<WanderEnemyState>();
        }

        private void Update()
        {
            currentState.UpdateState();
        }

        public void SetState<T>() where T : IEnemyState, new()
        {
            currentState = stateFactory.GetState<T>();
            currentState.EnterState(this);
        }

        public float GetDistanceToPlayer()
        {
            return Vector2.Distance(transform.position, playerTransform.position);
        }

        public void MoveTowards(Vector2 destination, float speedMultiplier = 1)
        {
            Vector3 direction = (Vector3)destination - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime * speedMultiplier;
        }

        public void LookAt(Vector2 destination, float speedMultiplier = 1)
        {
            Vector3 direction = (Vector3)destination - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * speedMultiplier);
        }
    }
}