using UnityEngine;

namespace JasonStorey
{
    public class ConsoleOutput : Output
    {
        public void Say(object message) => Debug.Log(message);

        public void Say(object message, Object sender) => Debug.Log(message,sender);
    }
}