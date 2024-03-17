using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logger {
    [System.Serializable]
    public class Logger
    {
        [SerializeField] private bool enableLogging = true;

        private void LogInternal(object message, UnityEngine.Object context, System.Action<object, UnityEngine.Object> logMethod)
        {
            if (!enableLogging) return;

#if UNITY_EDITOR
            string contextName = context != null ? $"{context.name}: " : "";
            logMethod($"{contextName}{message}", context);
#endif
        }

        public void Log(object message, UnityEngine.Object context = null)
        {
            LogInternal(message, context, Debug.Log);
        }

        public void LogWarning(object message, UnityEngine.Object context = null)
        {
            LogInternal(message, context, Debug.LogWarning);
        }

        public void LogError(object message, UnityEngine.Object context = null)
        {
            LogInternal(message, context, Debug.LogError);
        }
    }
}

