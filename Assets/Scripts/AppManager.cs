using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour {

//public variables
[Header("VARIABLES")]
[Tooltip("empty tile handy for resetting")]
//empty tile handy for resetting
public Button emptyTile;

//store the scene name for the home page here
public string sceneName;

[Tooltip("Text To Speech variable")]
//TTS gameobject
public GameObject ttsSpeech;

//array of all the text from the tiles in the top row
public List<string> output; 

[Header("_______________________________________________________________________________")]
[Header("topGrid")]
[Tooltip("ref to the top grid space holder")]
//ref to the top grid space holder
public Button[] topGridSpace;

//ref for the tile object which contains the image and the text that will be added
[HideInInspector]
public List <Button> topGridTiles;

[Header("_______________________________________________________________________________")]
[Header("mainGrid")]
[Tooltip("array of grid spaces to be used for the main grid")]
//array of grid spaces to be used for the main grid
public Button[] mainGridSpace;

[Tooltip("array of Tiles that will go into the main grid")]
//array of Tiles that will go into the main grid
public Button[] mainGridTiles;



	// Use this for initialization
	void Start () {
		//font.material.mainTexture.filterMode = FilterMode.Point;
		//Put image into grid
		//gridSpace sprite property setting equal to new image
		for (int i = 0; i < mainGridTiles.Length; i++)
        {
			//each image into each gridspace
            mainGridSpace[i].GetComponentInChildren<Image>().sprite = 
			mainGridTiles[i].GetComponentInChildren<Image>().sprite;
			//each text into each gridspace
			mainGridSpace[i].GetComponentInChildren<Text>().text = 
			mainGridTiles[i].GetComponentInChildren<Text>().text;
        }

		//delay the speech for the scene name 	
		StartCoroutine(WaitAndSay());

		
	}//close the start()



void Update(){

		//Putting the grid squares into the topGridSpace
		//if nothing selected these will be empty
		for (int i = 0; i < topGridTiles.Count; i++)
        {
			//get the top grid space and fill with the tile image
            topGridSpace[i].GetComponentInChildren<Image>().sprite = 
            topGridTiles[i].GetComponentInChildren<Image>().sprite;

            //get the top grid space and fill with the tile text
            topGridSpace[i].GetComponentInChildren<Text>().text = 
            topGridTiles[i].GetComponentInChildren<Text>().text;
        }
		
	}//close the update()


/////FUNCTIONS///////////////////////////////////////////////////

	//when you click the button tile, push tile into the array
	public void AddButton(Button tile){
		//add tile to the array	
       topGridTiles.Add(tile);	
	}


	//function to remove current tile in the top grid
	public void RemoveButton(int curTile){
		//remove from the top tiles array 
		//fillgridspace is a ref to the tile obj
		//current tile is the ref to the position of the button in the array
		//so we know which one has been clicked on
	
       topGridTiles.RemoveAt(curTile);	
	   
	   //replace the tile clicked with the empty placeholder tile image
	   //
        topGridSpace[topGridTiles.Count].GetComponentInChildren<Image>().sprite = 
		emptyTile.GetComponentInChildren<Image>().sprite;
		//do the same for the text
		topGridSpace[topGridTiles.Count].GetComponentInChildren<Text>().text = 
		emptyTile.GetComponentInChildren<Text>().text;	
	}


	public void PlaySoundButton(){
		//Empty all at the start
		output.Clear();
		//loop tru the tiles in the top row and take all the strings from them
		for (int i = 0; i < topGridTiles.Count; i++){
			output.Add(topGridTiles[i].GetComponentInChildren<Text>().text);
		}	
		//Debug.Log(topGridTiles.Count);	
		Debug.Log(string.Join("", output.ToArray()));	

		//tts goes here
		ttsSpeech.GetComponent<TTSmanager>()._inputText = string.Join("", output.ToArray());
		ttsSpeech.GetComponent<TTSmanager>().PlaySpeech();		
	}


//Play TTS when a single tile is tapped
	public void playTTS(Button tile){
		ttsSpeech.GetComponent<TTSmanager>()._inputText = tile.GetComponentInChildren<Text>().text;
		ttsSpeech.GetComponent<TTSmanager>().PlaySpeech();
	}

//Play TTS when categories on the home page are tapped
//Wait before TTS begins create a short delay
	private IEnumerator WaitAndSay() {
		yield return new WaitForSeconds(0.1f);
		ttsSpeech.GetComponent<TTSmanager>()._inputText = sceneName;
		ttsSpeech.GetComponent<TTSmanager>().PlaySpeech();
		Debug.Log(sceneName);
	}

	public void QuitGame () {
 		Application.Quit ();
		 ttsSpeech.GetComponent<TTSmanager>()._inputText = "Closing App";
		//Just to make sure its working
		Debug.Log("Game is exiting");
 	}


}//class closed here
