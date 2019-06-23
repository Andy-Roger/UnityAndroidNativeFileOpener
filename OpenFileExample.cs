using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

/// <summary>
/// This class demonstrates how to natively open a file on Android with a file's default application 
/// </summary>
public class OpenFileExample : MonoBehaviour {
    public GameObject quad;
    //private string downloadURL = "https://ichef.bbci.co.uk/news/624/cpsprodpb/3DA1/production/_96777751_c0220207-red-eyed_treefrog-spl.jpg";
    //private string downloadURL = "http://icwdm.org/handbook/reptiles/repf9.pdf";
    private string downloadURL = "https://media1.tenor.com/images/92cbbf4aacb333461ec36fd39cfda856/tenor.gif?itemid=5275848";

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(BeginDownloadingContent());
            quad.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    /// <summary>
    /// Download some content locally onto the Android device before calling "LoadContent"
    /// </summary>
    /// <returns></returns>
    IEnumerator BeginDownloadingContent() {
        string url = downloadURL;
        using (UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) {
                print(www.error);
            } else {
                //file name must have an extension that matches the content type
                //string fileName = "frogJGP.jpg";
                //string fileName = "frogPDF.PDF";
                string fileName = "frogGIF.gif"; //file name must have an extension that matches the content type

                string path = Path.Combine(Application.persistentDataPath, fileName); //creates a path to an uncreated file at the Unity app's persistent data path
                File.WriteAllBytes(path, www.downloadHandler.data);
                //File.WriteAllText(path, www.downloadHandler.text);
                LoadContent(path);
            }
        }
    }

    void LoadContent(string path) {
        AndroidContentOpenerWrapper.OpenContent(path); //path must be path/to/file.filetype
    }
}
