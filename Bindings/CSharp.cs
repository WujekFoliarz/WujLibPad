using System.Runtime.InteropServices;

public static class WujLibPad
{
    private const string DllName = "WujLibPad";

    public enum TriggerMode : byte
    {
        Off = 0x0,
        Rigid = 0x1,
        Pulse = 0x2,
        Rigid_A = 0x1 | 0x20,
        Rigid_B = 0x1 | 0x04,
        Rigid_AB = 0x1 | 0x20 | 0x04,
        Pulse_A = 0x2 | 0x20,
        Pulse_A2 = 35,
        Pulse_B = 0x2 | 0x04,
        Pulse_B2 = 38,
        Pulse_AB = 39,
        Calibration = 0xFC
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Touchpad
    {
        public int RawTrackingNum;
        [MarshalAs(UnmanagedType.I1)] public bool IsActive;
        public int ID;
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Gyro
    {
        public int X;
        public int Y;
        public int Z;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Accelerometer
    {
        public short X;
        public short Y;
        public short Z;
        public uint SensorTimestamp;
    }

    public enum BatteryState : byte
    {
        POWER_SUPPLY_STATUS_DISCHARGING = 0x0,
        POWER_SUPPLY_STATUS_CHARGING = 0x2,
        POWER_SUPPLY_STATUS_FULL = 0x1,
        POWER_SUPPLY_STATUS_NOT_CHARGING = 0xb,
        POWER_SUPPLY_STATUS_ERROR = 0xf,
        POWER_SUPPLY_TEMP_OR_VOLTAGE_OUT_OF_RANGE = 0xa,
        POWER_SUPPLY_STATUS_UNKNOWN = 0x0
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Battery
    {
        public byte State;
        public int Level;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ButtonState
    {
        // Ensure that each boolean is packed tightly
        [MarshalAs(UnmanagedType.I1)] public bool square;
        [MarshalAs(UnmanagedType.I1)] public bool triangle;
        [MarshalAs(UnmanagedType.I1)] public bool circle;
        [MarshalAs(UnmanagedType.I1)] public bool cross;
        [MarshalAs(UnmanagedType.I1)] public bool DpadUp;
        [MarshalAs(UnmanagedType.I1)] public bool DpadDown;
        [MarshalAs(UnmanagedType.I1)] public bool DpadLeft;
        [MarshalAs(UnmanagedType.I1)] public bool DpadRight;
        [MarshalAs(UnmanagedType.I1)] public bool L1;
        [MarshalAs(UnmanagedType.I1)] public bool L3;
        [MarshalAs(UnmanagedType.I1)] public bool R1;
        [MarshalAs(UnmanagedType.I1)] public bool R3;
        [MarshalAs(UnmanagedType.I1)] public bool R2Btn;
        [MarshalAs(UnmanagedType.I1)] public bool L2Btn;
        [MarshalAs(UnmanagedType.I1)] public bool share;
        [MarshalAs(UnmanagedType.I1)] public bool options;
        [MarshalAs(UnmanagedType.I1)] public bool ps;
        [MarshalAs(UnmanagedType.I1)] public bool touchBtn;
        [MarshalAs(UnmanagedType.I1)] public bool micBtn;

        public byte L2;
        public byte R2;
        public int RX, RY, LX, LY, TouchPacketNum;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string PathIdentifier;

        public Touchpad trackPadTouch0;
        public Touchpad trackPadTouch1;
        public Gyro gyro;
        public Accelerometer accelerometer;
        public Battery battery;

        public void SetDPadState(int dpadState)
        {
            DpadUp = DpadDown = DpadLeft = DpadRight = false;

            switch (dpadState)
            {
                case 0: DpadUp = true; break;
                case 1: DpadUp = DpadRight = true; break;
                case 2: DpadRight = true; break;
                case 3: DpadRight = DpadDown = true; break;
                case 4: DpadDown = true; break;
                case 5: DpadDown = DpadLeft = true; break;
                case 6: DpadLeft = true; break;
                case 7: DpadLeft = DpadUp = true; break;
            }
        }
    }

    // Constructor/Destructor
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Dualsense_Create(string path = "");

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_Destroy(IntPtr handle);

    // General Properties
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_IsConnected(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Dualsense_GetPath(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Dualsense_GetInstanceID(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Dualsense_EnumerateControllerIDs();
    public static List<string> EnumerateControllerIDs()
    {
        List<string> list = new List<string>();
        IntPtr handle = Dualsense_EnumerateControllerIDs();
        string result = Marshal.PtrToStringAnsi(handle);

        if (!string.IsNullOrEmpty(result))
        {
            foreach (string id in result.Split(','))
            {
                string trimmedId = id.Trim();
                if (!string.IsNullOrEmpty(trimmedId))
                {
                    list.Add(trimmedId);
                }
            }
        }

        return list;
    }

    // Haptics and Audio
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_StopAllHaptics(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_PlayHaptics(IntPtr handle, string soundName, bool dontPlayIfAlreadyPlaying, bool loopUntilStopped);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_SetSoundVolume(IntPtr handle, string soundName, float volume);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_SetSoundPitch(IntPtr handle, string soundName, float pitch);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_StopSoundsThatStartWith(IntPtr handle, string prefix);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_LoadSound(IntPtr handle, string SoundName, string Path);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_SetSpeakerVolume(IntPtr handle, int Volume);

    // Configuration and Settings
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_SetLightbarColor(IntPtr handle, byte r, byte g, byte b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_SetPlayerLED(IntPtr handle, byte player);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_SetMicrophoneLED(IntPtr handle, bool led, bool pulse);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_SetMicrophoneVolume(IntPtr handle, int volume);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_UseRumbleNotHaptics(IntPtr handle, bool flag);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_SetRumble(IntPtr handle, byte LeftMotor, byte RightMotor);

    // Trigger Settings
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_SetRightTrigger(IntPtr handle, byte mode, byte[] forces);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_SetLeftTrigger(IntPtr handle, byte mode, byte[] forces);

    // Audio Control
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_InitializeAudioEngine(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Dualsense_GetAudioDeviceName(IntPtr handle);
    public static string GetAudioDeviceName(IntPtr handle)
    {
        return Marshal.PtrToStringAnsi(handle);
    }

    // HID
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_Connect(IntPtr handle, bool samePort = true);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool Dualsense_Write(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_Read(IntPtr handle);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Dualsense_GetButtonState(IntPtr handle, ref ButtonState state);
}
