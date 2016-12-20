using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace App_Code
{
    /// <summary>
    ///获取网上Mac物理地址
    ///来源： http://blog.csdn.net/X_Craft/article/details/4398011
    ///网卡的MAC地址是可以伪造的，此类是使用　DeviceIoControl API获取固件中的MAC地址
    /// </summary>
    public class MacPhysicalAddress
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DeviceIoControl(
            IntPtr HDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            ref uint lpBytesReturned,
            IntPtr lpOverlapped);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);
        /// <summary>
        /// 获取网卡地址
        /// </summary>
        /// <param name="NicId">网卡ID</param>
        /// <returns></returns>
        public string GetNicAddress(string NicId)
        {
            System.IntPtr hDevice = CreateFile("\\\\.\\" + NicId,
                                                0x80000000 | 0x40000000,
                                                0,
                                                IntPtr.Zero,
                                                3,
                                                4,
                                                IntPtr.Zero
                                                );
            if (hDevice.ToInt32() == -1)
            {
                return string.Empty;
            }
            uint Len = 0;
            IntPtr Buffer = Marshal.AllocHGlobal(256);

            Marshal.WriteInt32(Buffer, 0x01010101);
            if (!DeviceIoControl(hDevice,
                              0x170002,
                              Buffer,
                              4,
                              Buffer,
                              256,
                              ref Len,
                              IntPtr.Zero))
            {
                Marshal.FreeHGlobal(Buffer);
                CloseHandle(hDevice);
                return string.Empty;
            }
            byte[] macBytes = new byte[6];
            Marshal.Copy(Buffer, macBytes, 0, 6);
            Marshal.FreeHGlobal(Buffer);
            CloseHandle(hDevice);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < macBytes.Length; i++)
            {
                sb.Append(macBytes[i].ToString("X2"));
                if (i != macBytes.Length - 1)
                {
                    sb.Append(":");
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取所有网卡地址
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllNic()
        {
            List<string> nics = new List<string>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic == null || nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet || string.IsNullOrEmpty(nic.Id)) continue;
                nics.Add(nic.Id);
            }
            return nics;
        }
    }
}