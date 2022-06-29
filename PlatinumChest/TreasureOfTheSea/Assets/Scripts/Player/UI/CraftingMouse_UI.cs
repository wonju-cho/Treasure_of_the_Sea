using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingMouse_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseTest = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseTest = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseTest = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
