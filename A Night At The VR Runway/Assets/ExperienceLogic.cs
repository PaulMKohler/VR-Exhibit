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
    public CanvasGroup fadeOutOnStart, fadeOutOnAttn, fadeOutInstructions1, fadeOutInstructions2;
    public GameObject destroyNotice, destroyAttn, destroyInstructions1, destroyInstructions2;
    public CanvasGroup fadeInOnStart, fadeInOnAttn, fadeInAfterInstructions1, fadeInAfterInstructions2;
    public GameObject exhibitAMonitor, exhibitBMonitor, exhibitCMonitor, exhibitDMonitor, exhibitEMonitor, exhibitFMonitor;
    public VideoPlayer exhibitAVideo, exhibitBVideo, exhibitCVideo, exhibitDVideo, exhibitEVideo, exhibitFVideo;
    public GvrAudioSource startMusic, designMusic, retailMusic, btcMusic, exhibitAMusic, exhibitBMusic, exhibitCMusic, exhibitDMusic, exhibitEMusic, exhibitFMusic;
    public AudioSource userSFX;
    public AudioClip walkleftSFX, walkrightSFX, selectSFX, backSFX;

    void Start()
    {
        // Update 'player' to be the camera's parent gameobject, i.e. GvrEditorEmulator, instead of the camera itself.
        // Required because GVR resets camera position to 0, 0, 0.
        player = player.transform.parent.gameObject;

        // Move player to the start position.
        player.transform.position = startPoint.transform.position;
    }

    public void NoticeAcknowledged(){
        userSFX.clip = selectSFX;
        userSFX.Play();
        StartCoroutine("NoticeAck");
    }

    public void AttentionAcknowledged(){
        userSFX.clip = selectSFX;
        userSFX.Play();
        StartCoroutine("AttnAck");        
    }

    public void Instr1Acknowledged(){
        userSFX.clip = selectSFX;
        userSFX.Play();
        StartCoroutine("Instr1Ack");
    }

    public void Instr2Acknowledged()
    {
        userSFX.clip = selectSFX;
        userSFX.Play();
        StartCoroutine("Instr2Ack");
    }

    public IEnumerator NoticeAck()
    {
        //Fade Out Notice, Fade In Start Menu
        yield return StartCoroutine(FadeCanvas(fadeOutOnStart, 1f, 0f, 2f));
        Destroy(destroyNotice);
        yield return StartCoroutine(FadeCanvas(fadeInOnStart, 0f, 1f, 1f));
    }

    public IEnumerator AttnAck()
    {
        //Fade Out Notice, Fade In Start Menu
        yield return StartCoroutine(FadeCanvas(fadeOutOnAttn, 1f, 0f, 2f));
        Destroy(destroyAttn);
        yield return StartCoroutine(FadeCanvas(fadeInOnAttn, 0f, 1f, 1f));
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
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(startUI, designUI);
        designMusic.Play();
        startMusic.Stop();
        iTween.MoveTo(player,
            iTween.Hash(
                "position", designPoint.transform.position,
                "time", 2,
                "easetype", "linear"
            )
        );
        StartCoroutine("playWalkSFX",2f);
    }
    public void ResetToStartFromDesign()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = startPoint.transform.position;
        designMusic.Stop();
        startMusic.Play();
        ToggleUI(designUI, startUI);
    }

    // Shift to the retail section
    public void StartRetail()
    {
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(startUI, retailUI);
        retailMusic.Play();
        startMusic.Stop();
        iTween.MoveTo(player,
            iTween.Hash(
                "position", retailPoint.transform.position,
                "time", 2,
                "easetype", "linear"
            )
        );
        StartCoroutine("playWalkSFX", 2f);
    }

    public void ResetToStartFromRetail()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = startPoint.transform.position;
        retailMusic.Stop();
        startMusic.Play();
        ToggleUI(retailUI, startUI);
    }

    //Shift to Exhibit A
    public void StartExhibitA(){
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(designUI, exhibitAUI);
        exhibitAMusic.Play();
        designMusic.Stop();
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint1.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
        StartCoroutine("playWalkSFX", 2f);
    }

    public void ResetToDesignFromExhibitA()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = designPoint.transform.position;
        exhibitAMusic.Stop();
        designMusic.Play();
        ToggleUI(exhibitAUI, designUI);
    }

    public void PlayVideoA(){
        ToggleUI(exhibitAUI, exhibitAMonitor);
        exhibitAMusic.Stop();
        exhibitAVideo.Play();
        exhibitAVideo.loopPointReached += EndVideoAReached;
    }

    public void StopVideoA(){
        exhibitAVideo.Stop();
        exhibitAMusic.Play();
        ToggleUI(exhibitAMonitor, exhibitAUI);
    }

    void EndVideoAReached(VideoPlayer vp){
        ToggleUI(exhibitAMonitor, exhibitAUI);
        exhibitAMusic.Play();

    }

    //Shift to Exhibit B
    public void StartExhibitB(){
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(designUI, exhibitBUI);
        exhibitBMusic.Play();
        designMusic.Stop();
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint2.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );  
        StartCoroutine("playWalkSFX", 2f);
    }

    public void ResetToDesignFromExhibitB()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = designPoint.transform.position;
        exhibitBMusic.Stop();
        designMusic.Play();
        ToggleUI(exhibitBUI, designUI);
    }

    public void PlayVideoB()
    {
        ToggleUI(exhibitBUI, exhibitBMonitor);
        exhibitBMusic.Stop();
        exhibitBVideo.Play();
        exhibitBVideo.loopPointReached += EndVideoBReached;
    }

    void EndVideoBReached(VideoPlayer vp)
    {
        exhibitBMusic.Play();
        ToggleUI(exhibitBMonitor, exhibitBUI);

    }

    public void StopVideoB()
    {
        exhibitBVideo.Stop();
        exhibitBMusic.Play();
        ToggleUI(exhibitBMonitor, exhibitBUI);
    }

    //Shift to Exhibit B
    public void StartExhibitC()
    {
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(btcUI, exhibitCUI);
        exhibitCMusic.Play();
        btcMusic.Stop();
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint3.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
        StartCoroutine("playWalkSFX", 2f);
    }

    public void ResetToBTCFromExhibitC()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = btcPoint.transform.position;
        exhibitCMusic.Stop();
        btcMusic.Play();
        ToggleUI(exhibitCUI, btcUI);
    }

    public void PlayVideoC()
    {
        ToggleUI(exhibitCUI, exhibitCMonitor);
        exhibitCMusic.Stop();
        exhibitCVideo.Play();
        exhibitCVideo.loopPointReached += EndVideoCReached;
    }
    public void StopVideoC()
    {
        exhibitCVideo.Stop();
        exhibitCMusic.Play();
        ToggleUI(exhibitCMonitor, exhibitCUI);
    }

    void EndVideoCReached(VideoPlayer vp)
    {
        ToggleUI(exhibitCMonitor, exhibitCUI);
        exhibitCMusic.Play();
    }

    //Shift to Exhibit B
    public void StartExhibitD()
    {
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(btcUI, exhibitDUI);
        exhibitDMusic.Play();
        btcMusic.Stop();
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint4.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
        StartCoroutine("playWalkSFX", 2f);
    }

    public void ResetToBTCFromExhibitD()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = btcPoint.transform.position;
        exhibitDMusic.Stop();
        btcMusic.Play();
        ToggleUI(exhibitDUI, btcUI);
    }

    public void PlayVideoD()
    {
        ToggleUI(exhibitDUI, exhibitDMonitor);
        exhibitDMusic.Stop();
        exhibitDVideo.Play();
        exhibitDVideo.loopPointReached += EndVideoDReached;
    }

    public void StopVideoD()
    {
        exhibitDVideo.Stop();
        exhibitDMusic.Play();
        ToggleUI(exhibitDMonitor, exhibitDUI);
    }

    void EndVideoDReached(VideoPlayer vp)
    {
        exhibitDMusic.Play();
        ToggleUI(exhibitDMonitor, exhibitDUI);
    }

    //Shift to Exhibit B
    public void StartExhibitE()
    {
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(retailUI, exhibitEUI);
        exhibitEMusic.Play();
        retailMusic.Stop();
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint5.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
        StartCoroutine("playWalkSFX", 2f);
    }

    public void ResetToRetailFromExhibitE()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = retailPoint.transform.position;
        exhibitEMusic.Stop();
        retailMusic.Play();
        ToggleUI(exhibitEUI, retailUI);
    }

    public void PlayVideoE()
    {
        ToggleUI(exhibitEUI, exhibitEMonitor);
        exhibitEMusic.Stop();
        exhibitEVideo.Play();
        exhibitEVideo.loopPointReached += EndVideoEReached;
    }

    public void StopVideoE()
    {
        exhibitEVideo.Stop();
        exhibitEMusic.Play();
        ToggleUI(exhibitEMonitor, exhibitEUI);
    }

    void EndVideoEReached(VideoPlayer vp)
    {
        exhibitEMusic.Play();
        ToggleUI(exhibitEMonitor, exhibitEUI);
    }

    //Shift to Exhibit B
    public void StartExhibitF()
    {
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(retailUI, exhibitFUI);
        exhibitFMusic.Play();
        retailMusic.Stop();
        iTween.MoveTo(player,
           iTween.Hash(
               "position", exhibitPoint6.transform.position,
               "time", 2,
               "easetype", "linear"
           )
       );
        StartCoroutine("playWalkSFX", 2f);
    }

    public void ResetToRetailFromExhibitF()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = retailPoint.transform.position;
        exhibitFMusic.Stop();
        retailMusic.Play();
        ToggleUI(exhibitFUI, retailUI);
    }

    public void PlayVideoF()
    {
        ToggleUI(exhibitFUI, exhibitFMonitor);
        exhibitFMusic.Stop();
        exhibitFVideo.Play();
        exhibitFVideo.loopPointReached += EndVideoFReached;
    }

    public void StopVideoF()
    {
        exhibitFVideo.Stop();
        exhibitFMusic.Play();
        ToggleUI(exhibitFMonitor, exhibitFUI);
    }

    void EndVideoFReached(VideoPlayer vp)
    {
        exhibitFMusic.Play();
        ToggleUI(exhibitFMonitor, exhibitFUI);
    }

    // Shift to the retail section
    public void StartBTC()
    {
        userSFX.clip = selectSFX;
        userSFX.Play();
        ToggleUI(startUI, btcUI);
        btcMusic.Play();
        startMusic.Stop();
        iTween.MoveTo(player,
            iTween.Hash(
                "position", btcPoint.transform.position,
                "time", 4,
                "easetype", "linear"
            )
        );
        StartCoroutine("playWalkSFX", 4f);
    }

    public void ResetToStartFromBTC()
    {
        userSFX.clip = backSFX;
        userSFX.Play();
        player.transform.position = startPoint.transform.position;
        btcMusic.Stop();
        startMusic.Play();
        ToggleUI(btcUI, startUI);
    }

    public IEnumerator playWalkSFX(float seconds){
        //Play left walk and right walk at .5 intervals until seconds is up
        while (seconds > 0f)
        {
            yield return StartCoroutine("WaitHalfSecond");
            seconds -= .5f;
            userSFX.clip = walkleftSFX;
            userSFX.Play();
            if (seconds > 0f)
            {
                yield return StartCoroutine("WaitHalfSecond");
                seconds -= .5f;
                userSFX.clip = walkrightSFX;
                userSFX.Play();
            }
        }
    }

    public IEnumerator WaitHalfSecond()
    {
        yield return new WaitForSeconds(.5f);
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
