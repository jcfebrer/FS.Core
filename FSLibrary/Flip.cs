namespace FSLibrary
{
    /// <summary>
    /// Clase que realiza el efecto de mostrar una cadena del revés
    /// </summary>
    public class Flip
    {
        /// <summary>
        /// Devuelve la cadena volteada.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string FlipString(string text)
        {
            text = text.ToLower();
            var last = text.Length - 1;
            var result = "";
            for (var i = last; i >= 0; --i) result += FlipChar(text.Substring(i, 1).ToCharArray()[0]);
            return result;
        }

        private static char FlipChar(char c)
        {
            if (c == 'a')
                return '\u0250';
            if (c == '\u0250')
                return 'a';
            if (c == 'b')
                return 'q';
            if (c == 'c')
                return '\u0254';
            if (c == '\u0254')
                return 'c';
            if (c == 'd')
                return 'p';
            if (c == 'e')
                return '\u04d9';
            if (c == '\u04d9')
                return 'e';
            if (c == 'f')
                return '\u025F';
            if (c == '\u025F')
                return 'f';
            if (c == 'g')
                return '\u0183';
            if (c == '\u0183')
                return 'g';
            if (c == 'h')
                return '\u0265';
            if (c == '\u0265')
                return 'h';
            if (c == 'i')
                return '!';
            if (c == '!')
                return 'i';
            if (c == 'j')
                return '\u027E';
            if (c == '\u027E')
                return 'j';
            if (c == 'k')
                return '\u029E';
            if (c == '\u029E')
                return 'k';
            if (c == 'l')
                return '|';
            if (c == '|')
                return 'l';
            if (c == 'm')
                return '\u026F';
            if (c == '\u026F')
                return 'm';
            if (c == 'n')
                return 'u';
            if (c == 'o')
                return 'o';
            if (c == 'p')
                return 'd';
            if (c == 'q')
                return 'b';
            if (c == 'r')
                return '\u0279';
            if (c == '\u0279')
                return 'r';
            if (c == 's')
                return 's';
            if (c == 't')
                return '\u0287';
            if (c == '\u0287')
                return 't';
            if (c == 'u')
                return 'n';
            if (c == 'v')
                return '\u028C';
            if (c == '\u028C')
                return 'v';
            if (c == 'w')
                return '\u028D';
            if (c == '\u028D')
                return 'w';
            if (c == 'x')
                return 'x';
            if (c == 'y')
                return '\u028E';
            if (c == '\u028E')
                return 'y';
            if (c == 'z')
                return 'z';
            if (c == '[')
                return ']';
            if (c == ']')
                return '[';
            if (c == '(')
                return ')';
            if (c == ')')
                return '(';
            if (c == '{')
                return '}';
            if (c == '}')
                return '{';
            if (c == '?')
                return '\u00BF';
            if (c == '\u00BF')
                return '?';
            if (c == '!')
                return '\u00A1';
            if (c == '\u00A1')
                return '!';
            if (c == '\'')
                return ',';
            if (c == ',')
                return '\'';
            if (c == '1')
                return '\u21C2';
            if (c == '\u21C2')
                return '1';
            if (c == '2')
                return '\u1105';
            if (c == '\u1105')
                return '2';
            if (c == '3')
                return '\u1110';
            if (c == '\u1110')
                return '3';
            if (c == '4')
                return '\u3123';
            if (c == '\u3123')
                return '4';
            if (c == '5')
                return '\u078E';
            if (c == '\u078E')
                return '5';
            if (c == '6')
                return '9';
            if (c == '7')
                return '\u3125';
            if (c == '\u3125')
                return '7';
            if (c == '8')
                return '8';
            if (c == '9')
                return '6';
            if (c == '0') return '0';
            return c;
        }
    }
}