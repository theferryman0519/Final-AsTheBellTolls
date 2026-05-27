// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Game Dependencies
using Atbt.Core;

namespace Atbt.Controller {
public class AudioController : Singleton<AudioController> {

#region -------------------- Serialized Variables --------------------

#endregion
#region -------------------- Public Variables --------------------

#endregion
#region -------------------- Private Variables --------------------

#endregion
#region -------------------- Initial Functions --------------------

#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    // Controls music playing and volume
    // Controls ambiance playing and volume
    // Controls the speech dialogue playing and volume
    // Controls sound effects playing and volume

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the audio controller");

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
