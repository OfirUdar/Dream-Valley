namespace Game.Map.Element
{
    public interface IEditVisual
    {
        public void SetPlaceAvailbility(bool isAvailable);
        public void ChangeToIdleMode();
        public void ChangeToEditMode();

    }
}