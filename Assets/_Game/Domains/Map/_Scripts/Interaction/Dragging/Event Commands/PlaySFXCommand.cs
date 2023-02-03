namespace Game.Map
{
    public class PlaySFXCommand : IEventCommand
    {
        private readonly ISoundsManager _sfxManager;
        private readonly AudioClipInfo _clipInfo;

        public PlaySFXCommand(ISoundsManager soundsManager, AudioClipInfo clipInfo)
        {
            _sfxManager = soundsManager;
            _clipInfo = clipInfo;
        }
        public void Execute(object value)
        {
            _sfxManager.PlayOneShot(_clipInfo);
        }
    }
}