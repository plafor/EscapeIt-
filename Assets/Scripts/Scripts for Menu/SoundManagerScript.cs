using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public const int SUCCESS = 0;
	public const int FAILURE = 1;
	public const int OPENING = 2;
	public const int ENTERING = 3;
	public const int STARTING = 4;
	public const int ZIP = 5;
	public const int UNHIDDE = 6;
    public const int REINFORCERCHEST = 7;
    public const int REINFORCERNEWSPRITE = 8;
    public const int REINFORCERCOMPLETE = 9;

    public static AudioClip successSound, failureSound, openingDoorSound, enteringDoorSound, startingSound, zipSound, unhindSound, reinforcerchestSound, reinforcernewspriteSound, reinforcercompleteSound;
	static AudioSource audioSrc;

	void Start() {
		successSound = Resources.Load<AudioClip> ("Sounds/mario/smb_coin"); //DM-CGS-45");
		failureSound = Resources.Load<AudioClip> ("Sounds/DM-CGS-47");
		openingDoorSound = Resources.Load<AudioClip> ("Sounds/mario/smb_fireball"); //DM-CGS-22");
		enteringDoorSound = Resources.Load<AudioClip> ("Sounds/mario/smb_pipe");   //DM-CGS-23");
		startingSound = Resources.Load<AudioClip> ("Sounds/mario/smb_kick"); //DM-CGS-26");
		zipSound = Resources.Load<AudioClip> ("Sounds/zip");
		unhindSound = Resources.Load<AudioClip> ("Sounds/mario/mb_jump");
        reinforcerchestSound = Resources.Load<AudioClip>("Sounds/mario/smb_powerup");
        reinforcernewspriteSound = Resources.Load<AudioClip>("Sounds/mario/smb_1-up");
        reinforcercompleteSound = Resources.Load<AudioClip>("Sounds/mario/smb_stage_clear");
        audioSrc = GetComponent<AudioSource> ();
	}

	public static void PlaySound(int clip) {
		switch (clip) {
		case SUCCESS:
			audioSrc.PlayOneShot (successSound);
			break;
		case FAILURE:
			//audioSrc.PlayOneShot (failureSound);
			break;
		case OPENING:
			audioSrc.PlayOneShot (openingDoorSound);
			break;
		case ENTERING:
			audioSrc.PlayOneShot (enteringDoorSound);
			break;
		case STARTING:
			audioSrc.PlayOneShot (startingSound);
			break;
		case ZIP:
			audioSrc.PlayOneShot (zipSound);
			break;
		case UNHIDDE:
			audioSrc.PlayOneShot (unhindSound);
			break;
        case REINFORCERCHEST:
            audioSrc.PlayOneShot(reinforcerchestSound);
            break;
        case REINFORCERNEWSPRITE:
            audioSrc.PlayOneShot(reinforcernewspriteSound);
            break;
        case REINFORCERCOMPLETE:
            audioSrc.PlayOneShot(reinforcercompleteSound);
            break;
        }
	}
}
