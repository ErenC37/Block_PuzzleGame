using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Vector3Extensions
{
    /* ------------------------------------------ */

    private static StringBuilder _builder, _tempBuilder;

    /* ------------------------------------------ */

    public static Vector3 Parse(this Vector3 original, string value, char character = '#')
    {
        string[] tempData = value.Split(character);

        return new Vector3()
            { x = float.Parse(tempData[0]), y = float.Parse(tempData[1]), z = float.Parse(tempData[2]) };
    }

    public static string Zip(this Vector3 original, char character = '#')
    {
        if (_builder == null)
            _builder = new StringBuilder();
        else
            _builder.Clear();

        _builder.Append(GetEconomicVersionOfIt(original.x.ToString()));

        _builder.Append(character);

        _builder.Append(GetEconomicVersionOfIt(original.y.ToString()));

        _builder.Append(character);

        _builder.Append(GetEconomicVersionOfIt(original.z.ToString()));

        return _builder.ToString();
    }

    /* ------------------------------------------ */

    private static string GetEconomicVersionOfIt(string tempString)
    {
        if (_tempBuilder == null)
            _tempBuilder = new StringBuilder();
        else
            _tempBuilder.Clear();

        bool tempPermission = false, done = false;
        int tempCounter = 0;

        for (int x = 0; x < tempString.Length; x++)
        {
            if (done)
                break;

            if (tempString[x] == '.' || tempString[x] == ',' || tempPermission)
            {
                tempPermission = true;

                tempCounter++;

                if (tempCounter >= 3)
                    done = true;
            }

            _tempBuilder.Append(tempString[x]);
        }

        return _tempBuilder.ToString();
    }

    /* ------------------------------------------ */
}