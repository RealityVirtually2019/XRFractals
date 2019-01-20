using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {
    AudioSource _audioSource;
    float[] _samples = new float[512];
    float[] _freqBand = new float[8];
    float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    private float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    public static float[] _amplitude = new float[8];
    public static float[] _amplitudeBuffer = new float[8];

    public static float _Amplitude, _AmplitudeBuffer;
    float _AmplitudeHighest;
    public float _audioProfile;


    // Use this for initialization1
    void Start ()
    {
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);

	}
	
	// Update is called once per frame
	void Update ()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBand();
        GetAmplitude();
	}

    void AudioProfile(float _audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = _audioProfile;
        }
    }

    void GetAmplitude()
    {
        float _currentAmplitude = 0;
        float _currentAmplitudeBuffer = 0;
        for(int i = 0; i< 8; i++)
        {
            _currentAmplitude += _audioBand[i];
            _currentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if(_currentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _currentAmplitude;
        }
        _Amplitude = _currentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _currentAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBand()
    {
        for (int i = 0; i < 8; i++)
        {
            if(_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest = _freqBand;
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_freqBand[i] / _freqBandHighest[i]);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples,0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for(int g = 0; g < 0; g++)
        {
            if(_freqBand[g]> _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if(_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    {

        int count = 0;
        
        for(int i = 0; i < 8; i++)
        {

            float average = 0;
            int sampleCount = (int)Mathf.Pow(2,i) *2; 
            if( i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;
            _freqBand[i] = average * 10;
        }
    }
}
