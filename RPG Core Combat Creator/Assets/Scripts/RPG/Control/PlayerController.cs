using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control {

    public class PlayerController : MonoBehaviour {

        private Mover mover;
        private Health health;

        private void Start () {
            mover = GetComponent<Mover> ();
            health = GetComponent<Health> ();
        } // Start

        private void Update () {
            if (health.IsDead ()) return;
            if (InteractWithCombat ()) return;
            if (InteractWithMovement ()) return;
        } // Update

        private bool InteractWithMovement () {
            RaycastHit hit;
            if (Physics.Raycast (GetMouseRay (), out hit)) {
                if (Input.GetMouseButton (0))
                    mover.StartMoveAction (hit.point, 1f);
                return true;
            }
            return false;
        } // InteractWithMovement

        private bool InteractWithCombat () {
            RaycastHit [] hits = Physics.RaycastAll (GetMouseRay ());
            foreach (RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget> ();
                if(target == null) continue;

                GameObject targetGameObject = target.gameObject;
                if(!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton (0)) 
                    GetComponent<Fighter> ().Attack (target.gameObject);
                return true;
            }
            return false;
        } // InteractWithCombat

        private static Ray GetMouseRay () {
            return Camera.main.ScreenPointToRay (Input.mousePosition);
        } // GetMouseRay

    } // Class PlayerController

} // Namespace RPG Control