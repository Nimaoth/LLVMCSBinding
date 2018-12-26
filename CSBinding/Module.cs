using System.Runtime.InteropServices;
using System.Text;

namespace LLVMCS
{
    public class Module
    {
        [DllImport(DLL.LLVM_DLL_NAME, CallingConvention = DLL.LLVM_DLL_CALLING_CONVENTION, CharSet = DLL.LLVM_DLL_CHAR_SET)]
        private unsafe extern static void* llvm_create_module(string name, void* contextPtr);

        [DllImport(DLL.LLVM_DLL_NAME, CallingConvention = DLL.LLVM_DLL_CALLING_CONVENTION, CharSet = DLL.LLVM_DLL_CHAR_SET)]
        private unsafe extern static void llvm_delete_module(void* ptr);

        [DllImport(DLL.LLVM_DLL_NAME, CallingConvention = DLL.LLVM_DLL_CALLING_CONVENTION, CharSet = DLL.LLVM_DLL_CHAR_SET)]
        private unsafe extern static void llvm_module_set_target_triple(void* ptr, string name);

        [DllImport(DLL.LLVM_DLL_NAME, CallingConvention = DLL.LLVM_DLL_CALLING_CONVENTION, CharSet = DLL.LLVM_DLL_CHAR_SET)]
        private unsafe extern static void llvm_module_get_target_triple(void* ptr, sbyte** data, int* size);


        unsafe internal void* instance;
        private Context context;

        public string Name { get; }

        public Module(string name)
        {
            this.Name = name;
            context = new Context();

            unsafe
            {
                instance = llvm_create_module(name, context.instance);
            }
        }

        public Module(string name, Context context)
        {
            this.Name = name;
            this.context = context;
            unsafe
            {
                instance = llvm_create_module(name, context.instance);
            }
        }

        ~Module()
        {
            Dispose();
            context.Dispose();
        }

        public void Dispose()
        {
            unsafe
            {
                if (instance != null)
                {
                    llvm_delete_module(instance);
                }
                instance = null;
            }
        }

        public void SetTargetTriple(string targetTriple)
        {
            unsafe
            {
                llvm_module_set_target_triple(instance, targetTriple);
            }
        }

        public string GetTargetTriple()
        {
            unsafe
            {
                sbyte* data = null;
                int length = 0;
                llvm_module_get_target_triple(instance, &data, &length);
                
                var targetTriple = new string(data, 0, length, Encoding.ASCII);
                return targetTriple;
            }
        }
    }
}
