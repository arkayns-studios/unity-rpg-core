using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction {

        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float timeBetweenAttacks = 1f;

        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Update () {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead ()) return;

            if (!GetIsInRange ()) {
                GetComponent<Mover> ().MoveTo (target.transform.position);
            } else {
                GetComponent<Mover> ().Cancel ();
                AttackBehaviour ();
            }
                
        } // Update

        private void AttackBehaviour () {
            transform.LookAt (target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks) {
                // This will trigger the Hit event
                TriggerAttack ();
                timeSinceLastAttack = 0;
            }
        } // AttackBehaviour

        private void TriggerAttack () {
            GetComponent<Animator> ().ResetTrigger ("stopAttack");
            GetComponent<Animator> ().SetTrigger ("attack");
        } // TriggerAttack

        private void ResetTriggerAttack () {
            GetComponent<Animator> ().ResetTrigger ("attack");
            GetComponent<Animator> ().SetTrigger ("stopAttack");
        } // ResetTriggerAttack

        // Animation Event
        public void Hit () {
            if (target == null) return;
            target.TakeDamage (weaponDamage);
        } // Hit

        public bool CanAttack (GameObject combatTarget) {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health> ();
            return targetToTest != null && !targetToTest.IsDead ();
        } // CanAttack

        public bool GetIsInRange () {
            return Vector3.Distance (transform.position, target.transform.position) < weaponRange;
        } // GetIsInRange

        public void Attack (GameObject combatTarget) {
            GetComponent<ActionScheduler> ().StartAction (this);
            target = combatTarget.GetComponent<Health> ();
        } // Attack

        public void Cancel () {
            ResetTriggerAttack ();
            target = null;
        } // Cancel

    } // Class Fighter

} // Namespace RPG Combat