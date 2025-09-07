using UnityEngine;

public static class UIInputGate
{
    private static int _frame;
    private static UiView _latchedTop;
    private static bool _consumed;
    public static bool TryConsume(UiView callerView, bool onlyWhenTop)
    {
        int f = Time.frameCount;

        if (_frame != f)
        {
            _frame = f;
            _latchedTop = GUIController.Instance ? GUIController.Instance.TopView : null;
            _consumed = false;
        }

        if (callerView != null)
        {
            if (onlyWhenTop && callerView != _latchedTop)
                return false;
        }
        if (_consumed)
            return false;

        _consumed = true;
        return true;
    }
}
