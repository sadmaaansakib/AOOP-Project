using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float jumpForce = 10f;

	public Rigidbody2D rb;
	public SpriteRenderer sr;

	private string currentColor;

	[Header("Player Colors")]
	public Color colorCyan;
	public Color colorYellow;
	public Color colorMagenta;
	public Color colorPink;

	private void Start()
	{
		SetRandomColor();
	}

	private void Update()
	{
		// Jump input handling
		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			Jump();
		}
	}

	private void Jump()
	{
		rb.velocity = Vector2.up * jumpForce;
	}



	//private void OnCollisionEnter2D(Collision2D other)
	//{
	//	//if (other.gameObject.CompareTag("CheckPoint"))
	//	//{
	//	//	Debug.Log("Checkpoint reached");
	//	//	// Load the "SampleScene" scene
	//	//	SceneManager.LoadScene("SampleScene");

	//	//	// Loader.Load(Loader.Scence.SampleScene);

	//	//}
 //       if (other.gameObject.CompareTag("BASELINE"))
 //       {
 //           Debug.Log("Checkpoint reached");
 //           // Load the "SampleScene" scene
 //           //GameFinished();

 //           // Loader.Load(Loader.Scence.SampleScene);

 //       }
 //   }

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("ColorChanger"))
		{
			// Change player color and destroy the ColorChanger object
			SetRandomColor();
			Destroy(col.gameObject);
			return;
		}

        // if (col.CompareTag("CheckPoint"))
        // {
        // 	Debug.Log("Checkpoint reached");
        // 	// Load the "SampleScene" scene
        // 	//SceneManager.LoadScene("SampleScene");
        // 	UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        // }

        else if (col.CompareTag("CheckPoint"))
        {
			Debug.Log("Checkpoint reached");
			// Load the "SampleScene" scene
			//GameFinished();

			Loader.Load(Loader.Scence.SampleScene2);

		}

		//else if (col.CompareTag("BASELINE"))
		//{
		//	Debug.Log("Checkpoint reached");
		//	// Load the "SampleScene" scene
		//	//GameFinished();
			
		//	// Loader.Load(Loader.Scence.SampleScene);

		//}
		// If the player's color does not match the collider's tag
		else if (col.tag != currentColor)
		{
			   Debug.Log("GAME OVER!");
			// Reload the current scene
			
              //Loader.Load(Loader.Scence.ColorSwitch);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(0f);

        Loader.Load(Loader.Scence.SampleScene2);

    }

    private void SetRandomColor()
	{
		int index = Random.Range(0, 4);

		switch (index)
		{
			case 0:
				currentColor = "Cyan";
				sr.color = colorCyan;
				break;
			case 1:
				currentColor = "Yellow";
				sr.color = colorYellow;
				break;
			case 2:
				currentColor = "Magenta";
				sr.color = colorMagenta;
				break;
			case 3:
				currentColor = "Pink";
				sr.color = colorPink;
				break;
		}
	}
}
