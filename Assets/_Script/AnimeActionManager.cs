using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeActionManager : MonoBehaviour {
	


    //protected List<AnimeAction

    public AnimeAction currentAction = null;

    protected List<AnimeAction> mActionList = new List<AnimeAction>();

        
    protected List<AnimeAction> mDoneList = new List<AnimeAction>();    
    protected List<AnimeAction> mNewActionQueue = new List<AnimeAction>();    

    public void Reset() {
        mActionList.Clear();
        mDoneList.Clear();
        mNewActionQueue.Clear();
    }

    public void RunAction(AnimeAction action) {
        Reset();
        StartAction(action);
    }

    public void StartAction(AnimeAction action) {
        if(action == null) {
            return;
        }

        action.SetManager(this);
        action.Start();
        mActionList.Add(action); 
    }

    public void QueueNewAction(AnimeAction action) {
        if(action == null) {
            return;
        }
        Debug.Log("INFO: ActionManager: " + action.name + " is queued");

        // action.SetManager(this);
        // action.Start();

        mNewActionQueue.Add(action);    // update loop will run it 
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {   
        mDoneList.Clear();

    

        // 
        foreach(AnimeAction action in mActionList) {
            if(action.IsStarted() == false) {
                continue;
            }

            action.Update(Time.deltaTime);

            if(action.IsDone()) {
                mDoneList.Add(action);
            }
        }


        // Remove the Done List 
        foreach(AnimeAction action in mDoneList) {
            mActionList.Remove(action);
        }
        
        StartQueuedActions();
    }

    void StartQueuedActions() {
        foreach(AnimeAction action in mNewActionQueue) {
            StartAction(action);
        }

        mNewActionQueue.Clear();
    }

    // public void Run() {
    //     if(currentAction == null) {
    //         Debug.Log("AnimeActionManager: action isn't defined");
    //         return;
    //     }

    //     if(currentAction.IsStarted() == true) {
    //         Debug.Log("AnimeActionManager: action already started");
    //         return;
    //     }

    //     if(currentAction.IsDone() == true) {
    //         Debug.Log("AnimeActionManager: action has done");
    //         return;
    //     }

    //     currentAction.Start();
    // }    
}

	
