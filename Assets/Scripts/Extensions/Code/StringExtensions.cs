public static class StringExtensions
{
    /* ------------------------------------------ */

    #region Public-Methods

    public static string ClearSpecialChars(this string original)
    {
        original = Helper(original, '"');
        original = Helper(original, '!');
        original = Helper(original, '^');
        original = Helper(original, '+');
        original = Helper(original, '*');
        original = Helper(original, '%');
        original = Helper(original, '&');
        original = Helper(original, '/');
        original = Helper(original, '(');
        original = Helper(original, ')');
        original = Helper(original, '=');
        original = Helper(original, '?');
        original = Helper(original, '_');
        original = Helper(original, 'é');
        original = Helper(original, '<');
        original = Helper(original, '>');
        original = Helper(original, ':');
        original = Helper(original, ';');
        original = Helper(original, '~');
        original = Helper(original, '¨');
        original = Helper(original, 'æ');
        original = Helper(original, '€');
        original = Helper(original, 'ß');
        original = Helper(original, '´');
        original = Helper(original, '£');
        original = Helper(original, '#');
        original = Helper(original, '$');
        original = Helper(original, '½');
        original = Helper(original, '{');
        original = Helper(original, '[');
        original = Helper(original, ']');
        original = Helper(original, '}');
        original = Helper(original, '|');

        return original;
    }

    public static string ClearLatinAlphabet(this string original)
    {
        original = HelperForAlphabet(original, "q");
        original = HelperForAlphabet(original, "w");
        original = HelperForAlphabet(original, "e");
        original = HelperForAlphabet(original, "r");
        original = HelperForAlphabet(original, "t");
        original = HelperForAlphabet(original, "y");
        original = HelperForAlphabet(original, "u");
        original = HelperForAlphabet(original, "ı");
        original = HelperForAlphabet(original, "o");
        original = HelperForAlphabet(original, "p");
        original = HelperForAlphabet(original, "ğ");
        original = HelperForAlphabet(original, "ü");
        original = HelperForAlphabet(original, "a");
        original = HelperForAlphabet(original, "s");
        original = HelperForAlphabet(original, "d");
        original = HelperForAlphabet(original, "f");
        original = HelperForAlphabet(original, "g");
        original = HelperForAlphabet(original, "h");
        original = HelperForAlphabet(original, "j");
        original = HelperForAlphabet(original, "k");
        original = HelperForAlphabet(original, "l");
        original = HelperForAlphabet(original, "ş");
        original = HelperForAlphabet(original, "i");
        original = HelperForAlphabet(original, "z");
        original = HelperForAlphabet(original, "x");
        original = HelperForAlphabet(original, "c");
        original = HelperForAlphabet(original, "v");
        original = HelperForAlphabet(original, "b");
        original = HelperForAlphabet(original, "n");
        original = HelperForAlphabet(original, "m");
        original = HelperForAlphabet(original, "ö");
        original = HelperForAlphabet(original, "ç");

        return original;
    }

    public static string ClearNumbers(this string original)
    {
        original = Helper(original, '0');
        original = Helper(original, '1');
        original = Helper(original, '2');
        original = Helper(original, '3');
        original = Helper(original, '4');
        original = Helper(original, '5');
        original = Helper(original, '6');
        original = Helper(original, '7');
        original = Helper(original, '8');
        original = Helper(original, '9');

        return original;
    }

    #endregion

    /* ------------------------------------------ */

    #region Private-Methods

    private static string Helper(string original, char replacedChar)
    {
        while (original.Contains(replacedChar))
            original = original.Replace("" + replacedChar, "");

        return original;
    }

    private static string HelperForAlphabet(string original, string replacedChar)
    {
        replacedChar = replacedChar.ToLower();
        while (original.Contains(replacedChar))
            original = original.Replace("" + replacedChar, "");

        replacedChar = replacedChar.ToUpper();
        while (original.Contains(replacedChar))
            original = original.Replace("" + replacedChar, "");

        return original;
    }

    #endregion

    /* ------------------------------------------ */
}