using Cocona;
using Microsoft.Extensions.DependencyInjection;
using PubDroid;
using PubDroid.Service;

var builder = CoconaApp.CreateBuilder(configureOptions: options =>
{
    options.EnableConvertCommandNameToLowerCase = true;
    options.EnableConvertOptionNameToLowerCase = false;
    options.TreatPublicMethodsAsCommands = false;
});

builder.Services.AddSingleton<IPublisher, Publisher>();

var app = builder.Build();

app.AddCommand(nameof(IPublisher.UploadBundle), async (IPublisher publisher, 
    [Option('b', Description = Constants.Description.Bundle)] string bundle,
    [Option('p', Description = Constants.Description.PackageName)] string packageName,
    [Option('j', Description = Constants.Description.JsonKey)] string jsonKey,
    [Option('t', Description = Constants.Description.Track)] string track = Constants.Track.Internal,
    [Option('s', Description = Constants.Description.Status)] string status = Constants.Status.Draft) =>
{
    await publisher.UploadBundle(bundle, packageName, jsonKey, track, status);
});

app.Run();