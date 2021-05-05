using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FSDatabase
{
    public class CsvParser : IEnumerable<string>
    {
        internal enum TokenType
        {
            Comma,
            Quote,
            Value
        }

        private readonly StreamTokenizer _tokenizer;

        public CsvParser(Stream data)
        {
            _tokenizer = new StreamTokenizer(new StreamReader(data));
        }

        public CsvParser(string data)
        {
            _tokenizer = new StreamTokenizer(new StringReader(data));
        }

        public IEnumerator<string> GetEnumerator()
        {
            var inQuote = false;
            var result = new StringBuilder();

            foreach (var token in _tokenizer)
                switch (token.Type)
                {
                    case TokenType.Comma:
                        if (inQuote)
                        {
                            result.Append(token.Value);
                        }
                        else
                        {
                            yield return result.ToString();
                            result.Length = 0;
                        }

                        break;
                    case TokenType.Quote:
                        // Toggle quote state
                        inQuote = !inQuote;
                        break;
                    case TokenType.Value:
                        result.Append(token.Value);
                        break;
                    default:
                        throw new InvalidOperationException("Unknown token type: " + token.Type);
                }

            if (result.Length > 0) yield return result.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        internal class Token
        {
            public Token(TokenType type, string value)
            {
                Value = value;
                Type = type;
            }

            public string Value { get; set; }

            public TokenType Type { get; set; }
        }

        internal class StreamTokenizer : IEnumerable<Token>
        {
            private readonly TextReader _reader;

            public StreamTokenizer(TextReader reader)
            {
                _reader = reader;
            }

            public IEnumerator<Token> GetEnumerator()
            {
                string line;
                var value = new StringBuilder();

                while ((line = _reader.ReadLine()) != null)
                {
                    foreach (var c in line)
                        switch (c)
                        {
                            //case '\'':
                            case '"':
                                if (value.Length > 0)
                                {
                                    yield return new Token(TokenType.Value, value.ToString());
                                    value.Length = 0;
                                }

                                yield return new Token(TokenType.Quote, c.ToString());
                                break;
                            case ',':
                                if (value.Length > 0)
                                {
                                    yield return new Token(TokenType.Value, value.ToString());
                                    value.Length = 0;
                                }

                                yield return new Token(TokenType.Comma, c.ToString());
                                break;
                            default:
                                value.Append(c);
                                break;
                        }

                    // Thanks, dpan
                    if (value.Length > 0) yield return new Token(TokenType.Value, value.ToString());
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }
}