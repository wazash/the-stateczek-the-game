using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class OptionsView : BaseView
{
    [SerializeField] private StateMachine gameStateMachine;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button menuBtn;

    [SerializeField] private GameObject defaultSelectetGameObject;

    public override void ShowView()
    {
        base.ShowView();

        EventSystem.current.SetSelectedGameObject(defaultSelectetGameObject);

        if (gameStateMachine.CurrentState.Name == StatesNames.MenuStateName)
        {
            resumeBtn.gameObject.SetActive(false);
        }
        else
        {
            resumeBtn.gameObject.SetActive(true);
        }
    }

    public void OnResumeButtonPressed(bool gamePaused)
    {
        Time.timeScale = 1;

        GameEvents.GamePaused((GameState)gameStateMachine.CurrentState, gamePaused);
    }

    public void OnMainMenuButtonPressed()
    {
        if(gameStateMachine.CurrentState.Name == StatesNames.GameStateName)
        {
            gameStateMachine.EnterState(new MenuState());
        }
        else
        {
            UIManager.Instance.ShowMainMenu();
        }
    }
}
