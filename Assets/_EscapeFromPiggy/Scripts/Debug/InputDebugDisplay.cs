using UnityEngine;
using TMPro;
using EscapeFromPiggy.Managers;

namespace EscapeFromPiggy.Debug
{
    public class InputDebugDisplay : MonoBehaviour
    {
        private TextMeshProUGUI _debugText;

        private void Start()
        {
            GameObject foundObj = GameObject.Find("DebugText");
            if (foundObj != null)
            {
                _debugText = foundObj.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                UnityEngine.Debug.LogError("DebugText objesi bulunamadı! İsminin 'DebugText' olduğundan emin ol.");
            }
        }

        private void Update()
        {
            if (_debugText == null || InputManager.Instance == null) return;

            _debugText.text = $"Move: {InputManager.Instance.MoveInput}\n" +
                              $"Jump Held: {InputManager.Instance.JumpHeld}\n" +
                              $"Jump Pressed: {InputManager.Instance.JumpPressed}\n" +
                              $"Dash Pressed: {InputManager.Instance.DashPressed}";
        }
    }
}
