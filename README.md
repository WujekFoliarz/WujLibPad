# Why use this library?
- Consistant connection.
- Easy-to-use haptic feedback designed for external game mods, with real-time editable audio (sound/pitch).

# Examples
```
// C# Example

var dualsense = WujLibPad.Dualsense_Create(); // Find first available controller

byte[] leftTriggerForces = { 10, 255, 40, 0, 0, 0, 0, 0, 0, 0, 0 };
while(true)
{
    if(Controller.IsConnected(dualsense) == false) {
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

```
// C++ Example

Dualsense controller = Dualsense();

while(true)
{
    if(controller.Connected == false) {
        controller.Reconnect();
    }

    controller.InitializeAudioEngine();
    controller.Read();

    controller.SetPlayerLED(1); // One LED light
    controller.SetLightbarColor(0, 1, 255); // Blue color
    controller.LoadSound("rumble", "haptics\\rumble_mid_4c.wav"); // Load wav file, will be skipped if already loaded (Must be a 4 channel 48KHz IEEE Float PCM)
    controller.PlayHaptics("rumble", true, true); // Play the sound in loop
    controller.SetLeftTrigger(TriggerMode::Pulse_B, 10, 255, 40, 0, 0, 0, 0, 0, 0, 0, 0); // Set automatic gun effect for right trigger

    if(controller.State.cross == true){
        std::cout << "Cross clicked!" << endl;
    }

    controller.Write();
}
```
