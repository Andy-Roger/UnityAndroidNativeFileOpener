using UnityEngine;
using System.IO;

/// <summary>
/// Java wrapper class. this class demonstrates how to natively open files on Android in Unity.
/// Largely adapted from NativeShare https://github.com/yasirkula/UnityNativeShare/blob/master/Plugins/NativeShare/NativeShare.cs
/// </summary>
public class AndroidContentOpenerWrapper {
    private static AndroidJavaClass m_ajc = null;
    private static AndroidJavaClass AJC {
        get {
            if (m_ajc == null) {
                m_ajc = new AndroidJavaClass("com.cartoontexas.andyr.unityplugin.ContentOpener"); //Accesses .JAR Java class ContentOpener
            }
            return m_ajc;
        }
    }

    private static AndroidJavaObject m_context = null;
    private static AndroidJavaObject Context {
        get {
            if (m_context == null) {
                using (AndroidJavaObject unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                    m_context = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
                }
            }
            return m_context;
        }
    }
    /// <summary>
    /// Access to Java class method OpenContent
    /// </summary>
    /// <param name="filePath">Local path to desired content to open</param>
    public static void OpenContent(string filePath) {
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath)) {
            AJC.CallStatic("OpenContent", Context, filePath); //Uses the OpenContent method in the Java OpenContent class
        } else {
            Debug.LogError("File does not exist at path or permission denied: " + filePath);
        }
    }
}
