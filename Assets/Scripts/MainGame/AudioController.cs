using UnityEngine;

public class AudioController : MonoBehaviour
{
	public float mainVolume = .7f;

	public double frequency = 440.0;
	private double increment;
	private double phase;
	private double sampling_frequency = 48000.0;

	public float fadeInTime;
	public float fadeInCount;

	public float gain;
	public float volume = 1f;

	public float[] frequencies;
	public int frequenciesIndex;

	[Range(-1f, 1f)]
	public float engineOffset;

	System.Random rand = new System.Random();
	AudioLowPassFilter lowPassFilter;

	private void Start()
	{
		frequencies = new float[8];
		frequencies[0] = 440;
		frequencies[1] = 497;
		frequencies[2] = 554;
		frequencies[3] = 587;
		frequencies[4] = 659;
		frequencies[5] = 740;
		frequencies[6] = 831;
		frequencies[7] = 880;

		fadeInTime = 1f;

		//gain = volume * mainVolume;
		fadeInCount = 1f / fadeInTime;
	}

	private void Awake()
	{
		Update();
	}

	private void Update ()
	{

		gain = volume * mainVolume * (1f - (fadeInCount / fadeInTime));

		fadeInCount -= Time.deltaTime;
		if (fadeInCount <= 0f)
		{
			fadeInCount = 0f;
		}


		/*
		if (Input.GetKeyDown(KeyCode.W))
		{
			gain = volume;
			frequency = frequencies[frequenciesIndex];
			frequenciesIndex += 1;
			frequenciesIndex = frequenciesIndex % frequencies.Length;

			engineOn = true;

		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			gain = 0;

			engineOn = false;
		}
		*/
	}

	private void OnAudioFilterRead(float[] data, int channels)
	{
		//SinWaves(data, channels);

		AudioMixer(data, channels);


	}

	public void PlayAudio(string sound)
	{
		//Debug.Log(sound);
	}

	private void AudioMixer(float[] data, int channels)
	{
		BGNoise(data, channels);
	}

	private void BGNoise(float[] data, int channels)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = gain * (float)(rand.NextDouble() * 2.0 - 1.0);
		}
	}

	private void Engine(float[] data)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = gain * (float)(rand.NextDouble() * 2.0 - 1.0 + engineOffset);
		}
	}

	/*
	void SinWaves(float[] data, int channels)
	{
		increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

		for (int i = 0; i < data.Length; i += channels)
		{
			phase += increment;

			//data[i] = (float)(gain * Mathf.Sin((float)phase));					//Sin wave

			if (gain * Mathf.Sin((float)phase) >= 0 * gain)                         //Square wave
			{
				data[i] = (float)gain * 0.6f;
			}
			else
			{
				data[i] = (-(float)gain) * 0.6f;
			}                                                                       //Square wave END

			//data[i] = (float)(gain * (double)Mathf.PingPong((float)phase, 1.0f));	//Triangle wave

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
	*/

}
