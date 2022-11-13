using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Credits : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject creditsPanel;

    private void Start()
    {
        creditsPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        creditsPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        creditsPanel.SetActive(false);
    }
}
