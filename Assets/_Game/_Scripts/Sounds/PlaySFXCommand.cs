namespace Game
{
    public class PlaySFXCommand : IEventCommand
    {
        private readonly ISoundsManager _sfxManager;
        private readonly AudioClipInfoSO _audioInfo;

        public PlaySFXCommand(ISoundsManager soundsManager, AudioClipInfoSO audioInfo)
        {
            _sfxManager = soundsManager;
            _audioInfo = audioInfo;
        }
        public void Execute(object value)
        {
            _sfxManager.PlayOneShot(_audioInfo);
        }
    }
}