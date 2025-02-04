## PubDroid

#### A lightweight, .NET [command-line] tool that simplifies uploading Android App Bundles to the Google Play Console for any type of app. Perfect for CI/CD pipelines or local workflows, it focuses on straightforward, reliable uploads to get your application deployed quickly. Inspired by a desire to avoid having to use Fastlane (which my team wasn’t leveraging for any other parts of our .NET MAUI pipeline).

Below is a quick guide to using the **PubDroid** tool. Currently, it offers a single command, **upload-bundle**, which handles uploading your Android App Bundle to the Google Play Console. I am planning to add more features and commands, but the existing command is fully usable as is.

---

## Usage

Run the tool from your command line by typing:

```
pubdroid [command]
```

For help or version info, use:
```
pubdroid --help
pubdroid --version
```

### Upload Bundle

The `upload-bundle` command requires a few parameters and supports some optional ones:

```
pubdroid upload-bundle [--bundle <String>] [--packageName <String>] [--jsonKey <String>] 
                       [--track <String>] [--status <String>] [--help]
```

- **-b, --bundle \<String>**  
  *Relative or absolute path of the app bundle (AAB).* **(Required)**
  
- **-p, --packageName \<String>**  
  *Package name from Google Play Console.* **(Required)**
  
- **-j, --jsonKey \<String>**  
  *Relative or absolute path to your service account JSON key.* **(Required)**
  
- **-t, --track \<String>**  
  *The Play Console testing track (defaults to `internal`).*
  
- **-s, --status \<String>**  
  *Release status (defaults to `draft`).*

- **-h, --help**  
  *Displays help for the command.*

#### Example

```
pubdroid upload-bundle \
    --bundle MyApp_v1.0.0.aab \
    --packageName com.example.myapp \
    --jsonKey /path/to/google-play-key.json \
    --track internal \
    --status draft
```

This will upload the specified bundle to your app’s **internal** test track on the Play Console, setting its status to **draft**. Adjust the track and status as needed to fit your release workflow.
