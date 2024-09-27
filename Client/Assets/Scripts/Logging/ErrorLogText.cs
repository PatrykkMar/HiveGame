using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour
{
    [SerializeField] private Text errorText;

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
        errorText.text = string.Empty;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    // Metoda do obs�ugi log�w
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            if (errorText != null)
            {
                errorText.text = logString;
            }
        }
    }
}