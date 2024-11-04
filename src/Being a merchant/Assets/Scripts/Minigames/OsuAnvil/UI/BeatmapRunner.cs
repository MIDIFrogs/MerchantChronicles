using SibGameJam.Minigames;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BeatmapRunner : MonoBehaviour
{
    [SerializeField] Transform circlesPanel;
    [SerializeField] ApproachCircle circlePrefab;

    private readonly LinkedPool<ApproachCircle> circlesPool;
    private Beatmap beatmap;
    private AudioSource player;
    private int health;

    private Coroutine gameCoroutine;
    private Coroutine beatCoroutine;

    public event System.EventHandler<bool> GameEnd;

    public BeatmapRunner()
    {
        circlesPool = new(CreateCircle);
    }

    public void StartGame(Beatmap beatmap, AudioSource gameSource)
    {
        this.beatmap = beatmap;
        player = gameSource;
        gameCoroutine = StartCoroutine(RunBeatmap());
        beatCoroutine = StartCoroutine(PlayBeat());
    }

    public void StopGame()
    {
        StopCoroutine(gameCoroutine);
        StopCoroutine(beatCoroutine);
    }

    public IEnumerator PlayBeat()
    {
        float currentTime = Time.fixedTime;
        for (int i = 0; i < beatmap.BeatTicks.Count; i++)
        {
            var nextTick = beatmap.BeatTicks[i];
            float nextTime = (float)nextTick.TickTime.TotalSeconds;
            float approachDelay = (nextTime - currentTime) - 1 / beatmap.ApproachRate;
            // TODO: initialize circle and start it.
            yield return new WaitForSeconds(approachDelay);
            var circle = circlesPool.Get();
            circle.Speed = beatmap.ApproachRate;
            currentTime = Time.fixedTime;
        }
    }

    public IEnumerator RunBeatmap()
    {
        yield return new WaitForSeconds(beatmap.AudioBegin.length);
        bool win = health > 0;
        AudioClip toPlay = win ? beatmap.AudioWin : beatmap.AudioLose;
        player.PlayOneShot(toPlay);
        yield return new WaitForSeconds(toPlay.length);
        GameEnd?.Invoke(this, win);
    }

    public ApproachCircle CreateCircle()
    {
        var circle = Instantiate(circlePrefab, circlesPanel);
        return circle;
    }
}
