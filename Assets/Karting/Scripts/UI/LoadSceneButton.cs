using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        [SerializeField] private TextMeshProUGUI  timer;


        private void Start()
        {
            StartCoroutine(AutoRestart());
        }

        private IEnumerator AutoRestart()
        {

            for (int i = 10; i >= 0; i--) {
                timer.text = $"Auto Restart in {i}s";
                yield return new WaitForSeconds(1);
                
            }

            SceneManager.LoadSceneAsync(SceneName);
        }

        public void LoadTargetScene() 
        {
            SceneManager.LoadSceneAsync(SceneName);
        }
    }
}
