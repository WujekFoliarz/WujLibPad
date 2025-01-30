// pch.h: This is a precompiled header file.
// Files listed below are compiled only once, improving build performance for future builds.
// This also affects IntelliSense performance, including code completion and many code browsing features.
// However, files listed here are ALL re-compiled if any one of them is updated between builds.
// Do not add files here that you will be updating frequently as this negates the performance advantage.

#ifndef PCH_H
#define PCH_H

// add headers that you want to pre-compile here
#include "framework.h"
#include <Propvarutil.h>
#include <chrono>
#include <iostream>
#include <mmdeviceapi.h>
#include <thread>
#include <vector>
#pragma comment(lib, "Propsys.lib")
#pragma comment (lib, "Setupapi.lib")
#include <atlbase.h>
#include <functiondiscoverykeys_devpkey.h>
#include <algorithm>
#include <array>
#include <cfgmgr32.h>
#include <cstdint>
#include <regex>
#include <setupapi.h>
#include <sstream>
#include <string>
#include <usbiodef.h>
#include <codecvt>
#include <map>
#include "include/hidapi.h"
#include "DualSenseAPI.h"

#endif //PCH_H
