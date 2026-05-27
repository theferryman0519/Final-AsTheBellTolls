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
public class ItemController : Singleton<ItemController> {

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
    // Controls central item lookup

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the item controller");

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
