using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformReplacer : PlatformPool
{
    private void Update()
    {
        TryReplacePlatform();
    }

    private void TryReplacePlatform()
    {
        GameObject platformUnderObserve = Platforms.Peek();

        if (CameraViewObserver.IsPositionLowerDisablePointZ(platformUnderObserve.transform.position.z))
        {
            platformUnderObserve.transform.position = Platforms.LastOrDefault().transform.position + StepPlatformPosition;
            platformUnderObserve.GetComponent<ObstaclePlacements>().RandomizeObstaclesActivity();
            Platforms.Enqueue(Platforms.Dequeue());
        }
    }
}
