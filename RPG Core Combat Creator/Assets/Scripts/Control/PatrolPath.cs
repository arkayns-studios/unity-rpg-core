using UnityEngine;

namespace RPG.Control {

    public class PatrolPath : MonoBehaviour {

        [SerializeField] private float radius = 0.5f;

        private void OnDrawGizmos () {
            Gizmos.color = Color.gray;

            for (int i = 0; i < transform.childCount; i++) {
                int j = GetNextIndex (i);
                Gizmos.DrawSphere (GetWaypoint (i), radius);
                Gizmos.DrawLine (GetWaypoint (i), GetWaypoint (j));
            }
        } // OnDrawGizmos

        public int GetNextIndex (int i) {
            if (i + 1 == transform.childCount) return 0;
            return i + 1;
        } // GetNextIndex

        public Vector3 GetWaypoint (int i) {
            return transform.GetChild (i).transform.position;
        } // GetWaypoint

    } // Class PatrolPath

} // Namespace RPG Control