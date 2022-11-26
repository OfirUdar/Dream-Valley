using System;

namespace Game
{
    public interface IDialog
    {
        public void Show(string title, string content, Action okCallback=null);
        public void ShowComplex(string title, string content, string ok = "ok", string cancel = "cancel", Action okCallback = null, Action cancelCallback = null);

        public void ForceHide();
    }

}
