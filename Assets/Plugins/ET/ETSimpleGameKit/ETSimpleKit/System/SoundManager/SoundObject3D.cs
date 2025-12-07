using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ETSimpleKit.SoundSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundObject3D : MonoBehaviour
    {
        private SoundGeneral _soundGeneral;
        private AudioSource _audioSource;
        public AudioSource audioSource
        {
            get
            {
                if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
                return _audioSource;
            }
        }
        private float _volumn;
        public float Volumn
        {
            get
            {
                return _volumn;
            }
            set
            {
                _volumn = value;
                audioSource.volume = _volumn;
            }
        }
        private float _pitch;
        public float Pitch
        {
            get
            {
                return _pitch;
            }
            set
            {
                _pitch = value;
                audioSource.pitch = _pitch;
            }
        }
        public void Init(SoundGeneral soundGeneral)
        {
            _soundGeneral = soundGeneral;
        }
        public void Play(Vector3 position, AudioClip audioClip, float volumn, float pitch)
        {
            transform.position = position;
            audioSource.clip = audioClip;
            Pitch = pitch;
            Volumn = volumn;
            audioSource.Play();

            // Start a coroutine to check when the audio has finished playing
            StartCoroutine(DisableObjectAfterAudioClip());
        }

        IEnumerator DisableObjectAfterAudioClip()
        {
            // Wait until the audio clip finishes playing
            yield return new WaitForSeconds(audioSource.clip.length + 0.1f);
            // Disable the GameObject
            gameObject.SetActive(false);
        }
    }

}