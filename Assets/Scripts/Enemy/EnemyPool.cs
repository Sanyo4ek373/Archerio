using System.Collections.Generic;
using UnityEngine;

namespace Archerio {
    public class EnemyPool : MonoBehaviour {
        [SerializeField] private List<GameObject> _enemies = new();

        public GameObject GetEnemy() {
            if (_enemies.Count == 0) return null;

            return _enemies[0];
        }

        public void MakeEnemyFirst(GameObject enemy) {
            if (_enemies.Contains(enemy)) {
                _enemies.Remove(enemy);
                _enemies.Insert(0, enemy);
            }
        }

        public void DeleteEnemy(GameObject enemy) {
            if (_enemies.Contains(enemy)) {
                _enemies.Remove(enemy);
            }
        }
    }
}

