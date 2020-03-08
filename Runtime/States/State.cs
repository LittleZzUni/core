namespace JasonStorey 
{
    public interface State
    {
        void Enter();
        void Update(float tick);
        void Exit();
    }

    public class NoState : State
    {
        public void Enter() { }

        public void Update(float tick) { }

        public void Exit() { }

        #region Singleton

        public static NoState Instance => _instance ?? (_instance = new NoState());
        private static NoState _instance;

        #endregion
    }
}