using UnityEngine;

namespace Archerio {
    public class EnemyView : MonoBehaviour {
        private Animator _animator;
        private EnemyModel _model;

        private const string IS_RUN = "IsRun";
        private const string IS_ATTACK = "IsAttack";

        private void Update() {
            _animator.SetBool(IS_RUN, _model.IsRun);
            _animator.SetBool(IS_ATTACK, _model.IsAttack);
        }

        public void Construct(EnemyModel model) {
            _animator = GetComponent<Animator>();
            _model = model;
        }
    }
}