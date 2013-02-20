using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Wlp
{
    public class ProcessMemory : IDisposable
    {
        private Process process;

        public IntPtr BaseAddress
        {
            get { return process.MainModule.BaseAddress; }
        }

        public Process Process
        {
            get { return process; }
        }

        public ProcessMemory(Process process)
        {
            OpenProcess(process);
        }

        public ProcessMemory(int pid)
        {
            OpenProcess(Process.GetProcessById(pid));
        }

        public ProcessMemory(string processName)
        {
            OpenProcess(GetProcessByName(processName));
        }

        private void OpenProcess(Process _process)
        {
            process = _process;
        }

        ~ProcessMemory()
        {
            process.Close();
            process.Dispose();
        }

        private static Process GetProcessByName(string processName)
        {
            var p = Process.GetProcessesByName(processName);

            if (p.Length == 0)
                throw new Exception(String.Format(CultureInfo.InvariantCulture, "{0} isn't running!", processName));

            return p[0];
        }

        public byte[] Read(IntPtr offset, int length)
        {
            var result = new byte[length];
            ReadProcessMemory(process.Handle, offset, result, new IntPtr(length), IntPtr.Zero);
            return result;
        }

        public bool Write(IntPtr offset, byte[] data)
        {
            return WriteProcessMemory(process.Handle, offset, data, new IntPtr(data.Length), IntPtr.Zero);
        }

        public string ReadCString(IntPtr offset, int maxLen)
        {
            return Encoding.UTF8.GetString(Read(offset, maxLen).TakeWhile(ret => ret != 0).ToArray());
        }

        public bool WriteCString(IntPtr offset, string str)
        {
            return Write(offset, Encoding.UTF8.GetBytes(str + '\0'));
        }

        public T Read<T>(IntPtr offset) where T : struct
        {
            byte[] result = new byte[Marshal.SizeOf(typeof(T))];
            ReadProcessMemory(process.Handle, offset, result, new IntPtr(result.Length), IntPtr.Zero);
            GCHandle handle = GCHandle.Alloc(result, GCHandleType.Pinned);
            T returnObject = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return returnObject;
        }

        public T Read<T>(IntPtr baseAddr, params int[] offsets) where T : struct
        {
            IntPtr ptr = Read<IntPtr>(baseAddr);

            if (ptr != IntPtr.Zero)
            {
                for (int i = 0; i < offsets.Length; ++i)
                {
                    if (i == offsets.Length - 1)
                        return Read<T>(ptr + offsets[i]);

                    ptr = Read<IntPtr>(ptr + offsets[i]);
                }
            }

            return default(T);
        }

        public void Write<T>(IntPtr offset, T value) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(value)];
            IntPtr hObj = Marshal.AllocHGlobal(buffer.Length);
            try
            {
                Marshal.StructureToPtr(value, hObj, false);
                Marshal.Copy(hObj, buffer, 0, buffer.Length);
                Write(offset, buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(hObj);
            }
        }

        public void Dispose()
        {
            process.Close();
            process.Dispose();
        }

        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, IntPtr nSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, IntPtr lpNumberOfBytesWritten);
    }
}
