using UnityEngine;
using System.Collections;

public class Playsound : MonoBehaviour 

{
	public string value;
	AudioSource audioSource;
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.2f;
	}
	public void Clicky (){
		audioSource.Play();
		GetComponentInParent<KeyPadInteraction>().enterednumber(value);
	}

	public void SubmitButton()
	{
		audioSource.Play();
		GetComponentInParent<KeyPadInteraction>().SubmitCode();
	}

	public void ClearButton()
	{
		audioSource.Play();
		GetComponentInParent<KeyPadInteraction>().Clear();
	}

	public void Close()
	{
		GetComponentInParent<KeyPadInteraction>().Close();
	}

}
