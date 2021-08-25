Microsoft Band SDK Preview README

1. LICENSE

Use of the Microsoft Band SDK is granted under the terms of the license
agreement found at http://go.microsoft.com/fwlink/?LinkID=525149.


2. CONTENTS

The Microsoft Band SDK Preview for Windows package contains the following:

Microsoft.Band.{version}.nupkg – The NuGet package for the Microsoft Band SDK Preview.

Samples – sample Windows Phone applications for the Microsoft Band SDK.

readme.txt – This file.


3. PREREQUISITES

The Microsoft Band SDK Preview requires having the Band updated with the latest
firmware (done through the Health application).


4. INSTALLATION INSTRUCTIONS

NOTE: The minimum supported Windows Phone version is 8.1.

There are two ways to reference the Microsoft Band SDK Nuget Package:
A. The latest release published on NuGet.org (recommended)
B. The version included in this archive


A. TO REFERENCE THE LATEST MICROSOFT BAND SDK PUBLISHED ON NUGET.ORG:

- In Solution Explorer, right click your project and select Manage Nuget
  Packages
- In the left pane go to Online -> nuget.org
- In the dropdown menu at the top of the screen, select Include Prerelease
- Search for Microsoft Band SDK, and in the result window click Install


B. TO REFERENCE THE MICROSOFT BAND SDK INCLUDED WITH THIS ARCHIVE:

First, add the Microsoft Band SDK NuGet package location as a NuGet package
source. To do that in Visual Studio:
- Go to Tools -> Options -> NuGet Package Manager -> Package Sources
- Add a new package source, and select the folder containing the Microsoft Band
  nupkg file

Next, add a reference to the Microsoft Band SDK NuGet package to your
application. To do that in Visual Studio:
- Right click your project in the Solution Explorer and select Manage Nuget
  Packages
- In the left pane go to Online -> [Package Source from earlier]
- In the dropdown menu at the top of the screen, select Include Prerelease
- Select Microsoft Band SDK and click Install


The NuGet package (from either source) should automatically add the following
capabilities to the <Capabilities> section of your Package.appxmanifest :
  <DeviceCapability Name="bluetooth.rfcomm" xmlns="http://schemas.microsoft.com/appx/2013/manifest">
    <Device Id="any">
      <!-- Microsoft Band RFCOMM Primary Socket -->
      <Function Type="serviceId:A502CA9A-2BA5-413C-A4E0-13804E47B38F" />
      <!-- Microsoft Band RFCOMM Push Notification Socket -->
      <Function Type="serviceId:C742E1A2-6320-5ABC-9643-D206C677E580" />
    </Device>
  </DeviceCapability>


5. SUPPORT AND FEEDBACK

For questions and support use stackoverflow.com with the tag 'msband'.

For feedback, such as feature requests and bug reports, please send e-mail
to msbandsdk@microsoft.com.
