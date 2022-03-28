using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UILogin_UIControl : UIController
{

	public override void Awake()
	{
		base.Awake();

	}

	void Start()
	{
		this.registButtonListener("startBtn", this.onClickStart);
	}

	private void onClickStart()
	{
		UIManager.Instance.RemoveUIView("UILogin");
		UIManager.Instance.ShowUIView("UIMain");
	}

}
