using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour {
    public GameObject target;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void LateUpdate() {
        this.transform.position = new Vector3(target.transform.position.x, transform.position.y, this.transform.position.z);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}