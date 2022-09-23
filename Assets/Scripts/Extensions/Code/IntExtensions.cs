using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
    /* ------------------------------------------ */

    public static int NonNegative(this int origin)
    {
        if (origin < 0)
            return 0;
        else
            return origin;
    }

    /* ------------------------------------------ */
}