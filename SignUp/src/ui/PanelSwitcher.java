package ui;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JFrame;
import javax.swing.JPanel;

import utility.Constants;

public class PanelSwitcher extends JFrame implements ActionListener{
	private MainPanel mainPanel;
	private SignUpPanel signUpPanel;
	
	public PanelSwitcher() {
		mainPanel = new MainPanel(this);
		signUpPanel = new SignUpPanel(this);
		
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
		
		getContentPane().add(mainPanel, BorderLayout.CENTER);
	}
	public void switchPanel(JPanel panel) {
		getContentPane().removeAll();
		getContentPane().add(panel, BorderLayout.CENTER);
		getContentPane().revalidate();
		getContentPane().repaint();
	}
	
	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == mainPanel.getSignUpButton()) {
			switchPanel(signUpPanel);
		}
		else if(event.getSource() == signUpPanel.getBackButton()) {
			switchPanel(mainPanel);
		}
	}
	
}
