namespace Game
{
    public class PlaySFXCommand : IEventCommand
    {
        private readonly ISFXManager _sfxManager;
        private readonly AudioClipInfoSO _audioInfo;

        public PlaySFXCommand(ISFXManager soundsManager, AudioClipInfoSO audioInfo)
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