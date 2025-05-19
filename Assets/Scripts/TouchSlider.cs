using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownEvent;
    public UnityAction OnPointerUpEvent;
    public UnityAction<float> OnPointerDragEvent;

    private Slider uiSlider;

    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownEvent != null)
        {
            OnPointerDownEvent.Invoke();
        }
        if (OnPointerDragEvent != null) {
            OnPointerDragEvent.Invoke(uiSlider.value);
        }
        Debug.Log("Touch started on slider: " + gameObject.name);
        // Add your touch slider logic here
    }

    private void OnSliderValueChanged(float value)
    {
        if (OnPointerDragEvent != null)
        {
            OnPointerDragEvent.Invoke(value);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the touch is released on the UI element
        if (eventData.pointerPress == gameObject)
        {
            if (OnPointerUpEvent != null)
            {
                OnPointerUpEvent.Invoke();
                // reset slider value to 0 when touch ends
                uiSlider.value = 0f;
            }
            Debug.Log("Touch ended on slider: " + gameObject.name);
            
        }

    }

    private void OnDestroy()
    {
        // remove listener to avoid memory leaks
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}

