using System;
using UnityEngine;

namespace Archerio {
    public class EnemyModel : MonoBehaviour {
        private float _health = 250;
        private float _damage = 30;
        private readonly float _moveSpeed = 1f;
        private readonly float _rotateSpeed = 7f;
        private float _reloadingTime = 2f;
   
        public bool IsRun { get; set; }
        public bool IsAttack { get; set; }

        public float Health => _health; 
        public float MoveSpeed => _moveSpeed; 
        public float RotateSpeed => _rotateSpeed; 
        public float ReloadingTime => _reloadingTime;
        public float Damage => _damage;

        public event Action OnDied;

        public void TakeDamage(float damage) {
            _health -= damage;
            Debug.Log($"Enemy take: {damage} damage");
            if (_health <= 0) OnDied.Invoke();
        }
    }
}
