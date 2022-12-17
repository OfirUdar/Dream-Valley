namespace Game.Map.Element
{
    public interface ISelectVisual
    {
        /// <summary>
        /// Refershing the model, it is useful after changing the GFX of the model. For example, after upgrading..
        /// </summary>
        public void RefreshGFX();
        public void Select();
        public void Unselect();
    }
}