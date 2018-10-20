using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFire : MonoBehaviour
{
	public float gain;
	public float volume;

	public AudioSource audioSource;

	public GameObject audioController;

	System.Random rndFrequency = new System.Random();

	public float fadeInTime;
	public float fadeInCount;

	public float fadeOutTime;
	public float fadeOutCount;

	AudioController audioC;

	AudioLowPassFilter lowPassFilter;

	//Sin wave
	public double frequency = 1000.0;
	public double maxFrequency = 1000.0;
	public double minFrequency = 100.0;

	private double increment;
	private double phase;
	private double sampling_frequency = 48000.0;

	void Start()
	{
		volume = 0.5f;

		audioController = GameObject.FindGameObjectWithTag("AudioController");
		audioC = audioController.GetComponent<AudioController>();

		lowPassFilter = this.GetComponent<AudioLowPassFilter>();

		audioSource.pitch = Random.Range(0.99f, 1.01f);
		//lowPassFilter.cutoffFrequency = Random.Range(400, 600);

		fadeInTime = 1f;
		fadeInCount = 1f / fadeInTime;

		fadeOutTime = 1f;
		fadeOutCount = 1f / fadeInTime;

	}

	void FixedUpdate()
	{
		gain = volume * audioC.mainVolume * (1 - (fadeInCount / fadeInTime)) * (fadeOutCount / fadeOutTime);

		fadeInCount -= 20f * Time.fixedDeltaTime;
		if (fadeInCount <= 0f)
		{
			fadeInCount = 0f;

			fadeOutCount -= 2f * Time.fixedDeltaTime;
			if (fadeOutCount <= 0f)
			{
				fadeOutCount = 0f;
			}
		}

		frequency = maxFrequency * fadeOutCount;

		if (frequency > maxFrequency)
		{
			frequency = maxFrequency;
		}
		else if (frequency <= minFrequency)
		{
			frequency = minFrequency;
		}
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		SinWaves(data, channels);
	}	

	void SinWaves(float[] data, int channels)
	{
		increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

		for (int i = 0; i < data.Length; i += channels)
		{
			phase += increment;

			if (gain * Mathf.Sin((float)phase) >= 0 * gain)                         //Square wave
			{
				data[i] = (float)gain * 0.6f;
			}
			else
			{
				data[i] = (-(float)gain) * 0.6f;
			}                                                                       //Square wave END

			if (channels == 2)
			{
				data[i + 1] = data[i];
			}

			if (phase > (Mathf.PI * 2))
			{
				phase = 0.0;
			}
		}
	}

}
