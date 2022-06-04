package ui;

import java.awt.event.ActionListener;

public interface UICreator {
	public void setComponent();
	public void setActionListener(ActionListener actionListener);  //메인 프레임인 PanelSwitcher에서 가져온 이벤트 리스너
}
