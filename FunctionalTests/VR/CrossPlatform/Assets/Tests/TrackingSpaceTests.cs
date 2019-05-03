using System.Collections;
using UnityEngine.Experimental.XR;
using UnityEngine;
using UnityEngine.XR;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TrackingSpaceTests : XrFunctionalTestBase
{
    [UnityTest]
    public IEnumerator VerifyXRDevice_GetCurrentTrackingSpace()
    {
        yield return SkipFrame(2 * OneSecOfFramesWaitTime);
        var trackingSpace = XRDevice.GetTrackingSpaceType();

        Assert.IsNotNull(trackingSpace, "Tracking space is not reading correctly");
    }
}
