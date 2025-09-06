using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class UIHover
{
    public static void Hover(Selectable selectable)
    {
        if (selectable == null)
            return;
        Hover(selectable.gameObject);
    }

    public static void Unhover(Selectable selectable)
    {
        if (selectable == null)
            return;
        Unhover(selectable.gameObject);
    }

    public static void Hover(GameObject gameObject)
    {
        var evSystem = EventSystem.current;
        if (gameObject == null || evSystem == null)
            return;

        if (evSystem.currentSelectedGameObject == gameObject)
            evSystem.SetSelectedGameObject(null);

        var data = new PointerEventData(evSystem);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerEnterHandler);
    }

    public static void Unhover(GameObject gameObject)
    {
        var evSystem = EventSystem.current;
        if (gameObject == null || evSystem == null)
            return;

        var data = new PointerEventData(evSystem);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerExitHandler);
    }
}
