namespace JasonStorey 
{
    public interface Output
    {
        void Say(object message);
        void Say(object message, UnityEngine.Object sender);
    }
}