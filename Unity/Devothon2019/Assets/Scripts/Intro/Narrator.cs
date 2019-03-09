using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Narrator : MonoBehaviour {

	public Text obj_Text;
	public GameObject messageBox;

	public AudioSource sonTexte;

	public int textSpeed;
	private static Queue<DialogText> messageQueue = new Queue<DialogText>();
    public static bool isTalking = false;
	private DialogText currentText;
	private bool isKeyDown = false;
	private int playSound = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine(manageText());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && currentText != null) {
            isKeyDown = true;
        }
    }

	public void SayText(bool p_Skippable, string p_Text)
	{
		DialogText temp = new DialogText(p_Skippable, p_Text);
		messageQueue.Enqueue(temp);
	}

    public static void SayTextStatic(bool skip, string text) {
        DialogText temp = new DialogText(skip, text);
        messageQueue.Enqueue(temp);
    }

    public static void SayTextStatic(DialogText text) {
        messageQueue.Enqueue(text);
    }

	private IEnumerator manageText()
	{

		bool skipped = false;

		while(true)
		{
			if(messageQueue.Count > 0)
			{
				messageBox.SetActive(true);
                isTalking = true;
				currentText = messageQueue.Dequeue();
				foreach(char c in currentText.message)
				{
					if(currentText.isSkippable && isKeyDown)
					{
						isKeyDown = false;
						obj_Text.text = currentText.message;
						skipped = true;
						break;
					}
					else
					{
						obj_Text.text += c;
						if(playSound == 0)
						{
							sonTexte.Play();
							playSound = 1;
						}
						else
						{
							playSound--;
						}
							

						yield return new WaitForSeconds(textSpeed/100f);
					}
				}

				if(skipped || currentText.isSkippable)
				{
					skipped = false;
					yield return new WaitForSeconds(0.167f);
					yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
					isKeyDown = false;
				}
				else
				{
					yield return new WaitForSeconds(2.00f);
				}
				
				obj_Text.text = "";
                currentText = null;
			}
			else 
			{
				messageBox.SetActive(false);
                isTalking = false;
			}

			yield return null;
		}
	}
}

[System.Serializable]
public class DialogText
{
	public bool isSkippable;
	public string message;
	public DialogText(bool p_isSkippable, string p_message)
	{
		isSkippable = p_isSkippable;
		message = p_message;
	}
}
