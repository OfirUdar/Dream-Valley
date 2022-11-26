namespace Game
{
    public class DialogManager 
    {
        public static IDialog Instance { get; private set; }

        public DialogManager(IDialog dialog)
        {
            Instance = dialog;
        }
    }
}
