using UnityEngine;
using TreeSharpPlus;
using System;
using System.Collections;
using System.Collections.Generic;

using RootMotion.FinalIK;

public class CharacterMecanim : MonoBehaviour
{
    public const float MAX_REACHING_DISTANCE = 1.1f;
    public const float MAX_REACHING_HEIGHT = 2.0f;
    public const float MAX_REACHING_ANGLE = 100;

    /** DAVE NEW - OBJECT INTERACTION**/
    
    /** START **/

    public GameObject FE_ground;
    public GameObject FE_hand;
    public GameObject ExtinguisherParticles;
    public GameObject FireParticles;
    public GameObject Gun_hand;
    
    /** END **/



    private GameObject COP;
    private GameObject thisCharacter;
    private GameObject lastHitCharacter;
    private List<GameObject> hitCharacters = new List<GameObject>();
    public bool helpme_interact = false;
    public bool helpme_interact2 = false;
    public bool criminal_interact = false;
    public bool hostage_interact = false;
    public bool button_interact = false;
    public bool extinguisher_interact = false;
    public bool has_extinguisher = false;
    public bool fire_interact = false;
    public bool cop_orient = false;
    public Canvas speechBubble = null;
    public int win_count = 0;

    private Dictionary<FullBodyBipedEffector, bool> triggers;
    private Dictionary<FullBodyBipedEffector, bool> finish;

    [HideInInspector]
    public BodyMecanim Body = null;

    void Awake() { this.Initialize(); }

    /** DAVE NEW FUNCTIONS START **/

    void ShowExtinguisher()
    {

        FE_hand.SetActive(true);
        FE_ground.SetActive(false);
    }

    void ShowExtinguisherParticles()
    {
        ExtinguisherParticles.SetActive(true);
        Invoke("DontShowExtinguisherParticles", 1F);
        Invoke("DisableFire", 1F);
    }

    void DontShowExtinguisherParticles()
    {
        ExtinguisherParticles.SetActive(false);
    }

    void DisableFire()
    {
        FireParticles.SetActive(false);
    }

    /** DAVE NEW FUNCTIONS END **/


    void Start()
    {
        GameObject[] cops = GameObject.FindGameObjectsWithTag("Cop");
        thisCharacter = this.gameObject;

        foreach (GameObject cop in cops)
        {
            COP = cop;
        }

        speechBubble.enabled = false;
    }

    private System.Diagnostics.Stopwatch speechTimer = new System.Diagnostics.Stopwatch();
    void Update()
    {
        if (speechBubble != null)
        {
            if (helpme_interact == true && speechTimer.ElapsedMilliseconds < 10000)
            {
                speechTimer.Start();
                speechBubble.enabled = true;
            }
            else
            {
                speechTimer.Stop();
                speechTimer.Reset();
                speechBubble.enabled = false;
                helpme_interact = false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {

            // Find where the ray hit
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Detect who the ray hit and allow interactions
            if (Physics.Raycast(ray, out hit) == true)
            {
                lastHitCharacter = hit.transform.gameObject;
                Vector3 characterPosition = hit.transform.gameObject.transform.position;
                Vector3 copPosition = COP.transform.position;
                if (hit.transform.gameObject.CompareTag("HelpMe") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 3 && hit.transform.gameObject == thisCharacter)
                {
                    helpme_interact = true;
                    Debug.Log(thisCharacter.name + " Clicked");

                }
                if (hit.transform.gameObject.CompareTag("HelpMe") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 3 && COP == thisCharacter)
                {
                    helpme_interact2 = true;

                }
                if (hit.transform.gameObject.CompareTag("Criminal") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 2 && (hit.transform.gameObject == thisCharacter || COP == thisCharacter))
                {
                    criminal_interact = true;
                    bool seen = false;
                    if (hitCharacters.Count != 0) {
                        for (int i = 0; i < hitCharacters.Count; i++)
                        { 
                            if (hitCharacters[i] == lastHitCharacter)
                            {
                                seen = true;
                            }
                        }
                    }
                    if (seen == false)
                    {
                        hitCharacters.Add(lastHitCharacter);
                        win_count = win_count + 1;
                    }

                }
                if (hit.transform.gameObject.CompareTag("Hostage") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 2 && (hit.transform.gameObject == thisCharacter || COP == thisCharacter))
                {
                    hostage_interact = true;
                    bool seen = false;
                    if (hitCharacters.Count != 0)
                    {
                        for (int i = 0; i < hitCharacters.Count; i++)
                        {
                            if (hitCharacters[i] == lastHitCharacter)
                            {
                                seen = true;
                            }
                        }
                    }
                    if (seen == false)
                    {
                        hitCharacters.Add(lastHitCharacter);
                        win_count = win_count + 1;
                    }

                }
                if (hit.transform.gameObject.CompareTag("ButtonDoor1") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 3 && COP == thisCharacter)
                {
                    button_interact = true;
                    Debug.Log("Button Clicked");

                }
                if (hit.transform.gameObject.CompareTag("Extinguisher") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 3 && COP == thisCharacter)
                {
                    extinguisher_interact = true;
                    has_extinguisher = true;

                }
                if (hit.transform.gameObject.CompareTag("Fire") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 5 && COP == thisCharacter && has_extinguisher)
                {
                    fire_interact = true;
                    bool seen = false;
                    if (hitCharacters.Count != 0)
                    {
                        for (int i = 0; i < hitCharacters.Count; i++)
                        {
                            if (hitCharacters[i] == lastHitCharacter)
                            {
                                seen = true;
                            }
                        }
                    }
                    if (seen == false)
                    {
                        hitCharacters.Add(lastHitCharacter);
                        win_count = win_count + 1;
                    }

                }
            }
        }

    }

    /*void OnMouseDown()
    {
        // Reset all interaction variables to allow new interactions
        helpme_interact = false;
        criminal_interact = false;
        hostage_interact = false;
        button_interact = false;

        // Find where the ray hit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Detect who the ray hit and allow interactions
        if (Physics.Raycast(ray, out hit) == true)
        {
            Vector3 characterPosition = hit.transform.gameObject.transform.position;
            Vector3 copPosition = COP.transform.position;
            Debug.Log("Tag clicked: " + hit.transform.gameObject.tag);
            if (hit.transform.gameObject.CompareTag("HelpMe") && Mathf.Abs(Vector3.Distance(characterPosition,copPosition)) < 2 && hit.transform.gameObject == thisCharacter)
            {
                helpme_interact = true;
                Debug.Log(thisCharacter.name + " Clicked");

            }
            else if (hit.transform.gameObject.CompareTag("Criminal") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 2 && hit.transform.gameObject == thisCharacter)
            {
                criminal_interact = true;
                Debug.Log(thisCharacter.name + " Clicked");

            }
            else if (hit.transform.gameObject.CompareTag("Hostage") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 2 && hit.transform.gameObject == thisCharacter)
            {
                hostage_interact = true;
                Debug.Log(thisCharacter.name + " Clicked");

            }
            else if (hit.transform.gameObject.CompareTag("ButtonDoor1") && Mathf.Abs(Vector3.Distance(characterPosition, copPosition)) < 4)
            {
                button_interact = true;
                Debug.Log(thisCharacter.name + " Clicked");

            }

        }

        /*GameObject[] allCharacters = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject character in allCharacters)
        {
            float dist = Vector3.Distance(character.transform.position, COP.transform.position);
            if(character.tag == "HelpMe" && Mathf.Abs(dist) < 3) { Debug.Log("Left click on dude who needs help!"); }
            if (character.tag == "Criminal" && Mathf.Abs(dist) < 3) { Debug.Log("Left click on criminal!"); }
            if (character.tag == "Hostage" && Mathf.Abs(dist) < 3) { Debug.Log("Left click on hostage!"); }
            //if (Vector3.Distance(transform.position, allCharacters[i].transform.position) <= minimumDistance)
            //return true;
        }

        //return false;
    }*/

    /// <summary>
    /// Searches for and binds a reference to the Body interface
    /// </summary>
    public void Initialize()
    {
        this.Body = this.GetComponent<BodyMecanim>();
        this.Body.InteractionTrigger += this.OnInteractionTrigger;
        this.Body.InteractionStop += this.OnInteractionFinish;
    }

    private void OnInteractionTrigger(
        FullBodyBipedEffector effector, 
        InteractionObject obj)
    {
        if (this.triggers == null)
            this.triggers = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.triggers.ContainsKey(effector))
            this.triggers[effector] = true;
    }

    private void OnInteractionFinish(
        FullBodyBipedEffector effector,
        InteractionObject obj)
    {
        if (this.finish == null)
            this.finish = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.finish.ContainsKey(effector))
            this.finish[effector] = true;
    }

    #region Smart Object Specific Commands
    public virtual RunStatus WithinDistance(Vector3 target, float distance)
    {
        if ((transform.position - target).magnitude < distance)
            return RunStatus.Success;
        return RunStatus.Failure;
    }

    public virtual RunStatus Approach(Vector3 target, float distance)
    {
        Vector3 delta = target - transform.position;
        Vector3 offset = delta.normalized * distance;
        return this.NavGoTo(target - offset);
    }
    #endregion

    #region Navigation Commands
    /// <summary>
    /// Turns to face a desired target point
    /// </summary>
    public virtual RunStatus NavTurn(Val<Vector3> target)
    {
        this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
        this.Body.NavSetDesiredOrientation(target.Value);
        if (this.Body.NavIsFacingDesired() == true)
        {
            this.Body.NavSetOrientationBehavior(
                OrientationBehavior.LookForward);
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    public virtual RunStatus NavTurnButton(Val<Vector3> target)
    {
        if (button_interact)
        {
            if (thisCharacter == COP)
            {
                this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
                this.Body.NavSetDesiredOrientation(target.Value);
                if (this.Body.NavIsFacingDesired() == true)
                {
                    this.Body.NavSetOrientationBehavior(
                        OrientationBehavior.LookForward);
                    return RunStatus.Success;
                }
                return RunStatus.Running;
            }
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavTurnExtinguisher()
    {
        if (extinguisher_interact)
        {
            if (thisCharacter == COP)
            {
                this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
                this.Body.NavSetDesiredOrientation(lastHitCharacter.transform.position);
                if (this.Body.NavIsFacingDesired() == true)
                {
                    this.Body.NavSetOrientationBehavior(
                        OrientationBehavior.LookForward);
                    return RunStatus.Success;
                }
                return RunStatus.Running;
            }
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavTurnFire()
    {
        if (fire_interact)
        {
            if (thisCharacter == COP)
            {
                this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
                this.Body.NavSetDesiredOrientation(lastHitCharacter.transform.position);
                if (this.Body.NavIsFacingDesired() == true)
                {
                    this.Body.NavSetOrientationBehavior(
                        OrientationBehavior.LookForward);
                    return RunStatus.Success;
                }
                return RunStatus.Running;
            }
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavTurnCriminal()
    {
        if (criminal_interact)
        {
            if (thisCharacter == COP)
            {
                this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
                this.Body.NavSetDesiredOrientation(lastHitCharacter.transform.position);
                if (this.Body.NavIsFacingDesired() == true)
                {
                    this.Body.NavSetOrientationBehavior(
                        OrientationBehavior.LookForward);
                    return RunStatus.Success;
                }
                return RunStatus.Running;
            }
            if (thisCharacter == lastHitCharacter)
            {
                this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
                this.Body.NavSetDesiredOrientation(COP.transform.position);
                if (this.Body.NavIsFacingDesired() == true)
                {
                    this.Body.NavSetOrientationBehavior(
                        OrientationBehavior.LookForward);
                    return RunStatus.Success;
                }
                return RunStatus.Running;
            }
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavTurnHostage()
    {
        if (hostage_interact)
        {
            if(thisCharacter == COP)
            {
                this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
                this.Body.NavSetDesiredOrientation(lastHitCharacter.transform.position);
                if (this.Body.NavIsFacingDesired() == true)
                {
                    this.Body.NavSetOrientationBehavior(
                        OrientationBehavior.LookForward);
                    return RunStatus.Success;
                }
                return RunStatus.Running;
            }
            if (thisCharacter == lastHitCharacter)
            {
                this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
                this.Body.NavSetDesiredOrientation(COP.transform.position);
                if (this.Body.NavIsFacingDesired() == true)
                {
                    this.Body.NavSetOrientationBehavior(
                        OrientationBehavior.LookForward);
                    return RunStatus.Success;
                }
                return RunStatus.Running;
            }
        }
        return RunStatus.Success;
    }

    /// <summary>
    /// Turns to face a desired orientation
    /// </summary>
    public virtual RunStatus NavTurn(Val<Quaternion> target)
    {
        this.Body.NavSetOrientationBehavior(OrientationBehavior.None);
        this.Body.NavSetDesiredOrientation(target.Value);
        if (this.Body.NavIsFacingDesired() == true)
        {
            this.Body.NavFacingSnap();
            this.Body.NavSetOrientationBehavior(
                OrientationBehavior.LookForward);
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    /// <summary>
    /// Sets a custom orientation behavior
    /// </summary>
    public virtual RunStatus NavOrientBehavior(
        Val<OrientationBehavior> behavior)
    {
        this.Body.NavSetOrientationBehavior(behavior.Value);
        return RunStatus.Success;
    }

    public virtual RunStatus NavOrientBehaviorButton(
        Val<OrientationBehavior> behavior)
    {
        if (button_interact)
        {
            this.Body.NavSetOrientationBehavior(behavior.Value);
            return RunStatus.Success;
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavOrientBehaviorExtinguisher(
        Val<OrientationBehavior> behavior)
    {
        if (extinguisher_interact)
        {
            this.Body.NavSetOrientationBehavior(behavior.Value);
            return RunStatus.Success;
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavOrientBehaviorFire(
        Val<OrientationBehavior> behavior)
    {
        if (fire_interact)
        {
            this.Body.NavSetOrientationBehavior(behavior.Value);
            return RunStatus.Success;
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavOrientBehaviorCriminal(
        Val<OrientationBehavior> behavior)
    {
        if (criminal_interact)
        {
            this.Body.NavSetOrientationBehavior(behavior.Value);
            return RunStatus.Success;
        }
        return RunStatus.Success;
    }

    public virtual RunStatus NavOrientBehaviorHostage(
        Val<OrientationBehavior> behavior)
    {
        if (hostage_interact)
        {
            this.Body.NavSetOrientationBehavior(behavior.Value);
            return RunStatus.Success;
        }
        return RunStatus.Success;
    }

    /// <summary>
    /// Sets a new navigation target. Will fail immediately if the
    /// point is unreachable. Blocks until the agent arrives.
    /// </summary>
    public virtual RunStatus NavGoTo(Val<Vector3> target)
    {
        if (this.Body.NavCanReach(target.Value) == false)
        {
            Debug.LogWarning("NavGoTo failed -- can't reach target");
            return RunStatus.Failure;
        }
        // TODO: I previously had this if statement here to prevent spam:
        //     if (this.Interface.NavTarget() != target)
        // It's good for limiting the amount of SetDestination() calls we
        // make internally, but sometimes it causes the character1 to stand
        // still when we re-activate a tree after it's been terminated. Look
        // into a better way to make this smarter without false positives. - AS
        this.Body.NavGoTo(target.Value);
        if (this.Body.NavHasArrived() == true)
        {
            this.Body.NavStop();
            return RunStatus.Success;
        }
        return RunStatus.Running;
        // TODO: Timeout? - AS
    }

    public virtual RunStatus NavGoToHostage()
    {
        if (hostage_interact)
        {
            Vector3 hostageRun = new Vector3(0f, 0f, 0f);
            if (this.Body.NavCanReach(hostageRun) == false)
            {
                Debug.LogWarning("NavGoTo failed -- can't reach target");
                return RunStatus.Failure;
            }
            // TODO: I previously had this if statement here to prevent spam:
            //     if (this.Interface.NavTarget() != target)
            // It's good for limiting the amount of SetDestination() calls we
            // make internally, but sometimes it causes the character1 to stand
            // still when we re-activate a tree after it's been terminated. Look
            // into a better way to make this smarter without false positives. - AS
            this.Body.NavGoTo(hostageRun);
            if (this.Body.NavHasArrived() == true)
            {
                this.Body.NavStop();
                return RunStatus.Success;
            }
            return RunStatus.Running;
        }
        return RunStatus.Success;
        // TODO: Timeout? - AS
    }

    /// <summary>
    /// Lerps the character towards a target. Use for precise adjustments.
    /// </summary>
    public virtual RunStatus NavNudgeTo(Val<Vector3> target)
    {
        bool? result = this.Body.NavDoneNudge();
        if (result == null)
        {
            this.Body.NavNudge(target.Value, 0.3f);
        }
        else if (result == true)
        {
            this.Body.NavNudgeStop();
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    private IEnumerator<RunStatus> snapper;

    private RunStatus TickSnap(
        Vector3 position,
        Vector3 target,
        float time = 1.0f)
    {
        if (this.snapper == null)
            this.snapper = 
                SnapToTarget(position, target, time).GetEnumerator();
        if (this.snapper.MoveNext() == false)
        {
            this.snapper = null;
            return RunStatus.Success;
        }
        return snapper.Current;
    }

    private IEnumerable<RunStatus> SnapToTarget(
        Vector3 position,
        Vector3 target,
        float time)
    {
        Interpolator<Vector3> interp =
            new Interpolator<Vector3>(
                position,
                target,
                Vector3.Lerp);
        interp.ForceMin();
        interp.ToMax(time);

        while (interp.State != InterpolationState.Max)
        {
            transform.position = interp.Value;
            yield return RunStatus.Running;
        }
        yield return RunStatus.Success;
        yield break;
    }

	/// <summary>
	/// Stops the Navigation system. Blocks until the agent is stopped.
	/// </summary>
	public virtual RunStatus NavStop()
    {
        this.Body.NavStop();
        if (this.Body.NavIsStopped() == true)
            return RunStatus.Success;
        return RunStatus.Running;
        // TODO: Timeout? - AS
    }

    public virtual RunStatus NavStopHostage()
    {
        if (hostage_interact)
        {
            this.Body.NavStop();
            if (this.Body.NavIsStopped() == true)
                hostage_interact = false;
            return RunStatus.Success;
            return RunStatus.Running;
        }
        return RunStatus.Success;
        // TODO: Timeout? - AS
    }
    #endregion

    #region Interaction Commands
    public virtual RunStatus WaitForTrigger(
        Val<FullBodyBipedEffector> effector)
    {
        if (this.triggers == null)
            this.triggers = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.triggers.ContainsKey(effector.Value) == false)
            this.triggers.Add(effector.Value, false);
        if (this.triggers[effector.Value] == true)
        {
            this.triggers.Remove(effector.Value);
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    public virtual RunStatus WaitForFinish(
        Val<FullBodyBipedEffector> effector)
    {
        if (this.finish == null)
            this.finish = new Dictionary<FullBodyBipedEffector, bool>();
        if (this.finish.ContainsKey(effector.Value) == false)
            this.finish.Add(effector.Value, false);
        if (this.finish[effector.Value] == true)
        {
            this.finish.Remove(effector.Value);
            return RunStatus.Success;
        }
        return RunStatus.Running;
    }

    public virtual RunStatus StartInteraction(
        Val<FullBodyBipedEffector> effector, 
        Val<InteractionObject> obj)
    {
        this.Body.StartInteraction(effector, obj);
        return RunStatus.Success;
    }

    public virtual RunStatus ResumeInteraction(
        Val<FullBodyBipedEffector> effector)
    {
        this.Body.ResumeInteraction(effector);
        return RunStatus.Success;
    }

    public virtual RunStatus StopInteraction(Val<FullBodyBipedEffector> effector)
    {
        this.Body.StopInteraction(effector);
        return RunStatus.Success;
    }	
    #endregion

    #region HeadLook Commands
    public virtual RunStatus HeadLookAt(Val<Vector3> target)
    {
        this.Body.HeadLookAt(target);
        return RunStatus.Success;
    }

    public virtual RunStatus HeadLookStop()
    {
        this.Body.HeadLookStop();
		return RunStatus.Success;
	}
    #endregion

    #region Animation Commands
    public virtual RunStatus FaceAnimation(
        Val<string> gestureName, Val<bool> isActive)
    {
        this.Body.FaceAnimation(gestureName.Value, isActive.Value);
		return RunStatus.Success;
	}
	
	public virtual RunStatus HandAnimation(
        Val<string> gestureName, Val<bool> isActive)
    {
        this.Body.HandAnimation(gestureName.Value, isActive.Value);
		return RunStatus.Success;
	}

    public virtual RunStatus HandAnimationHelpMe(
        Val<string> gestureName, Val<bool> isActive)
    {
        if (helpme_interact == false)
        {
            this.Body.HandAnimation(gestureName.Value, isActive.Value);
            return RunStatus.Success;
        }
        return RunStatus.Success;
    }

    public virtual RunStatus HandAnimationHelpMe2(
        Val<string> gestureName, Val<bool> isActive)
    {
        if (helpme_interact2)
        {
            this.Body.HandAnimation(gestureName.Value, isActive.Value);
            helpme_interact2 = false;
            return RunStatus.Success;
        }
        this.Body.HandAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public virtual RunStatus HandAnimationButton(
        Val<string> gestureName, Val<bool> isActive)
    {
        if (button_interact)
        {
            this.Body.HandAnimation(gestureName.Value, isActive.Value);
            button_interact = false;
            return RunStatus.Success;
        }
        this.Body.HandAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public virtual RunStatus HandAnimationFire(
        Val<string> gestureName, Val<bool> isActive)
    {
        if (fire_interact)
        {
            this.Body.HandAnimation(gestureName.Value, isActive.Value);
            fire_interact = false;
            Invoke("ShowExtinguisherParticles", 1.2F); /** DAVE ADDED **/
            return RunStatus.Success;
        }
        this.Body.HandAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public virtual RunStatus HandAnimationCriminal(
        Val<string> gestureName, Val<bool> isActive)
    {
        if (criminal_interact)
        {
            this.Body.HandAnimation(gestureName.Value, isActive.Value);
            criminal_interact = false;
            return RunStatus.Success;
        }
        this.Body.HandAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public virtual RunStatus HandAnimationHostage(
        Val<string> gestureName, Val<bool> isActive)
    {
        if (hostage_interact)
        {
            this.Body.HandAnimation(gestureName.Value, isActive.Value);
            hostage_interact = false;
            return RunStatus.Success;
        }
        this.Body.HandAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public virtual RunStatus BodyAnimation(Val<string> gestureName, Val<bool> isActive)
	{
		this.Body.BodyAnimation(gestureName.Value, isActive.Value);
		return RunStatus.Success;
	}

    public virtual RunStatus BodyAnimationButton(Val<string> gestureName, Val<bool> isActive)
    {
        if (button_interact)
        {
            this.Body.BodyAnimation(gestureName.Value, isActive.Value);
            button_interact = false;
            return RunStatus.Success;
        }
        this.Body.BodyAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public virtual RunStatus BodyAnimationExtinguisher(Val<string> gestureName, Val<bool> isActive)
    {
        if (extinguisher_interact)
        {
            Debug.Log("Extinguisher motion called!");
            this.Body.BodyAnimation(gestureName.Value, isActive.Value);
            extinguisher_interact = false;
            Invoke("ShowExtinguisher", 1.0F); /** DAVE ADDED **/
            return RunStatus.Success;
        }
        //this.Body.BodyAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }

    public virtual RunStatus BodyAnimationCriminal(Val<string> gestureName, Val<bool> isActive)
    {
        if (criminal_interact)
        {
            if(thisCharacter != COP)
            {
                Wait();
            }
            Debug.Log("Criminal action invoked!");
            this.Body.BodyAnimation(gestureName.Value, isActive.Value);
            criminal_interact = false;
            return RunStatus.Success;
        }
        this.Body.BodyAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public virtual RunStatus BodyAnimationHostage(Val<string> gestureName, Val<bool> isActive)
    {
        if (hostage_interact)
        {
            this.Body.BodyAnimation(gestureName.Value, isActive.Value);
            hostage_interact = false;
            return RunStatus.Success;
        }
        this.Body.BodyAnimation(gestureName.Value, false);
        return RunStatus.Success;
    }

    public RunStatus ResetAnimation()
    {
        this.Body.ResetAnimation();
        return RunStatus.Success;
    }
    #endregion

    #region Sitting Commands
    /// <summary>
    /// Sits the character down
    /// </summary>
    public virtual RunStatus SitDown()
    {
        if (this.Body.IsSitting() == true)
            return RunStatus.Success;
        this.Body.SitDown();
        return RunStatus.Running;
    }

    /// <summary>
    /// Stands the character up
    /// </summary>
    public virtual RunStatus StandUp()
    {
        if (this.Body.IsStanding() == true)
            return RunStatus.Success;
        this.Body.StandUp();
        return RunStatus.Running;
    }
    #endregion
}
