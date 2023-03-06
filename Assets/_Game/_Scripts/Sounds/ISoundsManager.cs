namespace Game
{
    public interface ISoundsManager
    {
        public void PlayOneShot(AudioClipInfoSO audioInfo);
        public void PlayRandomOneShot(params AudioClipInfoSO[] audioInfos);
    }
}
