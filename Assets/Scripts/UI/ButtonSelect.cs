using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("缩放比例")]
    public float scaleFactor;
    [Header("持续时间")]
    public float duration;
    
    public GameObject image;

    private Vector3 originalScale;
    private bool isHovering;
    
    private void Start()
    {
        isHovering = false;
        originalScale = transform.localScale;
        image.SetActive(false);
    }

    private void Update()
    {
        DoSomethingWhenHovering();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void DoSomethingWhenHovering()
    {
        if (isHovering)
        {
            image.SetActive(true);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * scaleFactor, Time.deltaTime / duration);
        }
        else
        { 
            image.SetActive(false);
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime / duration);
        }
    }
}
