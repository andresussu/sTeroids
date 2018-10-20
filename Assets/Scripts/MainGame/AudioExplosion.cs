using UnityEngine;

public class AudioExplosion : MonoBehaviour
{
	public float gain;
	public float volume = 1f;

	public AudioSource audioSource;

	public GameObject audioController;

	System.Random rndFrequency = new System.Random();

	public float fadeInTime;
	public float fadeInCount;

	public float fadeOutTime;
	public float fadeOutCount;

	AudioController audioC;

	AudioLowPassFilter lowPassFilter;

	void Start ()
	{
		audioController = GameObject.FindGameObjectWithTag("AudioController");
		audioC = audioController.GetComponent<AudioController>();

		lowPassFilter = this.GetComponent<AudioLowPassFilter>();

		audioSource.pitch = Random.Range( 0.8f, 1.2f);
		lowPassFilter.cutoffFrequency = Random.Range(400, 600);

		fadeInTime = 1f;
		fadeInCount = 1f / fadeInTime;

		fadeOutTime = 1f;
		fadeOutCount = 1f / fadeOutTime;
	}
	
	void Update ()
	{
		gain = volume * audioC.mainVolume * (1 -(fadeInCount / fadeInTime)) * (fadeOutCount / fadeOutTime);

		fadeInCount -= 25 * Time.deltaTime;
		if (fadeInCount <= 0f)
		{
			fadeOutCount -= .8f * Time.deltaTime;
			if (fadeOutCount <= 0f)
			{
				fadeOutCount = 0f;
			}
			fadeInCount = 0f;
		}



	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = gain * (float)(rndFrequency.NextDouble() * 2.0 - 1.0);
		}
	}

}
