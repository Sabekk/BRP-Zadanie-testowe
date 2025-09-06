using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class UIButtonHover
{
    public static void Hover(Button button)
    {
        if (button == null || EventSystem.current == null) 
            return;

        if (EventSystem.current.currentSelectedGameObject == button.gameObject)
            EventSystem.current.SetSelectedGameObject(null);

        var data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, data, ExecuteEvents.pointerEnterHandler);
    }

    public static void Unhover(Button button)
    {
        if (button == null || EventSystem.current == null) 
            return;

        var data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, data, ExecuteEvents.pointerExitHandler);
    }
}
