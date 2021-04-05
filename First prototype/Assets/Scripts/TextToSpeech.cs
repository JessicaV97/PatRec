using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Happify.Tts
{
    public class TextToSpeech : MonoBehaviour
    {
        public AudioSource _audio;

        // Start is called before the first frame update
        void Start()
        {
            _audio = gameObject.GetComponent<AudioSource>();
            //StartCoroutine(DownloadTheAudio());
        }

        public void ReadSpeech(string message)
        {
            StartCoroutine(DownloadTheAudio(message));
        }

        IEnumerator DownloadTheAudio(string message)
        {
            using (UnityWebRequest website = UnityWebRequestMultimedia.GetAudioClip("https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + message + "&tl=NL", AudioType.MPEG))
            {
                yield return website.SendWebRequest();

                if (website.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(website.error);
                }
                else
                {
                    AudioClip myClip = DownloadHandlerAudioClip.GetContent(website);
                    _audio.clip = myClip;
                    _audio.Play();
                }
            }

        }
    }
}
