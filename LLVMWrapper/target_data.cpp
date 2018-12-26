#include "common.h"
#include "llvm\Target\TargetMachine.h"
#include "llvm\IR\Module.h"

DLL_API llvm::DataLayout* llvm_create_data_layout_1(const char* layoutDesc) {
    
}

DLL_API llvm::DataLayout* llvm_create_data_layout_2(llvm::Module* mod) {
    return new llvm::DataLayout(mod);
}

DLL_API void llvm_create_data_delete(llvm::DataLayout* dataLayout) {
    delete dataLayout;
}
