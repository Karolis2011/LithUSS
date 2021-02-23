using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LithUSS
{
    class Encoding1257 : Encoding
    {
        private static Encoding1257 _instance;

        public static Encoding1257 Instance
        {
            get {
                if(_instance == null)
                {
                    _instance = new Encoding1257();
                }
                return _instance;
            }
        }

        private Dictionary<Char, Byte> encMap = new Dictionary<char, byte>()
        {
            ['\u20AC'] = 0x80,
            ['\u201A'] = 0x82,
            ['\u201E'] = 0x84,
            ['\u2026'] = 0x85,
            ['\u2020'] = 0x86,
            ['\u2021'] = 0x87,
            ['\u2030'] = 0x89,
            ['\u2039'] = 0x8B,
            ['\u00A8'] = 0x8D,
            ['\u02C7'] = 0x8E,
            ['\u00B8'] = 0x8F,
            ['\u2018'] = 0x91,
            ['\u2019'] = 0x92,
            ['\u201C'] = 0x93,
            ['\u201D'] = 0x94,
            ['\u2022'] = 0x95,
            ['\u2013'] = 0x96,
            ['\u2014'] = 0x97,
            ['\u2122'] = 0x99,
            ['\u203A'] = 0x9B,
            ['\u00AF'] = 0x9D,
            ['\u02DB'] = 0x9E,
            ['\u00D8'] = 0xA8,
            ['\u0156'] = 0xAA,
            ['\u00C6'] = 0xAF,
            ['\u00F8'] = 0xB8,
            ['\u0157'] = 0xBA,
            ['\u00E6'] = 0xBF,
            ['\u0104'] = 0xC0,
            ['\u012E'] = 0xC1,
            ['\u0100'] = 0xC2,
            ['\u0106'] = 0xC3,
            ['\u0118'] = 0xC6,
            ['\u0112'] = 0xC7,
            ['\u010C'] = 0xC8,
            ['\u0179'] = 0xCA,
            ['\u0116'] = 0xCB,
            ['\u0122'] = 0xCC,
            ['\u0136'] = 0xCD,
            ['\u012A'] = 0xCE,
            ['\u013B'] = 0xCF,
            ['\u0160'] = 0xD0,
            ['\u0143'] = 0xD1,
            ['\u0145'] = 0xD2,
            ['\u014C'] = 0xD4,
            ['\u0172'] = 0xD8,
            ['\u0141'] = 0xD9,
            ['\u015A'] = 0xDA,
            ['\u016A'] = 0xDB,
            ['\u017B'] = 0xDD,
            ['\u017D'] = 0xDE,
            ['\u0105'] = 0xE0,
            ['\u012F'] = 0xE1,
            ['\u0101'] = 0xE2,
            ['\u0107'] = 0xE3,
            ['\u0119'] = 0xE6,
            ['\u0113'] = 0xE7,
            ['\u010D'] = 0xE8,
            ['\u017A'] = 0xEA,
            ['\u0117'] = 0xEB,
            ['\u0123'] = 0xEC,
            ['\u0137'] = 0xED,
            ['\u012B'] = 0xEE,
            ['\u013C'] = 0xEF,
            ['\u0161'] = 0xF0,
            ['\u0144'] = 0xF1,
            ['\u0146'] = 0xF2,
            ['\u014D'] = 0xF4,
            ['\u0173'] = 0xF8,
            ['\u0142'] = 0xF9,
            ['\u015B'] = 0xFA,
            ['\u016B'] = 0xFB,
            ['\u017C'] = 0xFD,
            ['\u017E'] = 0xFE,
            ['\u02D9'] = 0xFF
        };

        private byte GetByte(char chr)
        {
            if (encMap.ContainsKey(chr))
                return encMap[chr];
            else
            {
                if (chr > 0xff)
                    return (byte)' ';
                else
                    return (byte)chr;
            }
        }
        private char GetChar(byte chr)
        {
            if (encMap.ContainsValue(chr))
                return encMap.First(x => x.Value == chr).Key;
            else
                return (char)chr;
        }


        public override int GetByteCount(char[] chars, int index, int count) => chars.Length;

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            for (var i = 0; i < charCount; i++)
            {
                bytes[byteIndex + i] = GetByte(chars[charIndex + i]);
            }
            return charCount;
        }

        public override int GetCharCount(byte[] bytes, int index, int count) => bytes.Length;

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            for (var i = 0; i < byteCount; i++)
            {
                chars[charIndex + i] = GetChar(bytes[byteIndex + i]);
            }
            return byteCount;
        }

        public override int GetMaxByteCount(int charCount) => charCount;

        public override int GetMaxCharCount(int byteCount) => byteCount;
    }
}
