package controller;

import ui.UICreator;
import ui.panel.MainPanel;
import ui.panel.SignUpCompletionPanel;
import ui.panel.SignUpPanel;
import ui.panel.UserModePanel;
import utility.Constants;

public class PanelFactory {
	public UICreator getPanel(String panelName) {
		switch(panelName) {
		
		case Constants.SIGN_UP_PANEL:
			return new SignUpPanel();
			
		case Constants.MAIN_PANEL:
			return new MainPanel();
			
		case Constants.SIGN_UP_COMPLETION_PANEL:
			return new SignUpCompletionPanel();
			
		case Constants.USER_MODE_PANEL:
			return new UserModePanel();
		}
		
		return null;
	}
}
