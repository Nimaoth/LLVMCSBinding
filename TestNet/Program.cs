using System;

using LLVMCS;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var module = new Module("test_module");

            module.SetTargetTriple("i386-pc-win32");

            var tt = module.GetTargetTriple();

            var dl = new DataLayout(module);

            dl.Dispose();
            module.Dispose();
            Console.WriteLine("ok");
        }
    }
}
