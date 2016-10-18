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
        keywordCollection.Add("Place", PlaceCommand);
        keywordCollection.Add("Reset World", ResetCommand);
        keywordCollection.Add("Explode", ExplodeCommand);

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
