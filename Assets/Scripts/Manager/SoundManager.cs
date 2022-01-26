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
		Init();
    }

    public void Init()
	{
		GameObject root = GameObject.Find("@Sound");
		if (root == null)
		{
			root = new GameObject { name = "@Sound" };
			Object.DontDestroyOnLoad(root);

			string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // "Bgm", "Effect"
            Debug.Log(soundNames.Length);
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

	public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
	{
		if (audioClip == null)
			return;

		if (type == Define.Sound.Bgm) // BGM 배경음악 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else // Effect 효과음 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
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

	public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
	{
		AudioClip audioClip = GetOrAddAudioClip(path, type);
		Play(audioClip, type, pitch);
	}
}

