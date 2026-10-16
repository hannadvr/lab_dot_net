namespace ElectronicsApp.Models
{
    public interface IConnectable
    {
        bool IsConnected { get; }
        string ConnectToWifi(string networkName);
    }
}