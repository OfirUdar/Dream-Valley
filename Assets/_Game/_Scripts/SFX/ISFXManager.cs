namespace Game
{
    public interface ISFXManager
    {
        public void PlayOneShot(AudioClipInfoSO audioInfo);
        public void PlayOneShotWithDelay(AudioClipInfoSO audioInfo,int milisecondsDelay);
        public void PlayRandomOneShot(params AudioClipInfoSO[] audioInfos);
    }
}
