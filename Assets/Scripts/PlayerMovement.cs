using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;


    Rigidbody rb;
    AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if(thrust.IsPressed()){
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngineSFX);
            }
            if(!mainEngineParticles.isPlaying){
                mainEngineParticles.Play();
            }
            
        }
        else{
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    void ProcessRotation(){
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0){
            ApplyRotation(rotationSpeed);
            if(!rightEngineParticles.isPlaying){
                leftEngineParticles.Stop();
                rightEngineParticles.Play();
            }
        }
        else if(rotationInput > 0){
            ApplyRotation(-rotationSpeed);
            if(!leftEngineParticles.isPlaying){
                rightEngineParticles.Stop();
                leftEngineParticles.Play();
            }
        }
        else{
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
    }
    void ApplyRotation(float rotationInFrame){
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationInFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

}
