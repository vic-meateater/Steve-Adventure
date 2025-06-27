using UnityEngine;

namespace SteveAdventure
{
    public class InteractObjectsController : MonoBehaviour
    {
        [SerializeField] private InteractToggleObject[] _interactObjects;
        [SerializeField] private InteractObject _interactObject;
        
        
        private void Start()
        {
            foreach (var obj in _interactObjects)
            {
                obj.OnStateChanged += OnStateChangedHandler;            
            }
            
            _interactObject.OnInteracted += OnInteractedHandler;
        }


        private void OnDestroy()
        {
            foreach (var obj in _interactObjects)
            {
                obj.OnStateChanged -= OnStateChangedHandler; 
            }
            _interactObject.OnInteracted -= OnInteractedHandler;
        }
        private void OnInteractedHandler(InteractObject obj)
        {
            if (IsAllObjActivated())
            {
                Debug.Log("Проходи...");
                obj.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Найди и активируй все столпы света. И мы тебя пропустим");
            }
        }

        private void OnStateChangedHandler(InteractToggleObject obj)
        {
            //TODO: эффекты при активации.  
        }

        private bool IsAllObjActivated()
        {
            foreach (var interactObject in _interactObjects)
            {
                if (!interactObject.IsActivated)
                    return false;
            }

            return true;
        }
    }
}
