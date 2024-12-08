using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    bool isControllable = true;
    AudioSource audioSource;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip sucsessSFX;
    [SerializeField] ParticleSystem SucsessParticle;
    [SerializeField] ParticleSystem CrashParticle;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if(!isControllable){return;}

        switch(other.gameObject.tag){
            case "finish":
                StartSucessesSequence();
                break;
            case "start":
                break;
            default:
                
                StartCrashSequence();
                break;
        }
    }
    void StartSucessesSequence(){
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(sucsessSFX);
        SucsessParticle.Play();
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("loadNextLevel",2f);
    }
    void StartCrashSequence(){
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionSFX);
        CrashParticle.Play();
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("ReloadLevel",2f);
    }
    void loadNextLevel(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx +1;
        if(nextSceneIdx == SceneManager.sceneCountInBuildSettings){
            nextSceneIdx = 0;
        }
        SceneManager.LoadScene(nextSceneIdx);
    }
    void ReloadLevel(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx);
    }
}
