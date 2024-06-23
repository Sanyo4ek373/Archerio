using System;
using UnityEngine;

namespace Archerio {
    public class ArcherModel : MonoBehaviour {
        private float _health = 500f;
        private float _maximumHealth = 500f;
        private readonly float _moveSpeed = 4f;
        private readonly float _rotateSpeed = 7f;
        private float _reloadingTime = 1f;
        private float _damage = 40;
   
        public bool IsRun { get; set; }
        public bool IsAttack { get; set; }

        public float Health => _health; 
        public float MaximumHealth => _maximumHealth;
        public float MoveSpeed => _moveSpeed; 
        public float RotateSpeed => _rotateSpeed; 
        public float ReloadingTime => _reloadingTime;
        public float Damage => _damage;

        public event Action OnDied;

        public void TakeDamage(float damage) {
            _health -= damage;
            Debug.Log($"Archer take {damage} damage");
            if (_health <= 0) OnDied.Invoke();
        }

        public void LevelUp() {
            _damage += 10;
            _maximumHealth += 20;
        }
    }
}
