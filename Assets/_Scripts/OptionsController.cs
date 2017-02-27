using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	//public Slider difficultySlider;
	public LevelManager levelManager;
	
	private MusicPlayer musicPlayer;
	
	void Start ()
	{
		musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		
		//difficultySlider.value = PlayerPrefsManager.GetDifficulty();
		
	}
	
	void Update ()
	{
        //if (musicPlayer)
        //{
        //	musicPlayer.SetVolume(volumeSlider.value);
        //}

        AudioListener.volume = volumeSlider.value;
	}
	
	public void SaveAndExit()
	{
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);

        //if (musicPlayer)
        //{
        //	musicPlayer.SetVolume(volumeSlider.value);
        //}

        AudioListener.volume = volumeSlider.value;

        //PlayerPrefsManager.SetDifficulty(difficultySlider.value);
        levelManager.LoadLevel("01a Start");
	}
	
	public void SetDefaults()
	{
		volumeSlider.value = .25f;
		//difficultySlider.value = 2f;
	}
}
