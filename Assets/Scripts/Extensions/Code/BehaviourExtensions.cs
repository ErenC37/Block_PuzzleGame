using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class BehaviourExtensions
{
    /* ------------------------------------------ */

    private static readonly CancellationTokenSource _cancellationTokenSource = new();
    
    /* ------------------------------------------ */

    /// <summary>
    /// An extension method for making sure some UI elements fit the content size such as Layout groups with
    /// constantly updated elements like Text.
    /// </summary>
    public static async UniTask Refresh(this Behaviour origin, float time = .25f)
    {
        origin.enabled = false;

        await UniTask.Delay(TimeSpan.FromSeconds(time), ignoreTimeScale: false, cancellationToken: _cancellationTokenSource.Token);

        origin.enabled = true;
    }

    /* ------------------------------------------ */
}