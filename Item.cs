using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { APPLE, BANANA, BREAD, CEREAL, EGG, KETCHUP, HAMBURGER, MEAT, CHEESE, PEAR, TOMATO, FORK, KNIFE, PAN, PLATE, CHICKEN, DEEP_PAN }

public abstract class Item : MonoBehaviour
{

    [SerializeField] public string itemName;
    [SerializeField] public ItemType itemType;
    [SerializeField] public UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable xRGrabInteractable;
    [SerializeField] public AudioClip itemAudioClip;
    [SerializeField] public Vector3 originalPosition;
    [SerializeField] public Quaternion originalRotation;
    public bool originalPositionAssigned;
    public AudioManager audioManager;

    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
    }

    public void SetOriginalPosition(Vector3 position) { originalPosition = position; }

    public ItemType GetItemType() { return itemType; }

    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    public Quaternion GetOriginalRotation() { return originalRotation; }    

    public void SetDraggable(bool draggable) { 
        xRGrabInteractable.enabled = draggable;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void SetAudioManager(AudioManager am) { audioManager = am; }

    public void PlayAudioClip()
    {
        if (itemAudioClip != null)
        {
           audioManager.PlayItemSound(itemAudioClip, itemName);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            this.gameObject.transform.position = originalPosition;
        }else if (collision.gameObject.layer == 11)
        {
            if (!originalPositionAssigned)
            {
                originalPosition = this.transform.position;
                originalPositionAssigned = true;
            }
        }
    }
}
