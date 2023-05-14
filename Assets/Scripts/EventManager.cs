using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void UpdateOptionsAction();
    public static event UpdateOptionsAction OnUpdatedOptions;

    public static void TriggerUpdatedOptions() {
        if(OnUpdatedOptions != null) {
            OnUpdatedOptions();
        }
    }
}
