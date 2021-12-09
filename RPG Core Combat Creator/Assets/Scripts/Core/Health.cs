using UnityEngine;

namespace RPG.Core {

    public class Health : MonoBehaviour {

        [SerializeField] private float healthPoint = 100;
        private bool isDead;

        public bool IsDead() => isDead;

        public void TakeDamage (float damage) {
            healthPoint = Mathf.Max (healthPoint - damage, 0);
            if (healthPoint == 0)
                Die ();
        } // TakeDamage

        private void Die () {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator> ().SetTrigger ("die");
            GetComponent<ActionScheduler> ().CancelCurrentAction ();
        } // Die

    } // Class Health

} // Namespace RPG Core