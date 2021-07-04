using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Camera mainCamera;
    public int startAmmo;
    public int currentAmmo;
    public float fireDelay;
    private float nextTimeToFire;
    public float range;
    public Text ammoText;
    public bool isRealoading;
    public float realoadTime;

    public GameObject muzzleFlash;
    public float damage;

    public float ShakeDuration;
    public float ShakeMagnitude;

    public bool shouldShake;

    public string ShootAnimation;

    public KeyboardActions keyBoard;

    void Awake()
    {
        keyBoard = new KeyboardActions();
    }   

    void Start()
    {
        currentAmmo = startAmmo;
    }

    void OnEnable()
    {
        keyBoard.Enable();
    }

    void OnDisable()
    {
        keyBoard.Disable();
    }

    
    void Update()
    {
        if(isRealoading == true)
            return;

        if(currentAmmo < startAmmo && keyBoard.Controls.Reload.ReadValue<float>() == 1)
            StartCoroutine(Reaload());

        if(Time.time > nextTimeToFire)
        {
            if (keyBoard.Controls.Shoot.ReadValue<float>() == 1)
            {
                nextTimeToFire = Time.time + fireDelay;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.SetActive(true);
        if(shouldShake == true)
        {
            ScreenShake shake = mainCamera.GetComponent<ScreenShake>();
            if(shake != null)
                StartCoroutine(shake.Shake(ShakeDuration, Random.Range(-ShakeMagnitude, ShakeMagnitude)));

            
        }

        Animator animator = GetComponent<Animator>();
        if(animator != null)
        {
            animator.Play(ShootAnimation);
        }

        GetComponent<AudioSource>().Play();
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, range))
        {

        }

        currentAmmo--;
        Invoke("DisableMuzzleFlash", 0.25f);

        ammoText.text = currentAmmo.ToString() + "/" + startAmmo.ToString();
        if(currentAmmo == 0)
            StartCoroutine(Reaload());
    }

    public void DisableMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    public IEnumerator Reaload()
    {
        isRealoading = true;
        yield return new WaitForSeconds(realoadTime);
        isRealoading = false;
        currentAmmo = startAmmo;
        ammoText.text = currentAmmo.ToString() + "/" + startAmmo.ToString();
    }
}
