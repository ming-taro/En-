package ui;

import java.awt.Graphics;
import java.awt.event.ActionListener;

public interface UICreator {
	public void setComponent();
	public void paintComponent(Graphics g);
	public void setActionListener(ActionListener actionListener);
}
