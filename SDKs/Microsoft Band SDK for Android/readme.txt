Microsoft Band SDK README

1. LICENSE

Use of the Microsoft Band SDK is granted under the terms of the license
agreement found at http://go.microsoft.com/fwlink/?LinkID=525148.


2. CONTENTS

The Microsoft Band SDK for Android package contains the following:

Band sample apps - example applications demonstrating some of the features of
				   the Microsoft Band. 

microsoft-band-@version@.jar – the Java jar library for the Microsoft Band SDK. 
                               The file name contains the version of the SDK release.

microsoft-band-@version@-docs.zip - file containing the java documentation for microsoft-band-{version}.jar

readme.txt – this file.


3. PREREQUISITES

The Microsoft Band SDK requires the latest version of the Microsoft Health 
application installed, as well as having the Band updated with the latest 
firmware (done through the Microsoft Health application).


4. INSTALLATION INSTRUCTIONS

NOTE: The minimum supported Android API version is 17.  

To use the sample apps:
Unzip the file "Microsoft Band SDK and Samples for Android.zip" into a single directory
(e.g. "C:\Microsoft Band SDK").  

In Eclipse: Select "import" from the "file" menu.  Select Android->"Existing Android Code
Into Workspace" In the ensuing pop up window, enter the name of the directory where the 
SDK package was unzipped (e.g. "C:\Microsoft Band SDK").  Each app should import just as 
it is, directly into an Eclipse project using ADT. 

In Android Studio: The sample apps will be refactored into the Android Gradle structure
upon importation.  Open Android Studio, select "File"->"New"->"Import Project...", 
choose an application's directory (e.g. "C:\Microsoft Band SDK\BandPersonalizationApp"),
and press "OK."  Select the destination directory (or just use the default selection to
refactor in place) and press "Next".  Press "Finish" and the project will load.

To use the microsoft-band-@version@.jar in your own project:
First, include the Microsoft Band SDK jar file in your Android project.  To do
that in Android Studio:
- Go to your Android project's application directory and locate the libs 
  folders (you can create it if it does not already exist), and copy the jar
  file to this location.
- Open the Project Structure window from the File menu.
- Select the application module from the Modules list on the left.
- Select the Dependencies tab on the right.
- Click the + button on the right and select File dependency.
- Expand the libs folder, select the jar file, and click OK.

In addition to adding the jar file to your Android project, you will also need
to declare the following uses-permission tags in the AndroidManifest.xml:
   <uses-permission android:name="android.permission.BLUETOOTH"/>
   <uses-permission android:name="com.microsoft.band.service.access.BIND_BAND_SERVICE"/>


5. SUPPORT AND FEEDBACK

For questions and support use stackoverflow.com with the tag 'microsoft-band'
(http://go.microsoft.com/fwlink/?LinkID=619303).

For feedback, such as feature requests, please send e-mail to msbandsdk@microsoft.com
or use Microsoft Band UserVoice (http://go.microsoft.com/fwlink/?LinkID=619304).
