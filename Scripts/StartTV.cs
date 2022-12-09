using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTV : MonoBehaviour
{
    Microphone micro;
    WebCamTexture camTexture;
    int snapshotCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (Microphone.devices.Length != 0)
        {
            audioSource.clip = Microphone.Start("", true, 1, 44100);
            audioSource.Play();
        }
        camTexture = new WebCamTexture();
        Debug.Log("Camera Name: " + camTexture.deviceName);
        camTexture.Play();
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.mainTexture = camTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if (camTexture.isPlaying) camTexture.Pause();
            else camTexture.Play();
        }
        if (Input.GetKey("s"))
        {
            camTexture.Stop();
        }
        if (Input.GetKeyDown("x"))
        {
            Texture2D snapshot = new Texture2D(camTexture.width, camTexture.height);
            snapshot.SetPixels(camTexture.GetPixels());
            snapshot.Apply();
            string picturesPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            System.IO.File.WriteAllBytes(picturesPath + "\\" + snapshotCounter++ + ".png", snapshot.EncodeToPNG());
        }
    }
}
