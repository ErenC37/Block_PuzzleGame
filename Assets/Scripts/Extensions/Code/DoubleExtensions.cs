using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoubleExtensions
{
    /* ------------------------------------------ */

    public static double NonNegative(this double origin)
    {
        if (origin < 0)
            return 0;
        else
            return origin;
    }

    /* ------------------------------------------ */
}