using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLoadLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //主要要求本程序必须是x64
            //项目属性-生成-目标平台-x64
            //var module = DLLWrapper.Load(@"D:\OtherDocuments\GuidelinesVS\OxyPlot_Test\bin\Debug\TSDPointClusterTestEvaluationIndexCalculation\TSDPointClusterTestEvaluationIndexCalculation.dll");

            DLLWrapper.Release(module);
        }
    }
}
