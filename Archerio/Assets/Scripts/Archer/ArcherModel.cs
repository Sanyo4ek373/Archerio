using UnityEngine;

namespace Archerio {
    public class ArcherModel : MonoBehaviour {
        private int _health = 5;
        private float _moveSpeed = 4f;
        private float _rotateSpeed = 7f;
   
        public bool IsRun { get; set; }
        public bool IsAttack { get; set; }
        public int Health { get => _health; }
        public float MoveSpeed { get => _moveSpeed; }
        public float RotateSpeed { get => _rotateSpeed; }
    }
}
