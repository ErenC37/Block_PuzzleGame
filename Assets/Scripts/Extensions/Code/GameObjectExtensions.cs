using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class GameObjectExtensions
{
    /* ------------------------------------------ */

    private static readonly CancellationTokenSource _cancellationTokenSource = new();

    /* ------------------------------------------ */

    public static async UniTask SetActive(this GameObject origin, bool active, float time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time), ignoreTimeScale: false,
            cancellationToken: _cancellationTokenSource.Token);

        if (origin)
            origin.SetActive(active);
    }

    /* ------------------------------------------ */
}