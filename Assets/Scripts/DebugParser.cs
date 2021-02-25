using UnityEngine;
using UnityEngine.UI;

namespace DebugStuff
{
    public class DebugParser : MonoBehaviour
    {
        public Text text;
        static string myLog = "";
        private string output;
        private string stack;

        void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string stackTrace, LogType type)
        {
            output = logString;
            stack = stackTrace;
            myLog = output + "\n" + myLog;
            if (myLog.Length >= 1000)
            {
                myLog = myLog.Substring(0, 1000);
            }
        }

        void Update()
        {
            text.text = myLog;
        }
    }
}