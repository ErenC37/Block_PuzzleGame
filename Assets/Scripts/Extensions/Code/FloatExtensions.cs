using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions
{
    /* ------------------------------------------ */

    public static float NonNegative(this float origin)
    {
        if (origin < 0)
            return 0;
        else
            return origin;
    }

    /* ------------------------------------------ */
}