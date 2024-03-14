using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CollisionHandle : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem crash;
    [SerializeField] ParticleSystem successCrash;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;  
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if(Input.GetKey(KeyCode.L)) 
        {
         NextLevel();
        }
        if(Input.GetKey(KeyCode.C))
        {
            collisionDisabled=!collisionDisabled;
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled ) { return; }
        switch (other.gameObject.tag)
        {
            case "start":
                Debug.Log("baþlangýc");
                break;
            case "Finish":
                startSuccessSequence();
                break;
           
            default:
                StartCrashSequence();
                break;

        }

    }
    void StartCrashSequence()
    {
       isTransitioning=true;
        
        audioSource.PlayOneShot(death);
        crash.Play();
        GetComponent<movement>().enabled = false;  
        Invoke("ReloadLevel", loadDelay);
    }
    void startSuccessSequence()
    {

        isTransitioning = true;
        audioSource.PlayOneShot(success);
        successCrash.Play();
        GetComponent<movement>().enabled = false;     
        Invoke("NextLevel", loadDelay);
    }
    void NextLevel()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
