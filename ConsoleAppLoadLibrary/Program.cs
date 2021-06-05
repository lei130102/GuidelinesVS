using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLoadLibrary
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate int Func_int_int(int param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate char Func_char_char(char param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void Func_void_char_ptr1(string param);
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void Func_void_char_ptr2([MarshalAs(UnmanagedType.LPStr)] string param);

    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void Func_void_wchar_t_ptr1(string param);
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    public delegate void Func_void_wchar_t_ptr2(string param);

    class Program
    {
        static void Main(string[] args)
        {
            //主要要求本程序必须是x64
            //项目属性-生成-目标平台-x64
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(baseDirectory);

            //
            var module = DLLWrapper.Load(baseDirectory + @"..\..\..\x64\Release\cpp_dll.dll");

            ////
            {
                Func_int_int func_int_int = (Func_int_int)DLLWrapper.GetFunctionAddress(module, "func_int_int", typeof(Func_int_int));
                if (func_int_int == null)
                {
                    return;
                }
                int result = func_int_int(10);
                Console.WriteLine(result);
            }

            {
                Func_char_char func_char_char = (Func_char_char)DLLWrapper.GetFunctionAddress(module, "func_char_char", typeof(Func_char_char));
                if (func_char_char == null)
                {
                    return;
                }
                char result = func_char_char('a');
                Console.WriteLine(result);
            }

            {
                Func_void_char_ptr1 func_void_char_ptr1 = (Func_void_char_ptr1)DLLWrapper.GetFunctionAddress(module, "func_void_char_ptr", typeof(Func_void_char_ptr1));
                if (func_void_char_ptr1 == null)
                {
                    return;
                }
                func_void_char_ptr1("编码");//在exe所在目录生成文件，文件内容正常，使用ansi编码
            }

            {
                Func_void_char_ptr2 func_void_char_ptr2 = (Func_void_char_ptr2)DLLWrapper.GetFunctionAddress(module, "func_void_char_ptr", typeof(Func_void_char_ptr2));
                if (func_void_char_ptr2 == null)
                {
                    return;
                }
                func_void_char_ptr2("编码");//在exe所在目录生成文件，文件内容正常，使用ansi编码
            }

            {
                Func_void_wchar_t_ptr1 func_void_wchar_t_ptr1 = (Func_void_wchar_t_ptr1)DLLWrapper.GetFunctionAddress(module, "func_void_wchar_t_ptr", typeof(Func_void_wchar_t_ptr1));
                if (func_void_wchar_t_ptr1 == null)
                {
                    return;
                }
                func_void_wchar_t_ptr1("编码");//在exe所在目录生成文件，文件内容正常，使用ansi编码
            }

            //{
            //    Func_void_wchar_t_ptr2 func_void_wchar_t_ptr2 = (Func_void_wchar_t_ptr2)DLLWrapper.GetFunctionAddress(module, "func_void_wchar_t_ptr", typeof(Func_void_wchar_t_ptr2));
            //    if (func_void_wchar_t_ptr2 == null)
            //    {
            //        return;
            //    }
            //    func_void_wchar_t_ptr2("编码");//无法使用，乱码
            //}









            //
            DLLWrapper.Release(module);
        }
    }
}
