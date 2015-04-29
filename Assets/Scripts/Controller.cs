using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Controller : MonoBehaviour {

	private BannerView bannerView;
	private int playedCount_ad;

	public GameObject player, killer;
	public GameObject Menu, OverTxt, score_board, show_new, medal_25, medal_50, medal_100;
	public GameObject clouds;
	public float minX, maxX;

	int gameCount=1;

	public float startWait, hazardCount, waveWait, spawnWait, speed;
	private float valueX;
	int score;

	bool gameOver = false, start = false;

	GameObject menu_clone ,player_clone;
	GameObject cloud_clone;

	public Texture2D button_play, button_rate, remove_ads, back_button, button_board, button_achieve, share_button;
	bool showMenu = true;

	int best, old_best;

	public AudioClip click;
	
	void OnGUI () {
				GUI.backgroundColor = Color.clear;
				if (showMenu) {
						if (GUI.Button (new Rect (Screen.width / 4, (Screen.height - Screen.height / 2.5f), Screen.width / 2, Screen.width / 4), button_play)) {
								start = true;
								showMenu = false;
				audio.PlayOneShot (click, 2.0f);
						}
						if (GUI.Button (new Rect (Screen.width / 5, (Screen.height - Screen.height / 4f), Screen.width / 3, Screen.width / 4), button_achieve)) {
				Social.ShowAchievementsUI();
				audio.PlayOneShot (click, 2.0f);
						}

			if (GUI.Button (new Rect (Screen.width / 2, (Screen.height - Screen.height / 4f), Screen.width / 3, Screen.width / 4), button_board)) {
				Social.ShowLeaderboardUI(); 
				audio.PlayOneShot (click, 2.0f);
			}

			if (GUI.Button (new Rect (Screen.width / 4, Screen.height - 75, Screen.width / 2, (button_rate.height/2)), button_rate)) {
				Application.OpenURL ("https://play.google.com/store/apps/details?id=com.techulus.trapdown"); 
				 audio.PlayOneShot (click, 2.0f);
						}
				}

				if (gameOver) {

			/*if (GUI.Button (new Rect (Screen.width / 2, (Screen.height - Screen.height / 4f), Screen.height / 5, Screen.height / 5), share_button)) {
				ShareScore(); 
				audio.PlayOneShot (click, 2.0f);
			}*/
						if (GUI.Button (new Rect (Screen.width / 5, (Screen.height - Screen.height / 4f), Screen.height / 5, Screen.height / 5), back_button)) {
								Application.LoadLevel (Application.loadedLevel); 
								audio.PlayOneShot (click, 2.0f);
						}
				}
		}

	// Use this for initialization
	void Start () {
		score = 0;
		best = PlayerPrefs.GetInt ("Best",10);
		old_best = PlayerPrefs.GetInt ("Best",10);
		speed = 1.0f;

		SendToLeaderBoard (best);

		menu_clone = (GameObject)Instantiate (Menu, new Vector3 (0.0f, 0.0f, -5.0f), Quaternion.identity);

		bannerView = new BannerView(
			"ca-app-pub-1104404799908393/1364538025", AdSize.Banner, AdPosition.Top);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);

		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) => {
			if(success) {
				Debug.Log("Signed IN");
			} else {
				Debug.Log("Signin fail!");
			}
		});

	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{	if(!gameOver) {
				valueX = Random.Range (minX, maxX);
				Instantiate (killer, new Vector3 (valueX, -6.0f, 0.0f), Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
					float x = Random.Range (-4.0f, 4.0f);
					float y = Random.Range (-5.0f, 5.0f);
					float z = 0.25f;
					cloud_clone = (GameObject)Instantiate (clouds, new Vector3 (x, y, z), Quaternion.identity);
					Destroy (cloud_clone.gameObject, 4.0f);
				}
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	public void AddScore (int newScore)
	{
		score += newScore;
		//Debug.Log (score);
		if (best < score) {
			best=score;
			try {
			PlayerPrefs.SetInt ("Best", score);
			PlayerPrefs.Save();
				Debug.Log("Saved best as : " + score);
			}
			catch (UnityException e) {
				Debug.LogError (e);
			}

		}
	}

	// Update is called once per frame
	void Update () {

		if (score > best) {
				SendToLeaderBoard (score);
			}

		if (score >= 5) {
			SaveAchieve ("CgkIo9C3xdQcEAIQAg");
		}
		
		if (score >= 10) {
			SaveAchieve ("CgkIo9C3xdQcEAIQAg");
			SaveAchieve ("CgkIo9C3xdQcEAIQAw");
		}
		
		if (score >= 15) {
			SaveAchieve ("CgkIo9C3xdQcEAIQAg");
			SaveAchieve ("CgkIo9C3xdQcEAIQAw");
			SaveAchieve ("CgkIo9C3xdQcEAIQBQ");
		}
		
		if (score >= 25) {
			SaveAchieve ("CgkIo9C3xdQcEAIQAg");
			SaveAchieve ("CgkIo9C3xdQcEAIQAw");
			SaveAchieve ("CgkIo9C3xdQcEAIQBQ");
			SaveAchieve ("CgkIo9C3xdQcEAIQBQ");
		}
		
		if (score >= 50) {
			SaveAchieve ("CgkIo9C3xdQcEAIQAg");
			SaveAchieve ("CgkIo9C3xdQcEAIQAw");
			SaveAchieve ("CgkIo9C3xdQcEAIQBQ");
			SaveAchieve ("CgkIo9C3xdQcEAIQBQ");
			SaveAchieve ("CCgkIo9C3xdQcEAIQBg");
		}
		
		if (score >= 100) {
			SaveAchieve ("CgkIo9C3xdQcEAIQAg");
			SaveAchieve ("CgkIo9C3xdQcEAIQAw");
			SaveAchieve ("CgkIo9C3xdQcEAIQBQ");
			SaveAchieve ("CgkIo9C3xdQcEAIQBQ");
			SaveAchieve ("CCgkIo9C3xdQcEAIQBg");
			SaveAchieve ("CgkIo9C3xdQcEAIQBw");
		}

		if (gameOver) {
			if (Input.GetKey(KeyCode.Escape)) 
			{ Application.LoadLevel (Application.loadedLevel);  }
		}

		/*if (Input.touchCount > 0) {
			start = true;
			Debug.Log("Touch");

			if(gameOver == true) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}*/
		if (score > 10) {
			speed = 1.25f;
			spawnWait = 3.0f; 
		}
		if (score > 20) {
			speed = 1.5f;
			spawnWait = 2.5f;
		}
		if (score > 40) {
			speed = 1.75f;	
			spawnWait = 2.25f;
		}
		if (score > 60) {
			speed = 2.0f;
			spawnWait = 2.0f;
		} 
		if (score > 80) {
			speed = 2.25f;
			spawnWait = 1.75f;
		}
		if (score > 100) {
			speed = 2.5f;
			spawnWait = 1.5f;
		}

		if (start == true) {
			startGame ();
			if (previousScore != score && !gameOver) { //save perf from non needed calculations 
								if (score < 10) {
										//just draw units
										Units.sprite = numberSprites [score];
								} else if (score >= 10 && score < 100) {
										(Tens.gameObject as GameObject).SetActive (true);
										Tens.sprite = numberSprites [score / 10];
										Units.sprite = numberSprites [score % 10];
								} else if (score >= 100) {
										(Hundreds.gameObject as GameObject).SetActive (true);
										Hundreds.sprite = numberSprites [score / 100];
										int rest = score % 100;
										Tens.sprite = numberSprites [rest / 10];
										Units.sprite = numberSprites [rest % 10];
								}
						}
				}
	}

	public void endGame() {
		gameOver = true;
		showBest ();
		Destroy (Units.gameObject);
		Destroy (Tens.gameObject);
		Destroy (Hundreds.gameObject);
		showHighScore ();

		Social.ReportScore(score, "CgkIo9C3xdQcEAIQAQ", (bool success) => {
			if(success) {
				Debug.Log("Score submitted to leaderboard");
			} else {
				Debug.Log("Score fail!");
			}
		});

		Instantiate (OverTxt, new Vector3(0.0f,1.0f,-1.0f), Quaternion.identity);
		Instantiate (score_board, new Vector3(0.0f,0.0f,-1.0f), Quaternion.identity);

		if (score >= 10) {
			Instantiate (medal_25, new Vector3 (0.0f, 0.0f, -5.0f), Quaternion.identity);		
		} else if (score >= 50) {
			Instantiate (medal_50, new Vector3 (0.0f, 0.0f, -5.0f), Quaternion.identity);		
		} else if (score >= 100) {
			Instantiate (medal_100, new Vector3 (0.0f, 0.0f, -5.0f), Quaternion.identity);		
		}

		if (best > old_best) {
			Instantiate (show_new, new Vector3 (1.05f, -0.18f, -1.5f), Quaternion.identity);	
		}
		Destroy (player_clone.gameObject, 0.5f);

		playedCount_ad = PlayerPrefs.GetInt ("GameCount",0);
		playedCount_ad += 1;
		PlayerPrefs.SetInt ("GameCount", playedCount_ad);
		PlayerPrefs.Save ();
		if (playedCount_ad == 3) {
			//counting games ;)
			PlayerPrefs.SetInt ("GameCount", 0);
			PlayerPrefs.Save ();
			}
	}
	
	public bool gameStatus () {
		return gameOver;
	}

	void startGame () {
		if(gameCount == 1) {
			Destroy (menu_clone.gameObject);
			player_clone = (GameObject)Instantiate (player, new Vector3 (0.0f, 3.0f, 0.0f), Quaternion.identity);
			StartCoroutine (SpawnWaves ());
		}
		gameCount = 0;
	}

	int previousScore = -1;
	public Sprite[] numberSprites;
	public SpriteRenderer Units, Tens, Hundreds;

	public Sprite[] numberSprites_small;
	public SpriteRenderer b_Units_small, b_Tens_small, b_Hundreds_small;
	public SpriteRenderer h_Units_small, h_Tens_small, h_Hundreds_small;

	public void SaveAchieve (string id) {
		Social.ReportProgress (id, 100.0f, (bool success) => {
			if (success) {
				Debug.Log ("Score submitted to leaderboard");
			} else {
				Debug.Log ("Score fail!");
			}
		});
	}

	void showBest() {
				if (best < 10) {
						b_Units_small.sprite = numberSprites_small [best];
				} else if (best >= 10 && best < 100) {
						(Tens.gameObject as GameObject).SetActive (true);
						b_Tens_small.sprite = numberSprites_small[best / 10];
			b_Units_small.sprite = numberSprites_small [best % 10];
				} else if (best >= 100) {
						(Hundreds.gameObject as GameObject).SetActive (true);
			b_Hundreds_small.sprite = numberSprites_small [best / 100];
						int rest = best % 100;
			b_Tens_small.sprite = numberSprites_small [rest / 10];
			b_Units_small.sprite = numberSprites_small [rest % 10];
				}

		}

	void showHighScore() {
		if (score < 10) {
			//just draw units
			h_Units_small.sprite = numberSprites_small [score];
		} else if (score >= 10 && score < 100) {
			(Tens.gameObject as GameObject).SetActive (true);
			h_Tens_small.sprite = numberSprites_small [score / 10];
			h_Units_small.sprite = numberSprites_small [score % 10];
		} else if (score >= 100) {
			(Hundreds.gameObject as GameObject).SetActive (true);
			h_Hundreds_small.sprite = numberSprites_small [score / 100];
			int rest = score % 100;
			h_Tens_small.sprite = numberSprites_small [rest / 10];
			h_Units_small.sprite = numberSprites_small [rest % 10];
		}
	
	}

	public float getSpeed() {
		return speed;
	}

	void SendToLeaderBoard (int value) {
		Social.ReportScore(value, "CgkIo9C3xdQcEAIQAQ", (bool success) => {
			if(success) {
				Debug.Log("Score submitted to leaderboard");
			} else {
				Debug.Log("Score fail!");
			}
		});
	}

	/*
	private bool isProcessing = false;

	void ShareScore() 
	{

				if(!isProcessing)
					StartCoroutine( ShareScreenshot() );

	}
	

	public IEnumerator ShareScreenshot()
	{
		isProcessing = true;
		
		// wait for graphics to render
		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// create the texture
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		
		// put buffer into texture
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		
		// apply
		screenTexture.Apply();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		
		byte[] dataToSave = screenTexture.EncodeToPNG();
		
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		
		File.WriteAllBytes(destination, dataToSave);
		
		if(!Application.isEditor)
		{
			// block to open the file and share it ------------START
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "https://play.google.com/store/apps/details?id=com.techulus.trapdown");
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			
			// option one:
			currentActivity.Call("startActivity", intentObject);
			
			// option two:
			//AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "YO BRO! WANNA SHARE?");
			//currentActivity.Call("startActivity", jChooser);
			
			// block to open the file and share it ------------END
			
		}
		isProcessing = false;
		guiTexture.enabled = true;
	}
	*/
}
