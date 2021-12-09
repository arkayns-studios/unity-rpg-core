using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control {

    public class AIController : MonoBehaviour {

        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointDwellTime = 3f;

        private Fighter fighter;
        private Health health;
        private Mover mover;
        private GameObject player;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int currentWayipointIndex = 0;

        private void Start () {
            fighter = GetComponent<Fighter> ();
            health = GetComponent<Health> ();
            mover = GetComponent<Mover> ();
            player = GameObject.FindGameObjectWithTag ("Player");

            guardPosition = transform.position;
        } // Start

        private void Update () {
            if (health.IsDead ()) return;

            if (InAttackRangeOfPlayer () && fighter.CanAttack (player))
                AttackBehaviour ();
            else if (timeSinceLastSawPlayer < suspicionTime)
                SuspicionBehaviour ();
            else
                PatrolBehaviour ();

            UpdateTimers ();
        } // Update

        private void UpdateTimers () {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        } // UpdateTimers

        private void AttackBehaviour () {
            timeSinceLastSawPlayer = 0;
            fighter.Attack (player);
        } // AttackBehaviour
        private void SuspicionBehaviour () {
            GetComponent<ActionScheduler> ().CancelCurrentAction ();
        } // SuspicionBehaviour

        private void PatrolBehaviour () {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null) {
                if (AtWaypoint ()) {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint ();
                }
                nextPosition = GetCurrenWaypoint ();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
                mover.StartMoveAction (nextPosition);
        } // PatrolBehaviour

        private bool AtWaypoint () {
            float distanceToWaypoint = Vector3.Distance (transform.position, GetCurrenWaypoint ());
            return distanceToWaypoint < waypointTolerance;
        } // AtWaypoint

        private void CycleWaypoint () {
            currentWayipointIndex = patrolPath.GetNextIndex (currentWayipointIndex);
        } // CycleWaypoint

        private Vector3 GetCurrenWaypoint () {
            return patrolPath.GetWaypoint (currentWayipointIndex);
        } // GetCurrenWaypoint

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere (transform.position, chaseDistance);
        } // OnDrawGizmosSelected

        private bool InAttackRangeOfPlayer () {
            float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        } // InAttackRangeOfPlayer

    } // Class AIController

} // Namespace RPG Control