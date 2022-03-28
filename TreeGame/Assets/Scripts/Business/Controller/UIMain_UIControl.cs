using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIMain_UIControl : UIController {

	private float countDown = 0;
	private int countDownSchedulerId = 0;

	private Text timeText;
	private Text scoreText;
	private Text recordText;

	public override void Awake() {
		base.Awake();

		this.countDown = 30.0f;

		this.timeText = this.view["timeText"].GetComponent<Text>();
		this.timeText.text = "Time: " + this.countDown.ToString();

		this.scoreText = this.view["scoreText"].GetComponent<Text>();
		this.scoreText.text = "Score: 0";
		
		this.recordText = this.view["recordText"].GetComponent<Text>();
		this.recordText.text = "Highest: 0";

		this.registButtonListener("returnBtn", this.onClickReturn);
	}

	void Start() {
		this.countDownSchedulerId = ScheduleManager.Instance.Schedule(this.updateTime, 1.0f, 0);
	}

    private void OnDestroy()
    {
		if (this.countDownSchedulerId != 0)
		{
			ScheduleManager.Instance.Unschedule(this.countDownSchedulerId);
			this.countDownSchedulerId = 0;
		}
	}

    private void onClickReturn()
    {
		UIManager.Instance.RemoveUIView("UIMain");
		UIManager.Instance.ShowUIView("UILogin");
	}

	public void onFinishGame()
    {
		UIManager.Instance.RemoveUIView("UIMain");
		UIManager.Instance.ShowUIView("UIResult");
	}

	public void updateTime(object param)
    {
		this.countDown--;
		if (this.countDown <= 0)
        {
			this.onFinishGame();
			return;
        }
        else
        {
			this.timeText.text = "Time: " + this.countDown.ToString();
        }
    }
}
