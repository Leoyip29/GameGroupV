using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MessageSpawner : MonoBehaviour
{
    public GameObject[] messages; // Array holdinjg messages for group chat
    private int messageCount = 0; // Counter for the number
    private float interval = 2.0f; // Time interval between messages

    public GameObject audioSourceGameObject;
    private AudioSource audioSource;

    private bool startScrolling = false;

    private float scrollTimer = 0.1f;
    private float scrollDuration = 0.3f;
    private float scrollSpeed = 210f; 
    
    private float sceneTransitionWaitTime = 36f; // Time after which the scene will change

    void Start()
    {
        audioSource = audioSourceGameObject.GetComponent<AudioSource>();
        StartCoroutine(DisplayMessages());
        StartCoroutine(TransitionToNextSceneAfterTime(sceneTransitionWaitTime)); 
    }

    // Spawn messages with regular delays and scrolling
    private IEnumerator DisplayMessages()
    {
        foreach (GameObject message in messages)
        {
            messageCount++;

            message.SetActive(true);
            audioSource.Play();
            yield return new WaitForSeconds(interval); 
            
            if (messageCount >= 8 && messageCount != 13)
            {
                startScrolling = true;
                scrollTimer = 0.1f;
            }
        }
    }

    // Transition to game scene
    IEnumerator TransitionToNextSceneAfterTime(float sceneTransitionWaitTime)
    {
        yield return new WaitForSeconds(sceneTransitionWaitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }

    void Update()
    {
        // Move chat messages object upwards
        if (startScrolling)
        {
            if (scrollTimer < scrollDuration)
            {
                // Continue moving text upwards
                transform.position = new Vector3(transform.position.x, transform.position.y + scrollSpeed * Time.deltaTime, transform.position.z);
                scrollTimer += Time.deltaTime;
            }
            else
            {
                // Stop scrolling after set time
                startScrolling = false;
            }
        }
    }
}
