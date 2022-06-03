package controller;

import ui.MainPanel;
import ui.SignUpCompletionPanel;
import ui.SignUpPanel;
import ui.UICreator;

public class PanelFactory {
	public UICreator getPanel(String panelName) {
		switch(panelName) {
		case "SignUpPanel":
			return new SignUpPanel();
		case "MainPanel":
			return new MainPanel();
		case "SignUpCompletionPanel":
			return new SignUpCompletionPanel();
		}
		return null;
	}
}
