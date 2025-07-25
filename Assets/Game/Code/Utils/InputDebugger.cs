using UnityEngine;
using UnityEngine.InputSystem;

namespace SteveAdventure
{
    public sealed class InputDebugger : MonoBehaviour
    {
        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                Debug.Log("Mouse click detected");
        }
    }
}