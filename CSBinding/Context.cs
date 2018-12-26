using System;
using System.Runtime.InteropServices;

namespace LLVMCS
{
    public class Context : IDisposable
    {
        [DllImport(DLL.LLVM_DLL_NAME, CallingConvention = DLL.LLVM_DLL_CALLING_CONVENTION, CharSet = DLL.LLVM_DLL_CHAR_SET)]
        private unsafe extern static void* llvm_create_context();

        [DllImport(DLL.LLVM_DLL_NAME, CallingConvention = DLL.LLVM_DLL_CALLING_CONVENTION, CharSet = DLL.LLVM_DLL_CHAR_SET)]
        private unsafe extern static void llvm_delete_context(void* ptr);

        internal unsafe void* instance;

        public Context()
        {
            unsafe
            {
                instance = llvm_create_context();
            }
        }

        ~Context()
        {
            Dispose();
        }

        public void Dispose()
        {
            unsafe
            {
                if (instance != null)
                {
                    llvm_delete_context(instance);
                }
                instance = null;
            }
        }
    }
}
