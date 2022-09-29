using UnityEngine;
using UnityEngine.Events;

public class PointerUpTrigger : MonoBehaviour
{
    private float _timer;

    public UnityEvent<float> OnPointerUpAsButtonEvent;
    private void OnMouseDown()
    {
        _timer = 0;
    }
    private void OnMouseUpAsButton()
    {
        OnPointerUpAsButtonEvent?.Invoke(_timer);
    }
    private void Update()
    {
        _timer += Time.deltaTime;
    }
}

