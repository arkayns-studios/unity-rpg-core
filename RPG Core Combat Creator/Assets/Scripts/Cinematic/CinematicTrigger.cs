using UnityEngine.Playables;
using UnityEngine;

namespace RPG.Cinematic {

    public class CinematicTrigger : MonoBehaviour {

        private bool alreadyTriggered = false;

        private void OnTriggerExit (Collider other) {
            if(!alreadyTriggered && other.gameObject.tag == "Player") {
                alreadyTriggered = true;
                GetComponent<PlayableDirector> ().Play ();
            }
                
        } // OnTriggerExit

    } // Class CinematicTrigger

} // Namespace RPG Cinematic