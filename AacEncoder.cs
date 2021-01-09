using System;
using System.Runtime.InteropServices;
using System.Security;

namespace accEncoderTest
{
    public class AacEncoder
    {
        public struct NativeMethods
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport("libfdk-aac", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "encoder_init")]
            [return: MarshalAsAttribute(UnmanagedType.I1)]
            internal static extern bool EncoderInit_0(int samplerate, int channels, int bitrate);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("libfdk-aac", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "encoder_release")]
            internal static extern void EncoderRelease_0();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("libfdk-aac", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "get_input_buffer_size")]
            internal static extern int GetInputBufferSize_0();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("libfdk-aac", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "get_max_output_buffer_size")]
            internal static extern int GetMaxOutputBufferSize_0();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("libfdk-aac", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "encode_buffer")]
            internal static extern int EncodeBuffer_0(IntPtr inbuf, int len, IntPtr outbuf, int outsize);
        }

        public static bool EncoderInit(int samplerate, int channels, int bitrate)
        {
            var __ret = NativeMethods.EncoderInit_0(samplerate, channels, bitrate);
            return __ret;
        }

        public static void EncoderRelease()
        {
            NativeMethods.EncoderRelease_0();
        }

        public static int GetInputBufferSize()
        {
            var __ret = NativeMethods.GetInputBufferSize_0();
            return __ret;
        }

        public static int GetMaxOutputBufferSize()
        {
            var __ret = NativeMethods.GetMaxOutputBufferSize_0();
            return __ret;
        }

        public static int EncodeBuffer(IntPtr inbuf, int len, IntPtr outbuf, int outsize)
        {
            var __ret = NativeMethods.EncodeBuffer_0(inbuf, len, outbuf, outsize);
            return __ret;
        }
    }
}