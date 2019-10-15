# Support_tracker
To launch the project on local PC, please do the following:
1. Launch smtp4dev.exe on port, what is used in webconfig file (example below):
```
    <mailSettings>
      <smtp from="andrewchess25@gmail.com">
        <network host="localhost" port="32" userName="Andrew Lomakin" password="" enableSsl="false" />
      </smtp>
    </mailSettings>
```
2. In nuget command line execute the following command:
```Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r ```
##### Now you may launch web app in debug mode.
