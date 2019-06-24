# Unity Android Native File Opener
Open files from a Unity application natively on an Android device. 
Android recently added more security measures to restrict access to files from outside of an application. It used to be possible to open files with Unity's Application.OpenUrl() method on Android but now it requires a little more work to achive this. This plugin has been tested on Android Version 8, Unity Version 2018.2.17f1, and the "Internal" build system.

How to set it up:
- Download the Unity package here: [UnityAndroidNativeFileOpener UnityPackage](https://github.com/Andy-Roger/UnityAndroidNativeFileOpener/raw/master/AndroidContentOpener.unitypackage)
- OR
- Clone this repo into your Assets folder

How to use the example:
- Add this tag to your AndroidManifest.xml file in Assets/Plugins/Android inside of the ```<application>nest here</application>:``` tag.
![alt text](https://github.com/Andy-Roger/UnityAndroidNativeFileOpener/blob/master/Images/AssetManifestProviderImage.png)
- The ```android:authorities``` attribute should be a unique name, make this your app bundleID to assure uniqueness.
- Open example scene: "AndroidNativeFileOpenerExample"
- Add the example scene to your build settings and build to your device.
- When done, open app, tap the scene and a frog GIF should be downloaded locally and appear in your default Android GIF viewer.
- Feel free to try the other examples in the example script which can be uncommented and used.

How to use the API:
- Prepare the build using the steps above.
- Create a reference to a file on your device.
- Insert ```AndroidContentOpenerWrapper.OpenContent(pathToYourLocalFile);``` in your code.

Credits: Much of this plugin was adapted from yasirkula (thanks for the help!) [UnityNativeShare](https://github.com/yasirkula/UnityNativeShare)
