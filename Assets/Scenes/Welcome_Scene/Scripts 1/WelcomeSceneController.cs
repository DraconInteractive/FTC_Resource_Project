using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneController : MonoBehaviour {
	//Make a group of words
	public List<string> phrases;
	//Make a container for a Text component. It is public so we can set it in editor.
	public Text myText;
	//Make a number to track what text we are up to
	int textIndex;
	//Make a boolean (true/false) to track if we are using the complex version of the code. Public so we can set it in editor.
	public bool useComplex;
	//Create a tracker for our IEnumerator, if we use it.
	Coroutine ntRoutine;
	//Make a float that tells the complex function how long to wait between letters. Public so we can set it in editor.
	public float letterDelta;

	// Use this for initialization
	void Start () {
		//Set our current text to -1.
		//As we will call NextText immediately, this makes it start at 0
		textIndex = -1;

		//If our complex boolean is true...
		if (useComplex) {
			//Initialise our Text (complexly) with a 0.1 second delay between letters. Store the function in 'ntRoutine'.
			ntRoutine = StartCoroutine (NextTextComplex ());
		} else {
			//Initialise our Text (simply)
			NextText();
		}

	}
	
	// Update is called once per frame
	void Update () {
		//If we press Space...
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (useComplex) {
				//Check 'ntRoutine' to see if the letter function is already running. If it is, stop it.
				if (ntRoutine != null) {
					StopCoroutine (ntRoutine);
				}
				//Then start it again doing what we want it to do.
				ntRoutine = StartCoroutine (NextTextComplex ());
			} else {
				//Goto the next text
				NextText();
			}

		}
	}
	//A function to go to next text
	void NextText () {
		//Change our tracker number first
		textIndex += 1;
		//Check if we have enough words in our word container
		if (textIndex >= phrases.Count) {
			//if not, go back to the first word
			textIndex = 0;
		}
		//Set the text to the words we want.
		myText.text = phrases [textIndex];
	}

	//A more complicated function, that occurs over time. Takes an input of a decimal (float) number.
	IEnumerator NextTextComplex() {
		//Change our tracker number first
		textIndex += 1;
		//Check if we have enough words in our word container
		if (textIndex >= phrases.Count) {
			//if not, go back to the first word
			textIndex = 0;
		}
		//Set the text to the words we want.
		//myText.text = phrases [textIndex];
		myText.text = "";
		//If a word is a string of letters, a string is a string of char's. So turn our words into a collection of letters instead of a whole word.
		char[] letters = phrases [textIndex].ToCharArray ();
		//Make a number to count what letter we are up to.
		int charCounter = 0;
		//A while loop repeats its code again and again while its phrase is false. 
		//In this case, it keeps going while our Text component is not equal to the word it needs to show. 
		//These always need a 'yield' statement inside, or it will try to repeat infinitely, and crash!
		while (myText.text != phrases [textIndex]) {
			//Add on the letter we are up to
			myText.text += letters [charCounter];
			//Go to next letter
			charCounter++;
			//Wait for an amount of time between letters. Gives a cool effect, and is required for a while loop.
			yield return new WaitForSeconds (letterDelta);
		}
		//IEnumerator's need to be told when to exit. Thats what yield break does.
		yield break;
	}
}
