namespace Game
{
    public interface ISoundsManager
    {
        public void PlayOneShot(AudioClipInfo audioInfo);
        public void PlayRandomOneShot(params AudioClipInfo[] audioInfos);
    }
}
