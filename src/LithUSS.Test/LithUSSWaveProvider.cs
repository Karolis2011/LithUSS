using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class LithUSSWaveProvider : IWaveProvider
    {
        private int pos = 0;
        private byte[] _buffer;
        public LithUSSWaveProvider(byte[] buffer)
        {
            _buffer = buffer;
        }


        public WaveFormat WaveFormat => new WaveFormat(22000, 1);

        public int Read(byte[] buffer, int offset, int count)
        {
            
            var realCount = count;
            if(count + pos > _buffer.Length)
            {
                realCount = _buffer.Length - pos;
            }
            Array.Copy(_buffer, pos, buffer, offset, realCount);
            pos += realCount;
            return realCount;
        }
    }
}
