using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LithUSS
{
    public static class Native
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct @event
        {
            public short Id;
            public short phonviz;
            public int charOffset;
            public int signOffset;
        }

        const string libPath = "LithUSS_x64";
        //const string libPath = "LithUSS_x32";
        //const string libPath = "FullLithUSS_x64.dll";

        [DllImport(libPath, CharSet = CharSet.Ansi, EntryPoint = "initLUSS")]
        public static extern ErrorEnum initLUSS(byte[] szDataDir, byte[] katVoice);

        public static ErrorEnum initLUSS(string szDataDir, string katVoice)
        {
            byte[] text = Encoding1257.Instance.GetBytes(szDataDir);
            byte[] finalArray = new byte[text.Length + 1];
            text.CopyTo(finalArray, 0);
            finalArray[text.Length] = 0;


            byte[] text2 = Encoding1257.Instance.GetBytes(katVoice);
            byte[] finalArray2 = new byte[text2.Length + 1];
            text2.CopyTo(finalArray2, 0);
            finalArray2[text2.Length] = 0;

            return initLUSS(finalArray, finalArray2);
        }

        [DllImport(libPath, CharSet = CharSet.Ansi, EntryPoint = "synthesizeWholeText")]
        private static extern ErrorEnum synthesizeWholeText(byte[] tekstas, IntPtr signbuf, ref uint signbufsize, IntPtr evarr, ref int evarrsize, int greitis, int tonas);

        private static ErrorEnum synthesizeWholeText(string tekstas, out IntPtr signalBuffer, out uint bufferSize, int greitis, int tonas)
        {
            greitis = Helper.Clamp(30, greitis, 300);
            tonas = Helper.Clamp(75, tonas, 133);

            byte[] text = Encoding1257.Instance.GetBytes(tekstas);
            byte[] finalArray = new byte[text.Length + 1];
            text.CopyTo(finalArray, 0);
            finalArray[text.Length] = 0;

            int eillen = finalArray.Length * 2;

            int tmpBufferSize;
            if (greitis >= 100)
            {
                tmpBufferSize = eillen * 1250 * greitis / 100;
            }
            else
            {
                tmpBufferSize = eillen * 1250 * greitis / 150;
            }
            bufferSize = (uint)Math.Max(tmpBufferSize, 2000000);

            int tmpEventSize = Math.Max(1600, eillen);
            int eventSize = tmpEventSize;
            IntPtr eventBuffer = Marshal.AllocHGlobal(eventSize * Marshal.SizeOf(typeof(@event)));
            signalBuffer = Marshal.AllocHGlobal((int)bufferSize * Marshal.SizeOf(typeof(short)));

            var inter = synthesizeWholeText(finalArray, signalBuffer, ref bufferSize, eventBuffer, ref eventSize, greitis, tonas);
            Marshal.FreeHGlobal(eventBuffer);
            return inter;
        }

        public static ErrorEnum synthesizeWholeText(string tekstas, out short[] signalBuffer, int greitis, int tonas)
        {
            var inter = synthesizeWholeText(tekstas, out IntPtr signalPtr, out uint buffersize, greitis, tonas);
            signalBuffer = new short[buffersize];
            Copy(signalPtr, signalBuffer, (int)buffersize);
            Marshal.FreeHGlobal(signalPtr);
            return inter;
        }

        public static ErrorEnum synthesizeWholeText(string tekstas, out byte[] signalBuffer, int greitis, int tonas)
        {
            var inter = synthesizeWholeText(tekstas, out IntPtr signalPtr, out uint buffersize, greitis, tonas);
            signalBuffer = new byte[buffersize*2];
            Copy(signalPtr, signalBuffer, (int)buffersize * 2);
            Marshal.FreeHGlobal(signalPtr);
            return inter;
        }

        public static unsafe void Copy(IntPtr ptrSource, short[] dest, int elements)
        {
            var sourcePtr = (short*)ptrSource;
            for (int i = 0; i < 0 + elements; ++i)
            {
                dest[i] = *sourcePtr++;
            }
        }

        public static unsafe void Copy(IntPtr ptrSource, byte[] dest, int elements)
        {
            var sourcePtr = (byte*)ptrSource;
            for (int i = 0; i < 0 + elements; ++i)
            {
                dest[i] = *sourcePtr++;
            }
        }

    }
}
