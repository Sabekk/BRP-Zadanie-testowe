using UnityEngine;
using UnityEngine.UI;

public static class ScrollHelpers
{
    private static readonly Vector3[] _corners = new Vector3[4];

    /// <summary>
    /// Przesuwa ScrollRect, aby 'target' był w pełni widoczny w viewport (z paddingiem).
    /// Działa z Mask/RectMask2D, ContentSizeFitter, GridLayoutGroup, itp.
    /// </summary>
    public static void ScrollToMakeVisible(this ScrollRect sr, RectTransform target, Vector2 padding)
    {
        if (!sr || !target || !sr.content) return;

        // upewnij się, że layout jest przeliczony (ważne przy ContentSizeFitter)
        Canvas.ForceUpdateCanvases();
        sr.StopMovement();
        sr.velocity = Vector2.zero;

        var content = sr.content;
        var viewport = sr.viewport ? sr.viewport : (RectTransform)sr.transform;

        // rogi targetu w przestrzeni viewportu
        target.GetWorldCorners(_corners);
        for (int i = 0; i < 4; i++)
            _corners[i] = viewport.InverseTransformPoint(_corners[i]);

        float minX = Mathf.Min(_corners[0].x, _corners[1].x, _corners[2].x, _corners[3].x);
        float maxX = Mathf.Max(_corners[0].x, _corners[1].x, _corners[2].x, _corners[3].x);
        float minY = Mathf.Min(_corners[0].y, _corners[1].y, _corners[2].y, _corners[3].y);
        float maxY = Mathf.Max(_corners[0].y, _corners[1].y, _corners[2].y, _corners[3].y);

        var vRect = viewport.rect; // lokalny rect viewportu (środek w (0,0))
        float dx = 0f, dy = 0f;

        if (sr.horizontal)
        {
            float leftLimit = vRect.xMin + padding.x;
            float rightLimit = vRect.xMax - padding.x;
            if (maxX > rightLimit) dx = maxX - rightLimit; // za bardzo w prawo → przesuń w lewo
            else if (minX < leftLimit) dx = minX - leftLimit;  // za bardzo w lewo  → przesuń w prawo (dx ujemny)
        }

        if (sr.vertical)
        {
            float bottomLimit = vRect.yMin + padding.y;
            float topLimit = vRect.yMax - padding.y;
            if (minY < bottomLimit) dy = minY - bottomLimit; // za nisko  → przesuń w górę (dy ujemny)
            else if (maxY > topLimit) dy = maxY - topLimit;    // za wysoko → przesuń w dół
        }

        if (dx == 0f && dy == 0f) return;

        // dx/dy są w przestrzeni viewportu → przelicz na przestrzeń contentu
        Vector2 deltaInContent = (Vector2)content.InverseTransformVector(viewport.TransformVector(new Vector3(dx, dy, 0f)));
        // przesunięcie contentu w przeciwną stronę niż „wystawanie”
        content.anchoredPosition -= deltaInContent;
    }
}
