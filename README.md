# Why use this library?
- Consistant connection.
- Easy-to-use haptic feedback designed for external game mods, with real-time editable audio (sound/pitch).

# How to use in C++:
- Clone the repo
- Compile the project
- Download [DualSenseAPI.h](https://github.com/WujekFoliarz/WujLibPad/blob/master/WujLibPad/DualSenseAPI.h) and include it
- Download [hidapi.dll](https://github.com/libusb/hidapi/releases) and put it in your release folder
- Put WujLibPad.dll in your bin folder

# How to use in C#:
- Clone the repo
- Compile the project
- Download [WujLibPad-CSharp.cs](https://github.com/WujekFoliarz/WujLibPad/blob/master/Bindings/CSharp.cs) and include it in your C# project
- Put WujLibPad.dll and [hidapi.dll](https://github.com/libusb/hidapi/releases) in your bin folder

# Examples
```
// C# Example

var dualsense = WujLibPad.Dualsense_Create(); // Find first available controller

byte[] leftTriggerForces = { 10, 255, 40, 0, 0, 0, 0, 0, 0, 0, 0 };
while(true)
{
    if(WujLibPad.Dualsense_IsConnected(dualsense) == false) {
        WujLibPad.Dualsense_Connect(dualsense);
    }

    WujLibPad.Dualsense_InitializeAudioEngine(dualsense);
    WujLibPad.Dualsense_Read(dualsense);

    WujLibPad.Dualsense_SetPlayerLED(dualsense, 1); // One LED light
    WujLibPad.Dualsense_SetLightbarColor(dualsense, 0, 1, 255); // Blue color
    WujLibPad.Dualsense_LoadSound(dualsense, "rumble", @"haptics\rumble_mid_4c.wav"); // Load wav file, will be skipped if already loaded (Must be a 4 channel 48KHz IEEE Float PCM)
    WujLibPad.Dualsense_PlayHaptics(dualsense, "rumble", true, true); // Play the sound in loop
    WujLibPad.Dualsense_SetLeftTrigger(dualsense, (byte)WujLibPad.TriggerMode.Pulse_B, leftTriggerForces); // Set automatic gun effect for right trigger

    WujLibPad.ButtonState btnState = new WujLibPad.ButtonState();
    WujLibPad.Dualsense_GetButtonState(dualsense, ref btnState);
    if(btnState.cross == true) {
        Console.WriteLine("Cross clicked!");
    }

    WujLibPad.Dualsense_Write(dualsense);
}
```
