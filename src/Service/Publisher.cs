using System.Net.Mime;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace PubDroid.Service;

public class Publisher : IPublisher
{
    public async Task UploadBundle(string bundlePath, string packageName, string jsonKeyPath, string track, string status)
    {
        var service = GetPublisherFromJsonKey(jsonKeyPath);

        try
        {
            var requestEdit = new AppEdit();
            var insertRequest = service.Edits.Insert(requestEdit, packageName);
            var appEdit = await insertRequest.ExecuteAsync();

            var updateRequest = service.Edits.Tracks.Update(
                body: new Track
                {
                    TrackValue = track,
                    Releases = [new TrackRelease { Status = status }]
                },
                packageName: packageName,
                editId: appEdit.Id,
                track: track);
            await updateRequest.ExecuteAsync();

            await using var bundleStream = File.OpenRead(bundlePath);
            var request = service.Edits.Bundles.Upload(packageName, appEdit.Id, bundleStream,
                MediaTypeNames.Application.Octet);
            request.ProgressChanged += progress =>
            {
                Console.WriteLine(
                    $"Upload status: {progress.Status}  |  bytes sent: {progress.BytesSent}  |  Exception: {progress.Exception?.Message ?? string.Empty}");
            };
            await request.UploadAsync();

            var commitRequest = service.Edits.Commit(packageName, appEdit.Id);
            var commit = await commitRequest.ExecuteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            service.Dispose();
        }
    }
    
    private static AndroidPublisherService GetPublisherFromJsonKey(string jsonKeyPath)
    {
        var serviceAccCred = ServiceAccountCredential.FromServiceAccountData(File.OpenRead(jsonKeyPath));
        var credential = GoogleCredential
            .FromServiceAccountCredential(serviceAccCred)
            .CreateScoped(AndroidPublisherService.Scope.Androidpublisher);
        
        var initializer = new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            HttpClientTimeout = TimeSpan.FromMinutes(5)
        };
        var service = new AndroidPublisherService(initializer);

        return service;
    }
}