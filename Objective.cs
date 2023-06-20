using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameManager gameManager;
    public TutorialManager tutorialManager;
    public AudioManager audioManager;
    public List<Item> objectiveItems;
    public Item resultItem;
    public bool destroyObjectiveItems;
    private bool objetiveCompleted;
    public Vector3 objetivePosition;
    public Objective brotherObjective;
    public int objectivePoints;
    public AudioClip completedObjectiveAudioClip;
    public AudioClip correctItemAudioClip;
    public AudioClip wrongItemAudioClip;
    public List<Transform> objectiveTransforms;

    void Start()
    {
        objetiveCompleted = false;
        objetivePosition = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z);

    }

    private bool IsObjetiveItem(Item item)
    {
        for(int i = 0; i < objectiveItems.Count; i++)
        {
            if(objectiveItems[i].GetItemType() == item.GetItemType()) { return true; }
        }
        return false;
    }

    private void RemoveObjetiveItem(Item item)
    {
        for (int i = 0; i < objectiveItems.Count; i++)
        {
            if (objectiveItems[i].GetItemType() == item.GetItemType()) {
                objectiveItems.Remove(objectiveItems[i]);
                Debug.Log(i);
            }
        }
    }

    private void AccomplishObjectiveItem(Item item)
    {
        Debug.Log(item.GetItemType());
        RemoveObjetiveItem(item);
        if(objectiveTransforms.Count <= 0) 
        { 
            item.gameObject.transform.localPosition = objetivePosition;
        }
        else
        {
            item.gameObject.transform.localPosition = new Vector3(objectiveTransforms[0].localPosition.x, objectiveTransforms[0].localPosition.y, objectiveTransforms[0].localPosition.z);
            objectiveTransforms.RemoveAt(0);
        }
        item.gameObject.transform.rotation = item.GetOriginalRotation();
        item.SetDraggable(false);
        if (destroyObjectiveItems)
        {
          item.DestroyItem();
        }
        if (brotherObjective != null)
        {
            brotherObjective.RemoveObjetiveItem(item);
            objectiveItems.Clear();
        }
        if(objectiveItems.Count == 0)
        {
            audioManager.PlaySoundEffect(completedObjectiveAudioClip, 1f);
            if (resultItem != null)
            {
                resultItem.SetAudioManager(audioManager);
                Instantiate(resultItem.gameObject,transform.position, Quaternion.identity);
            }
            if (gameManager != null)
            {
                gameManager.CompletedObjective();
            }
            if(tutorialManager != null)
            {
                tutorialManager.CompletedObjective();
            }
        }
        else
        {
            audioManager.PlaySoundEffect(correctItemAudioClip, 1f);
        }
    }

    private void CompletedObjective()
    {
        objetiveCompleted = true;
        if(gameManager != null) { gameManager.CompletedObjective(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Item>()!=null) {
            Item collisionItem = collision.gameObject.GetComponent<Item>();
            Debug.Log(collisionItem.GetItemType());
            if (IsObjetiveItem(collisionItem)) {
                AccomplishObjectiveItem(collisionItem);
            }
            else
            {
                audioManager.PlaySoundEffect(wrongItemAudioClip, 1f);
            }
        }
    }

    public Vector3 GetObjetivePosition() { return objetivePosition; }

    public int GetObjectivePoints() { return objectivePoints; }
}
