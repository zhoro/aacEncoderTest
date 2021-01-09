using System;
using System.IO;
using System.Reflection;

namespace accEncoderTest
{
    class Program
    {
        private static readonly int _sampleRate = 8000;

        private static readonly int _bitRate = 16000;

        private static readonly int _channels = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("AAC Encoder test!");
            DllMap.Register(Assembly.GetExecutingAssembly());
            if (!AacEncoder.EncoderInit(_sampleRate, _channels, _bitRate))
            {
                Console.WriteLine("AAC Encoder NOT initialized!");
                return;
            }

            var inputBufferSize = AacEncoder.GetInputBufferSize();
            var outputBufferMaxSize = AacEncoder.GetMaxOutputBufferSize();
            if (inputBufferSize <= 0 || outputBufferMaxSize <= 0)
            {
                Console.WriteLine("AAC Encoder inputBufferSize or outputBufferMaxSize not initialized!");
                return;
            }

            var inputBuffer = new byte[inputBufferSize];
            var outputBuffer = new byte[outputBufferMaxSize];
            var outputPtr = InteropHelper.AllocByteArray(outputBuffer);

            var stream = new FileStream("input.wav", FileMode.Open, FileAccess.Read);
            var aacStream = new FileStream("output.aac", FileMode.Create, FileAccess.Write);

            int read;
            while ((read = stream.Read(inputBuffer, 0, inputBufferSize)) > 0)
            {
                var inputPtr = InteropHelper.AllocByteArray(inputBuffer);
                var size = AacEncoder.EncodeBuffer(inputPtr, read, outputPtr, outputBufferMaxSize);
                InteropHelper.FreeObjectArray(inputPtr, typeof(byte), inputBufferSize);

                if (size <= 0) continue;
                if (InteropHelper.ParsePtrToByteArray(outputPtr, size, out var result))
                {
                    aacStream.Write(result);
                }
            }

            aacStream.Close();
        }
    }
}