using UnityEngine;
using UnityEngine.InputSystem;

public class FireExtinguisherController : MonoBehaviour
{
    [SerializeField] private ParticleSystem hoseParticle;
    private InputSystem_Actions inputSystem;
    private InputAction hoseAction;
    private InputAction openPinAction;
    private Animator animator;
    [SerializeField] private bool openedPin = false;
    [SerializeField] private float sprayRange = 10f;
    [SerializeField] private float extinguishRate = 20f; // decrease per second
    [SerializeField] private Transform nozzleTip;
    [SerializeField] private ParticleSystem groundHitParticlePrefab;
    private bool hasSpawnedGroundEffect = false;


    #region Unity Methods
    void Awake()
    {
        animator = GetComponent<Animator>();
    }   

    void Update()
    {
        RotateWithCamera();
        UpdateFireHitEffect();
    }
    #endregion  

    void OnEnable(){
        inputSystem = new InputSystem_Actions();
        hoseAction = inputSystem.Player.Attack;
        hoseAction.performed += ctx => {
            if(!openedPin) return;
            OnFireHose();
        };  
        hoseAction.canceled += ctx => {
            if(!openedPin) return;
            OnStopHose();
        };

        openPinAction = inputSystem.Player.OpenPin;
        openPinAction.performed += ctx => {
            OnOpenPin();
        };
        inputSystem.Enable();
    }

    void OnDisable()    
    {
        inputSystem.Disable();
    }
    #region Animation Events
    void OnOpenPin()
    {
        animator.Play("OpenPin");
        openedPin = true;
    }
    public void OnFireHose()
    {
        animator.Play("FireHose");
    }
    public void OnStopHose()
    {
        animator.Play("UnFireHose");
    }

    public void PlayHoseEffect(){
        hoseParticle.Play();
    }

    public void StopHoseEffect(){
        if(hoseParticle.isPlaying){
            hoseParticle.Stop();
        }
    }
 
    #endregion

    #region Helper Methods
    void RotateWithCamera()
    {
        Transform cam = Camera.main.transform;

        // Match the extinguisher rotation to camera rotation
        transform.rotation = Quaternion.Euler(
            cam.eulerAngles.x,
            cam.eulerAngles.y,
            cam.eulerAngles.z
        );
    }


    void UpdateFireHitEffect()
    {
        if (!hoseParticle.isPlaying) return; // only work when spraying

        Ray ray = new Ray(nozzleTip.transform.position, -nozzleTip.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, sprayRange))
        {
            FireObject fire = hit.collider.GetComponent<FireObject>();

            if (fire != null)
            {
                print("Hitting fire object");
                fire.ReduceFire(extinguishRate * Time.deltaTime);
            }
            else
            {
                print("Not hitting fire object");
            }
        }
    }


    void OnDrawGizmos()
    {
        // Warna garis
        Gizmos.color = Color.cyan;

        // Jika nozzleTip ada, pakai itu sebagai origin
        Vector3 origin = nozzleTip != null ? nozzleTip.position : transform.position;
        Vector3 direction = nozzleTip != null ? nozzleTip.forward : transform.forward;

        // Gambar garis jangkauan semprot
        Gizmos.DrawLine(origin, origin + direction * sprayRange);

        // Gambar sphere kecil di ujung garis
        Gizmos.DrawSphere(origin + direction * sprayRange, 0.1f);

        // Gambar posisi nozzle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, 0.05f);
    }
    #endregion
}


