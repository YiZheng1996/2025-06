namespace MainUI.Devices;

/// <summary>
/// 设备类型基类
/// </summary>
public interface IBaseType
{
    DeviceType TypeName { get; }
}

public enum DeviceType
{
    Modbus,
    TCPIP,
}
