using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement {
    
    public class Portal : MonoBehaviour {
        [SerializeField] private int m_sceneToLoad = -1;
        
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) 
                SceneManager.LoadScene(m_sceneToLoad);
        } // OnTriggerEnter

    } // Class Portal
    
} // Namespace RPG SceneManagement

