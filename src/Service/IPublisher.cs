namespace PubDroid.Service;

public interface IPublisher
{
    public Task UploadBundle(string bundlePath, string packageName, string jsonKeyPath, string track, string status);
}