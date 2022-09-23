using UnityEngine.UI;

public static class UIButtonExtensions
{
    /* ------------------------------------------ */

    public static void Click(this Button origin)
    {
        origin.onClick?.Invoke();
    }

    /* ------------------------------------------ */
}