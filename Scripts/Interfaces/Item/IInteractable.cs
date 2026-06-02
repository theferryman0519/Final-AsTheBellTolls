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
using Atbt.Controller;
using Atbt.Object;

namespace Atbt.Item {
public interface IInteractable {
    ToolObject RequiredTool { get; set; }

    void Interact(PlayerController player, ToolObject tool);
}}
