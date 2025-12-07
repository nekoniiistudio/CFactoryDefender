using System.Diagnostics;
using Newtonsoft.Json;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace __RSUnityFramework__.Core.Utilities
{
    public class RSLogger
    {
        private const string CONDITIONAL_KEY = "RS_LOG";

        // Color constants for different log types
        private const string COLOR_LOG = "#00FF00";     // Green
        private const string COLOR_WARNING = "#FFFF00"; // Yellow
        private const string COLOR_ERROR = "#FF0000";   // Red
        private const string COLOR_SUCCESS = "#00FFFF"; // Cyan
        private const string COLOR_INFO = "#FFFFFF";    // White

#region Basic Log Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void Log(string msg, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {msg}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogObject(object msg, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {JsonConvert.SerializeObject(msg)}");


        [Conditional(CONDITIONAL_KEY)]
        public static void Log(string msg, Object context, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {msg}", context);

        [Conditional(CONDITIONAL_KEY)]
        public static void LogWarning(string msg, string channel = "RS")
            => Debug.LogWarning($"[{ColorText(channel, COLOR_WARNING)}] : {msg}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogWarning(string msg, Object context, string channel = "RS")
            => Debug.LogWarning($"[{ColorText(channel, COLOR_WARNING)}] : {msg}", context);

        [Conditional(CONDITIONAL_KEY)]
        public static void LogError(string msg, string channel = "RS")
            => Debug.LogError($"[{ColorText(channel, COLOR_ERROR)}] : {msg}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogError(string msg, Object context, string channel = "RS")
            => Debug.LogError($"[{ColorText(channel, COLOR_ERROR)}] : {msg}", context);

#endregion

#region Colored Log Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void LogColored(string msg, string color, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {ColorText(msg, color)}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogColored(string msg, string color, Object context, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {ColorText(msg, color)}", context);

        [Conditional(CONDITIONAL_KEY)]
        public static void LogSuccess(string msg, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_SUCCESS)}] ✓ {ColorText(msg, COLOR_SUCCESS)}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogSuccess(string msg, Object context, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_SUCCESS)}] ✓ {ColorText(msg, COLOR_SUCCESS)}", context);

        [Conditional(CONDITIONAL_KEY)]
        public static void LogInfo(string msg, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_INFO)}] ℹ {msg}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogInfo(string msg, Object context, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_INFO)}] ℹ {msg}", context);

#endregion

#region Formatted Log Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void LogFormat(string format, string channel = "RS", params object[] args)
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {string.Format(format, args)}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogWarningFormat(string format, string channel = "RS", params object[] args)
            => Debug.LogWarning($"[{ColorText(channel, COLOR_WARNING)}] : {string.Format(format, args)}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogErrorFormat(string format, string channel = "RS", params object[] args)
            => Debug.LogError($"[{ColorText(channel, COLOR_ERROR)}] : {string.Format(format, args)}");

#endregion

#region Highlight Object Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void LogHighlight(string msg, Object obj, string channel = "RS")
        {
            Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {msg}", obj);
            HighlightObject(obj);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void LogWarningHighlight(string msg, Object obj, string channel = "RS")
        {
            Debug.LogWarning($"[{ColorText(channel, COLOR_WARNING)}] : {msg}", obj);
            HighlightObject(obj);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void LogErrorHighlight(string msg, Object obj, string channel = "RS")
        {
            Debug.LogError($"[{ColorText(channel, COLOR_ERROR)}] : {msg}", obj);
            HighlightObject(obj);
        }

#endregion

#region Bold & Italic Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void LogBold(string msg, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {BoldText(msg)}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogItalic(string msg, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {ItalicText(msg)}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogBoldColored(string msg, string color, string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] : {BoldText(ColorText(msg, color))}");

#endregion

#region Separator & Header Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void LogSeparator(string channel = "RS")
            => Debug.Log($"[{ColorText(channel, COLOR_LOG)}] {new string('=', 60)}");

        [Conditional(CONDITIONAL_KEY)]
        public static void LogHeader(string header, string channel = "RS")
        {
            LogSeparator(channel);
            Debug.Log($"[{ColorText(channel, COLOR_LOG)}] {BoldText(ColorText(header, COLOR_SUCCESS))}");
            LogSeparator(channel);
        }

#endregion

#region Conditional Logging

        [Conditional(CONDITIONAL_KEY)]
        public static void LogIf(bool condition, string msg, string channel = "RS")
        {
            if (condition)
                Log(msg, channel);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void LogWarningIf(bool condition, string msg, string channel = "RS")
        {
            if (condition)
                LogWarning(msg, channel);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void LogErrorIf(bool condition, string msg, string channel = "RS")
        {
            if (condition)
                LogError(msg, channel);
        }

#endregion

#region Assert Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void Assert(bool condition, string msg, string channel = "RS")
        {
            if (!condition)
                LogError($"ASSERT FAILED: {msg}", channel);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void AssertNotNull(Object obj, string msg = "Object is null!", string channel = "RS")
        {
            if (obj == null)
                LogError($"NULL ASSERT: {msg}", channel);
        }

#endregion

#region Helper Methods

        private static string ColorText(string text, string color)
            => $"<color={color}>{text}</color>";

        private static string BoldText(string text)
            => $"<b>{text}</b>";

        private static string ItalicText(string text)
            => $"<i>{text}</i>";

        private static void HighlightObject(Object obj)
        {
            if (obj == null) return;

#if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.PingObject(obj);
            UnityEditor.Selection.activeObject = obj;
#endif
        }

#endregion

#region Advanced Methods

        [Conditional(CONDITIONAL_KEY)]
        public static void LogArray<T>(T[] array, string arrayName = "Array", string channel = "RS")
        {
            if (array == null || array.Length == 0)
            {
                LogWarning($"{arrayName} is null or empty", channel);
                return;
            }

            LogHeader($"{arrayName} (Count: {array.Length})", channel);
            for (int i = 0; i < array.Length; i++)
            {
                Log($"[{i}] = {array[i]}", channel);
            }

            LogSeparator(channel);
        }

        [Conditional(CONDITIONAL_KEY)]
        public static void LogException(System.Exception ex, string channel = "RS")
            => Debug.LogException(ex);

        [Conditional(CONDITIONAL_KEY)]
        public static void LogVector3(Vector3 vector, string name = "Vector3", string channel = "RS")
            => Log($"{name}: X={vector.x:F2}, Y={vector.y:F2}, Z={vector.z:F2}", channel);

        [Conditional(CONDITIONAL_KEY)]
        public static void LogQuaternion(Quaternion quaternion, string name = "Quaternion", string channel = "RS")
            => Log($"{name}: X={quaternion.x:F2}, Y={quaternion.y:F2}, Z={quaternion.z:F2}, W={quaternion.w:F2}",
                channel);

#endregion
    }
}