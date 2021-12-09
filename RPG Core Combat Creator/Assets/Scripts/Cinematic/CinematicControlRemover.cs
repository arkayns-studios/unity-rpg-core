using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematic {

    public class CinematicControlRemover : MonoBehaviour {

        private GameObject player;

        private void Start () {
            GetComponent<PlayableDirector> ().played += DisableControl;
            GetComponent<PlayableDirector> ().stopped += EnableControl;
            player = GameObject.FindGameObjectWithTag ("Player");
        } // Start

        private void DisableControl (PlayableDirector pd) {
            player.GetComponent<ActionScheduler> ().CancelCurrentAction ();
            player.GetComponent<PlayerController> ().enabled = false;
        } // DisableControl

        private void EnableControl (PlayableDirector pd) {
            player.GetComponent<PlayerController> ().enabled = true;
        } // EnableControl

    } // Class CinematicControlRemover

} // Namespace RPG Cinematic