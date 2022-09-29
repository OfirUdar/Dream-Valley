using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralPointerUpTrigger : MonoBehaviour
{
    private float _timer = 0;
    public UnityEvent<float> OnPointerUpEvent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _timer = 0;
        if (Input.GetMouseButtonUp(0))
            OnPointerUpEvent?.Invoke(_timer);

        _timer += Time.deltaTime;
    }
}
