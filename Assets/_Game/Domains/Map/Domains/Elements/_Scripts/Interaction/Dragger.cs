namespace Game.Map.Element
{
    public class Dragger : IDraggable
    {
        private readonly IEditVisual _editVisual;

        public Dragger(IEditVisual editVisual)
        {
            _editVisual = editVisual;
        }
        
        public void StartDrag()
        {
            _editVisual.ChangeToEditMode();
        }


        public void EndDrag(bool hasPlaced)
        {
           if(hasPlaced)
                _editVisual.ChangeToIdleMode();
        }

        public void OnDrag(bool canPlace)
        {
            _editVisual.SetPlaceAvailbility(canPlace);
        }


    }
}