using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Define
{
	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount,  // 아무것도 아님. 그냥 Sound enum의 개수 세기 위해 추가. (0, 1, '2' 이렇게 2개) 
	}
}

public class SoundManager : MonoBehaviour
{
    public AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    public static SoundManager soundManager;
    GameObject root;

    float _volume = 0;
    bool isBGMStarting = false;

    public static SoundManager GetInstance()
    {
        if(soundManager == null)
        {
            soundManager = FindObjectOfType<SoundManager>();
        }

        return soundManager;
    }
 
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        if(Player.GetInstance() != null)
            Player.GetInstance().onDead += Dead;
        if (soundManager != null)
        {
            if (soundManager != this)
            {
                Destroy(gameObject);
            }
        }
        Init();
    }

    public void Dead()
    {
        Destroy(root.transform.GetChild(1).gameObject);
        _audioSources[(int)Define.Sound.Bgm].loop = false;
        SoundManager.GetInstance().Play("Sound/LevelSound/DeadScene", 0.7f, Define.Sound.Bgm);
    }

    public void Init()
	{
		root = GameObject.Find("@Sound");
		if (root == null)
		{
			root = new GameObject { name = "@Sound" };
			//Object.DontDestroyOnLoad(root);

			string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // "Bgm", "Effect"
			for (int i = 0; i < soundNames.Length - 1; i++)
			{
				GameObject go = new GameObject { name = soundNames[i] };
				_audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
			}

			_audioSources[(int)Define.Sound.Bgm].loop = true; // bgm 재생기는 무한 반복 재생
		}
        else
        {
            for(int i = 0; i < (int)Define.Sound.MaxCount; i++)
            {
                _audioSources[i] = root.transform.GetChild(i).GetComponent<AudioSource>();
            }
        }
	}

	public void Play(AudioClip audioClip, float volume = 1.0f, Define.Sound type = Define.Sound.Effect, float BGMChangeSpeed = 1, float pitch = 1.0f)
	{
		if (audioClip == null)
			return;

		if (type == Define.Sound.Bgm) // BGM 배경음악 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            audioSource.clip = audioClip;
			audioSource.pitch = pitch;
			_volume = volume;
            StartCoroutine("ChangeBGM", BGMChangeSpeed);
		}
		else // Effect 효과음 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip,volume);
		}
	}

	AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
	{
		if (path.Contains("Sound/") == false)
			path = $"Sound/{path}"; // 📂Sound 폴더 안에 저장될 수 있도록
	
		AudioClip audioClip = null;

		if (type == Define.Sound.Bgm) // BGM 배경음악 클립 붙이기
		{
			audioClip = Resources.Load<AudioClip>(path);
		}
		else // Effect 효과음 클립 붙이기
		{
			if (_audioClips.TryGetValue(path, out audioClip) == false)
			{
				audioClip = Resources.Load<AudioClip>(path);
				_audioClips.Add(path, audioClip);
			}
		}

		if (audioClip == null)
			Debug.Log($"AudioClip Missing ! {path}");

		return audioClip;
	}

    public void Play(string path, float volume = 1.0f, Define.Sound type = Define.Sound.Effect, float BGMChangeSpeed = 1, float pitch = 1.0f)
	{
		AudioClip audioClip = GetOrAddAudioClip(path, type);
		Play(audioClip, volume, type, BGMChangeSpeed, pitch);
	}

    public void Play(AudioSource effectAudio, string path, float volume = 1.0f, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);

        effectAudio.pitch = pitch;
        
        effectAudio.PlayOneShot(audioClip, volume);
    }

    public void stopBGM(float term = 0)
    {
        StartCoroutine("StopBGM", term);
    }

    IEnumerator StartBGM(float BGMChangeSpeed)
    {
        isBGMStarting = true;
        AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
        audioSource.volume = 0;
        audioSource.Play();

        for (int i = 0; i < 100; i++)
        {
            _audioSources[(int)Define.Sound.Bgm].volume  += (float)(_volume / 100.0f);
            yield return new WaitForSeconds(0.1f * BGMChangeSpeed);
        }
        
    }

    IEnumerator StopBGM(float term)
    {
        if (isBGMStarting)
            StopCoroutine("StartBGM");

        float vol = _audioSources[(int)Define.Sound.Bgm].volume;

        for (int i = 0; i < 50; i++)
        {
            _audioSources[(int)Define.Sound.Bgm].volume -= (float)(vol / 50.0f);
            yield return new WaitForSeconds(0.05f * term);

            Debug.Log("Chaneg2");
        }

        Debug.Log("Chaneg1");
    }

    IEnumerator ChangeBGM(float BGMChangeSpeed)
    {
        AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
        if (audioSource.isPlaying == false)
        {
            StartCoroutine("StartBGM", BGMChangeSpeed);
        }
        else
        {
            StartCoroutine("StopBGM", BGMChangeSpeed);

            yield return new WaitForSeconds(2.5f);

            if (audioSource.isPlaying)
                audioSource.Stop();
            StartCoroutine("StartBGM", BGMChangeSpeed);
            Debug.Log("Chaneg");
        }
        yield return new WaitForSeconds(0);
    }
}

