#include <iostream>
#include <Windows.h>

using namespace std;

int main()
{
    if (IsDebuggerPresent() == true) {
        MessageBox(0, L"I Think Somebody is Watching Me...", L"MK", 0);
        cout << "Isolate Me :)";
        return -1;
    }
    else {
        MessageBox(0, L"I Think I'm Alone Now...", L"MK", 0); 
        cout << "Starting Secret Job... :)\n";
    }
    
    return 0;
}

