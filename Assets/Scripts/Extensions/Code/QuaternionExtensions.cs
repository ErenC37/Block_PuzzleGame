using System.Text;
using UnityEngine;

public static class QuaternionExtensions
{
    /* ------------------------------------------ */

    private static StringBuilder _builder, _tempBuilder;

    /* ------------------------------------------ */

    public static Quaternion Parse(this Quaternion original, string value, char character = '#')
    {
        string[] tempData = value.Split(character);

        return new Quaternion()
        {
            eulerAngles = new Vector3(float.Parse(tempData[0]), float.Parse(tempData[1]), float.Parse(tempData[2]))
        };
    }

    public static string Zip(this Quaternion original, char character = '#')
    {
        if (_builder == null)
            _builder = new StringBuilder();
        else
            _builder.Clear();


        _builder.Append(GetEconomicVersionOfIt(original.eulerAngles.x.ToString()));

        _builder.Append(character);

        _builder.Append(GetEconomicVersionOfIt(original.eulerAngles.y.ToString()));

        _builder.Append(character);

        _builder.Append(GetEconomicVersionOfIt(original.eulerAngles.z.ToString()));

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
                {
                    done = true;
                }
            }

            _tempBuilder.Append(tempString[x]);
        }

        return _tempBuilder.ToString();
    }

    /* ------------------------------------------ */
}