﻿using UnityEngine.AI;
using UnityEngine;
using RPG.Core;

namespace RPG.Movement {

    public class Mover : MonoBehaviour, IAction {

        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private Health health;

        private void Start () {
            navMeshAgent = GetComponent<NavMeshAgent> ();
            animator = GetComponent<Animator> ();
            health = GetComponent<Health> ();
        } // Start

        private void Update () {
            navMeshAgent.enabled = !health.IsDead ();
            UpdateAnimator ();
        } // Update

        public void StartMoveAction (Vector3 destination) {
            GetComponent<ActionScheduler> ().StartAction (this);
            MoveTo (destination);
        } // StartMoveAction

        public void MoveTo (Vector3 destination) {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        } // MoveTo

        public void Cancel () {
            navMeshAgent.isStopped = true;
        } // Cancel

        private void UpdateAnimator () {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection (velocity);
            float speed = localVelocity.z;
            animator.SetFloat ("forwardSpeed", speed);
        } // UpdateAnimator

    } // Class Mover

} // Namespace RPG Movement