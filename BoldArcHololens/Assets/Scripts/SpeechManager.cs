using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour {

    [SerializeField]
    private GameObject model;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;

    // Use this for initialization
    void Start()
    {
        keywordCollection = new Dictionary<string, KeywordAction>();
        //keywordCollection.Add("Move", MoveCommand);
        //keywordCollection.Add("Place", PlaceCommand);
        keywordCollection.Add("Reset World", ResetCommand);
        keywordCollection.Add("Explode", ExplodeCommand);
        keywordCollection.Add("Show Label", ShowLabelCommand);
        keywordCollection.Add("Place World", PlaceWorldCommand);
        keywordCollection.Add("Show Info", ShowInfoCommand);
        keywordCollection.Add("Place Plan", PlacePlanCommand);
        keywordCollection.Add("Show Plan", ShowPlanCommand);
        keywordCollection.Add("Hide Plan", HidePlanCommand);
        keywordCollection.Add("Place Model", PlaceModelCommand);
        keywordCollection.Add("Show Model", ShowModelCommand);
        keywordCollection.Add("Hide Model", HideModelCommand);
        keywordCollection.Add("Show Wall", ShowWallCommand);
        keywordCollection.Add("Hide Wall", HideWallCommand);
        keywordCollection.Add("Add Wall", AddWallCommand);

        /*keywords.Add("Reset World", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("OnReset");
        });

        keywords.Add("Drop Sphere", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                // Call the OnDrop method on just the focused object.
                focusObject.SendMessage("OnDrop");
            }
        });*/

        // Tell the KeywordRecognizer about our keywords.
        //keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        //keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        //keywordRecognizer.Start();
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void OnDestroy()
    {
        keywordRecognizer.Dispose();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }

    private void MoveCommand(PhraseRecognizedEventArgs args)
    {
        GestureManager.Instance.Transition(GestureManager.Instance.ManipulationRecognizer);
    }

    private void PlaceCommand(PhraseRecognizedEventArgs args)
    {
        //SpatialMapping.Instance.SendMessage("Place");
        //this.BroadcastMessage("Place");

        model.BroadcastMessage("Place");
        //model.BroadcastMessage("ResetResetPosition");
    }

    private void ResetCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("OnReset");
    }

    private void ExplodeCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("Explode");
    }

    private void ShowLabelCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("ShowLabel");
    }

    private void PlaceWorldCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("PlaceWorld");
    }

    private void ShowInfoCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("ShowInfo");
    }

    private void PlacePlanCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("PlacePlan");
    }

    private void ShowPlanCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("ShowPlan");
    }

    private void HidePlanCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("HidePlan");
    }

    private void PlaceModelCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("PlaceModel");
    }

    private void ShowModelCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("ShowModel");
    }

    private void HideModelCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("HideModel");
    }

    private void ShowWallCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("ShowWall");
    }

    private void HideWallCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("HideWall");
    }

    private void AddWallCommand(PhraseRecognizedEventArgs args)
    {
        model.BroadcastMessage("AddWall");
    }

    public void Update()
    {
        //if (isModelExpanding && Time.realtimeSinceStartup >= expandAnimationCompletionTime)
        //{
        //    isModelExpanding = false;

        //    Animator[] expandedAnimators = ExpandModel.Instance.ExpandedModel.GetComponentsInChildren<Animator>();

        //    foreach (Animator animator in expandedAnimators)
        //    {
        //        animator.enabled = false;
        //    }
        //}
    }
}
