using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ExperienceLogic : MonoBehaviour
{
    public GameObject player;
    public GameObject startUI, designUI, retailUI,  btcUI, exhibitAUI, exhibitBUI, exhibitCUI, exhibitDUI, exhibitEUI, exhibitFUI;
    public GameObject startPoint, designPoint, retailPoint, btcPoint, exhibitPoint1, exhibitPoint2, exhibitPoint3, exhibitPoint4, exhibitPoint5, exhibitPoint6;
    public CanvasGroup fadeOutOnStart, fadeOutInstructions1, fadeOutInstructions2;
    public GameObject destroyNotice, destroyInstructions1, destroyInstructions2;
    public CanvasGroup fadeInOnStart, fadeInAfterInstructions1, fadeInAfterInstructions2;
    public GameObject exhibitAMonitor, exhibitBMonitor, exhibitCMonitor, exhibitDMonitor, exhibitEMonitor, exhibitFMonitor;
    public VideoPlayer exhibitAVideo, exhibitBVideo, exhibitCVideo, exhibitDVideo, exhibitEVideo, exhibitFVideo;

    void Start()
    {
        // Update 'player' to be the camera's parent gameobject, i.e. GvrEditorEmulator, instead of the camera itself.
        // Required because GVR resets camera position to 0, 0, 0.
        player = player.transform.parent.gameObject;

        // Move player to the start position.
        player.transform.position = startPoint.transform.position;
    }

    public void NoticeAcknowledged(){
        StartCoroutine("NoticeAck");
    }

    public void Instr1Acknowledged(){
        StartCoroutine("Instr1Ack");
    }

    public void Instr2Acknowledged()
    {
        StartCoroutine("Instr2Ack");
    }

    public IEnumerator NoticeAck()
    {
        //Fade Out Notice, Fade In Start Menu
        yield return StartCoroutine(FadeCanvas(fadeOutOnStart, 1f, 0f, 2f));
        Destroy(destroyNotice);
        yield return StartCoroutine(FadeCanvas(fadeInOnStart, 0f, 1f, 1f));
    }

    public IEnumerator Instr1Ack(){
        //Fade Out Notice, Fade In Start Menu
        yield return StartCoroutine(FadeCanvas(fadeOutInstructions1, 1f, 0f, 2f));
        Destroy(destroyInstructions1);
        yield return StartCoroutine(FadeCanvas(fadeInAfterInstructions1, 0f, 1f, 1f));        
    }

    public IEnumerator Instr2Ack()
    {
        //Fade Out Notice, Fade In Start Menu
        yield return StartCoroutine(FadeCanvas(fadeOutInstructions2, 1f, 0f, 2f));
        Destroy(destroyInstructions2);
        yield return StartCoroutine(FadeCanvas(fadeInAfterInstructions2, 0f, 1f, 1f));
    }

    public static IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float endAlpha, float duration)
    {
        // keep track of when the fading started, when it should finish, and how long it has been running&lt;/p&gt; &lt;p&gt;&a
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;

        // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
        canvas.alpha = startAlpha;
        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if (startAlpha > endAlpha) // if we are fading out/down 
            {
                canvas.alpha = startAlpha - percentage; // calculate the new alpha
            }
            else // if we are fading in/up
            {
                canvas.alpha = startAlpha + percentage; // calculate the new alpha
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        canvas.alpha = endAlpha; // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
    }


    // Shift to the design section
    public void StartDesign()
    {
        ToggleUI(startUI, designUI);
        iTween.MoveTo(player,
            iTween.Hash(
                "position", designPoint.transform.position,
                "time", 2,
                "easetype", "linear"
            )
        );
    }

    // Shift to the retail section
    public void StartRetail()
    {
        ToggleUI(startUI, retailUI);
        iTween.MoveTo(player,
            iTween.Hash(
                "position", retailPoint.transform.position,
                "time", 2,
                "easetype", "linear"
            )
        );
    }

    //Shift to Exhibit A
    public void StartExhibitA(){
        ToggleUI(designUI, exhibitAUI);
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint1.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );       
    }

    public void PlayVideoA(){
        ToggleUI(exhibitAUI, exhibitAMonitor);
        exhibitAVideo.Play();
        exhibitAVideo.loopPointReached += EndVideoAReached;
    }

    public void StopVideoA(){
        exhibitAVideo.Stop();
        ToggleUI(exhibitAMonitor, exhibitAUI);
    }

    void EndVideoAReached(VideoPlayer vp){
        ToggleUI(exhibitAMonitor, exhibitAUI);

    }

    //Shift to Exhibit B
    public void StartExhibitB(){
        ToggleUI(designUI, exhibitBUI);
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint2.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );       
    }

    public void PlayVideoB()
    {
        ToggleUI(exhibitBUI, exhibitBMonitor);
        exhibitBVideo.Play();
        exhibitBVideo.loopPointReached += EndVideoBReached;
    }

    void EndVideoBReached(VideoPlayer vp)
    {
        ToggleUI(exhibitBMonitor, exhibitBUI);

    }

    public void StopVideoB()
    {
        exhibitBVideo.Stop();
        ToggleUI(exhibitBMonitor, exhibitBUI);
    }

    //Shift to Exhibit B
    public void StartExhibitC()
    {
        ToggleUI(btcUI, exhibitCUI);
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint3.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
    }

    public void PlayVideoC()
    {
        ToggleUI(exhibitCUI, exhibitCMonitor);
        exhibitCVideo.Play();
        exhibitCVideo.loopPointReached += EndVideoCReached;
    }
    public void StopVideoC()
    {
        exhibitCVideo.Stop();
        ToggleUI(exhibitCMonitor, exhibitCUI);
    }

    void EndVideoCReached(VideoPlayer vp)
    {
        ToggleUI(exhibitCMonitor, exhibitCUI);
    }

    //Shift to Exhibit B
    public void StartExhibitD()
    {
        ToggleUI(btcUI, exhibitDUI);
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint4.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
    }

    public void PlayVideoD()
    {
        ToggleUI(exhibitDUI, exhibitDMonitor);
        exhibitDVideo.Play();
        exhibitDVideo.loopPointReached += EndVideoDReached;
    }

    public void StopVideoD()
    {
        exhibitDVideo.Stop();
        ToggleUI(exhibitDMonitor, exhibitDUI);
    }

    void EndVideoDReached(VideoPlayer vp)
    {
        ToggleUI(exhibitDMonitor, exhibitDUI);
    }

    //Shift to Exhibit B
    public void StartExhibitE()
    {
        ToggleUI(retailUI, exhibitEUI);
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint5.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
    }

    public void PlayVideoE()
    {
        ToggleUI(exhibitEUI, exhibitEMonitor);
        exhibitEVideo.Play();
        exhibitEVideo.loopPointReached += EndVideoEReached;
    }

    public void StopVideoE()
    {
        exhibitEVideo.Stop();
        ToggleUI(exhibitEMonitor, exhibitEUI);
    }

    void EndVideoEReached(VideoPlayer vp)
    {
        ToggleUI(exhibitEMonitor, exhibitEUI);
    }

    //Shift to Exhibit B
    public void StartExhibitF()
    {
        ToggleUI(retailUI, exhibitFUI);
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint6.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
    }

    public void PlayVideoF()
    {
        ToggleUI(exhibitFUI, exhibitFMonitor);
        exhibitFVideo.Play();
        exhibitFVideo.loopPointReached += EndVideoFReached;
    }

    public void StopVideoF()
    {
        exhibitFVideo.Stop();
        ToggleUI(exhibitFMonitor, exhibitFUI);
    }

    void EndVideoFReached(VideoPlayer vp)
    {
        ToggleUI(exhibitFMonitor, exhibitFUI);
    }

    // Shift to the retail section
    public void StartBTC()
    {
        ToggleUI(startUI, btcUI);
        iTween.MoveTo(player,
            iTween.Hash(
                "position", btcPoint.transform.position,
                "time", 4,
                "easetype", "linear"
            )
        );
    }

    public void ResetToStartFromDesign(){
        player.transform.position = startPoint.transform.position;
        ToggleUI(designUI,startUI);
    }

    public void ResetToStartFromBTC()
    {
        player.transform.position = startPoint.transform.position;
        ToggleUI(btcUI, startUI);
    }

    public void ResetToStartFromRetail()
    {
        player.transform.position = startPoint.transform.position;
        ToggleUI(retailUI, startUI);
    }
    // Reset the puzzle sequence.
    public void ResetPuzzle()
    {
        //player.transform.position = startPoint.transform.position;
        //ToggleUI();
    }


    public void ToggleUI(GameObject toggleOff, GameObject toggleOn)
    {
        toggleOff.SetActive(!toggleOff.activeSelf);
        toggleOn.SetActive(!toggleOn.activeSelf);
    }

    // Placeholder method to prevent compiler errors caused by this method being called from LightUp.cs.
    public void PlayerSelection(GameObject sphere)
    {
        // Will be completed later in the course.
    }
}
