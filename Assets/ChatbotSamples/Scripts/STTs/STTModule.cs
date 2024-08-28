using com.cyborgAssets.inspectorButtonPro;
using OpenAI;
using Samples.Whisper;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class STTModule : MonoBehaviour
{
    private readonly string fileName = "output.wav";
    private readonly int duration = 5;

    private AudioClip clip;
    private bool isRecording;
    private float time;
    private OpenAIApi openAIApi;

    public GPTData gptData;


    public UnityEvent onStartRecording;
    public UnityEvent<string> onEndRecording;

    private void Start()
    {
        openAIApi = new OpenAIApi(gptData.apiKey);

        onEndRecording.AddListener(OnEndRecording);
    }

    [ProButton]
    private void StartRecording()
    {
        isRecording = true;
        clip = Microphone.Start(null, false, duration, 44100);

        onStartRecording.Invoke();
    }

    [ProButton]
    private async void EndRecording()
    {
        Microphone.End(null);

        byte[] data = SaveWav.Save(fileName, clip);

        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() {Data = data, Name = "audio.wav"},
            // File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
            Language = "ko"
        };
        var res = await openAIApi.CreateAudioTranscription(req);

        onEndRecording.Invoke(res.Text);
       
    }

    private void OnEndRecording(string meassage)
    {
        Debug.Log(meassage);
    }
}
