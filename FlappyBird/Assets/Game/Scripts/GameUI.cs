using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
    public GameObject Logo;
    public GameObject Start;
    public GameObject Ladder;
    public GameObject Ready;
    public GameObject Tutorial;
    public GameObject Score;
    public GameObject Over;

    public void UpdateUI(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                Logo.SetActive(true);
                Start.SetActive(true);
                Ladder.SetActive(true);
                Ready.SetActive(false);
                Tutorial.SetActive(false);
                Score.SetActive(false);
                Over.SetActive(false);
                break;
            case GameState.Ready:
                Logo.SetActive(false);
                Start.SetActive(false);
                Ladder.SetActive(false);
                Ready.SetActive(true);
                Tutorial.SetActive(true);
                Score.SetActive(false);
                Over.SetActive(false);
                break;
            case GameState.Play:
                Logo.SetActive(false);
                Start.SetActive(false);
                Ladder.SetActive(false);
                Ready.SetActive(false);
                Tutorial.SetActive(false);
                Score.SetActive(true);
                Over.SetActive(false);
                break;
            case GameState.Over:
                Logo.SetActive(false);
                Start.SetActive(false);
                Ladder.SetActive(false);
                Ready.SetActive(false);
                Tutorial.SetActive(false);
                Score.SetActive(false);
                Over.SetActive(true);
                break;
            default: break;
        }
    }
}