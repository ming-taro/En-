package controller;

import ui.UICreator;
import ui.panel.EditionPanel;
import ui.panel.MainPanel;
import ui.panel.SignUpCompletionPanel;
import ui.panel.SignUpPanel;
import ui.panel.UserModePanel;
import utility.Constants;

public class PanelFactory {       //메인 frame에 넣을 패널 이름을 인자로 받아 해당 패널 객체를 생성해 반환 
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
			
		case Constants.EDITION_PANEL:
			return new EditionPanel();
		}
		
		return null;
	}
}
