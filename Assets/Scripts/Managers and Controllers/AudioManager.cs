using System.Collections;
using UnityEngine;

[System.Serializable]
public class Sound {

	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 0.7f;
	[Range(0.5f, 1.5f)]
	public float pitch = 1f;

	[Range(0f, 0.5f)]
	public float randomVolume = 0.1f;
	[Range(0f, 0.5f)]
	public float randomPitch = 0.1f;

	public bool loop = false;

	private AudioSource source;

	public Sound () {
		volume = 0.7f;
		pitch = 1f;
		randomVolume = 0.1f;
		randomPitch = 0.1f;
		loop = false;
	}

	public void SetSource (AudioSource _source) {
		source = _source;
		source.clip = clip;
		source.loop = loop;
	}

	public void Play (float pitchModifier, float _volume) {
		float newPitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f)) * pitchModifier;
		source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f)) * _volume;
		source.pitch = newPitch;
		source.Play();
	}

	public void Stop () {
		source.Stop();
	}

}

public class AudioManager : MonoBehaviour {

	private GameObject holder;
	public static AudioManager instance;

	[SerializeField]
	Sound[] sounds;

	[Range(0,1)]
	[SerializeField] float volume = 1;
	public float Volume {
		set { volume = value; }
		get { return volume; }
	}
	[SerializeField] bool enabledSound = true;
	public bool EnabledSound {
		set { enabledSound = value; }
		get { return enabledSound; }
	}

	bool disabledSoundFromStart;

	void Awake ()
	{
		if (instance != null)
		{
			Debug.LogError("More than one AudioManager in the scene.");
			Destroy(gameObject);
			return;
		} else
		{
			instance = this;
		}
	}

	private void Update () {
		disabledSoundFromStart = true;
	}

	void Start ()
	{
		disabledSoundFromStart = false;
		holder = new GameObject("_Sounds");
		holder.transform.parent = transform;
		for (int i = 0; i < sounds.Length; i++)
		{
			GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
			_go.transform.SetParent(holder.transform);
			sounds[i].SetSource (_go.AddComponent<AudioSource>());
		}

	}
	public void PlaySound(string _name) {
		PlaySound(_name, 1);
	}
	public void PlaySound (string _name, float pitchModifier)
	{
		if (!(disabledSoundFromStart && enabledSound)) {
			return;
		}
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].Play(pitchModifier, volume);
				return;
			}
		}

		// no sound with _name
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
	}

	public void StopSound (string _name) {
		for (int i = 0; i < sounds.Length; i++) {
			if (sounds[i].name == _name) {
				sounds[i].Stop();
				return;
			}
		}

		// no sound with _name
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
	}

	// Testing and debuging

	[HideInInspector] public GameObject testGo;
	[HideInInspector] public string testSoundName;
	[HideInInspector] public float testPitchMod = 1;


	public void TestSound (string _name, float pitchModifier) {
		for (int i = 0; i < sounds.Length; i++) {
			if (sounds[i].name == _name) {
				if(testGo == null) {
					testGo = new GameObject("Sound_for_testing");
					testGo.transform.SetParent(transform);
					testGo.AddComponent<AudioSource>();
					testGo.GetComponent<AudioSource>().playOnAwake = false;
				}
				sounds[i].SetSource(testGo.GetComponent<AudioSource>());
				sounds[i].Play(pitchModifier, volume);
				return;
			}
		}
		Debug.LogWarning(_name + " not found");
	}
}
