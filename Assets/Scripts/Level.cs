using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump"))
        {
            SceneManager.LoadScene("TestScene");
        }
        else if(Input.GetButton("Enter"))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
