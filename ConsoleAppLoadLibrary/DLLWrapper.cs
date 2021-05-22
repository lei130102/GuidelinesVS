using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLoadLibrary
{
    /// <summary>
    /// DLLWrapper
    /// </summary>
    internal class DLLWrapper
    {
        /// <summary>
        /// API GetLastError
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        /// <summary>
        /// API LoadLibraryEx
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <param name="hReservedNull"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        /// <summary>
        /// API GetProcAddress
        /// 特别注意CharSet=CharSet.Ansi的指定
        /// https://stackoverflow.com/questions/3754264/c-sharp-getprocaddress-returns-zero
        /// GetProcAddress only comes in an ANSI flavor, hence we help the runtime by telling
        /// it to always use ANSI when marshalling the string parameter. We also prevent the 
        /// runtime looking for a non-existent GetProcAddressA, because the default for C# is 
        /// to set ExactSpelling to false.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="funcname"></param>
        /// <returns></returns>
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        /// <summary>
        ///  API FreeLibrary
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        //[DllImport("Kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        //private static extern int FreeLibrary(int handle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        ///<summary>
        /// 通过非托管函数名转换为对应的委托
        ///</summary>
        ///<param name="dllModule"> 通过 LoadLibrary 获得的 DLL 句柄 </param>
        ///<param name="functionName"> 非托管函数名 </param>
        ///<param name="t"> 对应的委托类型 </param>
        ///<returns> 委托实例，可强制转换为适当的委托类型 </returns>
        public static Delegate GetFunctionAddress(IntPtr dllModule, string functionName, Type t)
        {
            IntPtr address = GetProcAddress(dllModule, functionName);
            if (address == IntPtr.Zero)
            {
                var errCode = GetLastError();
                return null;
            }
            else
                return Marshal.GetDelegateForFunctionPointer(address, t);
        }

        ///<summary>
        /// 将表示函数地址的 intPtr 实例转换成对应的委托
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(IntPtr address, Type t)
        {
            if (address == IntPtr.Zero)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(address, t);
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        public static IntPtr Load(string lpFileName)
        {
            if (File.Exists(lpFileName))
            {
                var hReservedNull = IntPtr.Zero;
                var dwFlags = LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH;

                var result = LoadLibraryEx(lpFileName, hReservedNull, dwFlags);

                var errCode = GetLastError();
                //LogHelper.Info($"LoadSDK Result:{result}, ErrorCode: {errCode}");

                return result;
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 释放sdk
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static int Release(IntPtr handle)
        {
            try
            {
                if (handle != IntPtr.Zero)
                {
                    //LogHelper.Info($"FreeLibrary handle：{handle}");
                    var result = FreeLibrary(handle);
                    var errCode = GetLastError();
                    //LogHelper.Info($"FreeLibrary Result:{result}, ErrorCode: {errCode}");
                    return 0;
                }
                return -1;
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
                return -1;
            }
        }
    }

    /// <summary>
    /// LoadLibraryFlags
    /// </summary>
    public enum LoadLibraryFlags : uint
    {
        /// <summary>
        /// DONT_RESOLVE_DLL_REFERENCES
        /// </summary>
        DONT_RESOLVE_DLL_REFERENCES = 0x00000001,

        /// <summary>
        /// LOAD_IGNORE_CODE_AUTHZ_LEVEL
        /// </summary>
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,

        /// <summary>
        /// LOAD_LIBRARY_AS_DATAFILE
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE = 0x00000002,

        /// <summary>
        /// LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,

        /// <summary>
        /// LOAD_LIBRARY_AS_IMAGE_RESOURCE
        /// </summary>
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,

        /// <summary>
        /// LOAD_LIBRARY_SEARCH_APPLICATION_DIR
        /// </summary>
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,

        /// <summary>
        /// LOAD_LIBRARY_SEARCH_DEFAULT_DIRS
        /// </summary>
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,

        /// <summary>
        /// LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR
        /// </summary>
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,

        /// <summary>
        /// LOAD_LIBRARY_SEARCH_SYSTEM32
        /// </summary>
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,

        /// <summary>
        /// LOAD_LIBRARY_SEARCH_USER_DIRS
        /// </summary>
        LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,

        /// <summary>
        /// LOAD_WITH_ALTERED_SEARCH_PATH
        /// </summary>
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
    }
}
