namespace MaskTransitions
{
    using UnityEngine;

    public class SceneSwitch : MonoBehaviour
    {
        public string sceneToLoadName;
        public float totalTransitionTime;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SwitchScene()
        {
            TransitionManager.Instance.LoadLevel(sceneToLoadName);
        }

        public void PlayTransition()
        {
            TransitionManager.Instance.PlayTransition(totalTransitionTime);
        }

        void PlayStartOfTransition()
        {
            TransitionManager.Instance.PlayStartHalfTransition(totalTransitionTime / 2);
        }
        void PlayEndOfTransition()
        {
            TransitionManager.Instance.PlayEndHalfTransition(totalTransitionTime / 2);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PlayStartOfTransition();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                PlayEndOfTransition();
            }
        }
    }
}
