﻿namespace Jacobi.Vst.Core
{
    using System;

    public unsafe class VstAudioBuffer : IDirectBufferAccess32
    {
        public VstAudioBuffer(float* buffer, int length, bool canWrite)
        {
            Buffer = buffer;
            SampleCount = length;
            CanWrite = canWrite;
        }

        public int SampleCount { get; private set; }
        public bool CanWrite { get; private set; }
        private float* Buffer { get; set; }

        public float this[int index]
        {
            get
            {
                if (index < 0 || index >= SampleCount)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                        String.Format("The value should lie between '0' and '{0}'.", SampleCount));
                }

                unsafe
                {
                    return Buffer[index];
                }
            }
            set
            {
                if (!CanWrite) throw new InvalidOperationException("Cannot write to the channel.");
                if (index < 0 || index >= SampleCount)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                        String.Format("The value should lie between '0' and '{0}'.", SampleCount));
                }

                unsafe
                {
                    Buffer[index] = value;
                }
            }
        }

        #region IDirectBufferAccess32 Members

        unsafe float* IDirectBufferAccess32.Buffer
        {
            get { return Buffer; }
        }

        #endregion
    }
}
