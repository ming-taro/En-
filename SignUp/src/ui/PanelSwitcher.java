package ui;

import java.awt.BorderLayout;
import java.awt.Image;
import java.awt.Toolkit;

import javax.swing.JFrame;

import utility.Constants;

public class PanelSwitcher extends JFrame {
	private LogInPanel logInPanel;
	
	public PanelSwitcher() {
		logInPanel = new LogInPanel();
		
		setComponent();
	}
	public void setComponent() {
		setSize(Constants.WIDTH, Constants.HEIGHT);
		setTitle("StardewValley");
		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setLayout(new BorderLayout());
		setLocationRelativeTo(null);   //윈도우 가운데에 frame 띄우기
		Toolkit kit = Toolkit.getDefaultToolkit();
		Image img = kit.getImage("image\\StardueValley_icon.png");  //frame 아이콘
		setIconImage(img);
		setVisible(true);
		
		getContentPane().add(logInPanel, BorderLayout.CENTER);
	}
	
}
