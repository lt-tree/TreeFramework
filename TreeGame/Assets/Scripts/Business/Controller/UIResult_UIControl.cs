using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIResult_UIControl : UIController {

	public override void Awake() {
		base.Awake();

	}

	void Start() {
		this.registButtonListener("restartBtn", this.onClickRestart);
		this.registButtonListener("returnBtn", this.onClickReturn);
	}

	private void onClickRestart()
    {
		UIManager.Instance.RemoveUIView("UIResult");
		UIManager.Instance.ShowUIView("UIMain");
	}

	private void onClickReturn()
    {
		UIManager.Instance.RemoveUIView("UIResult");
		UIManager.Instance.ShowUIView("UILogin");
	}

}
