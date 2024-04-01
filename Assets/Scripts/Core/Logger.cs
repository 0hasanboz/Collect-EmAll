using UnityEngine;

namespace Core
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void Log(string message, Color color)
        {
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>");
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}