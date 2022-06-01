package ui;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.Image;

import javax.swing.*;

import utility.Constants;

public class LogInPanel extends JPanel implements UICreator{
	public LogInPanel() {
		setComponent();
	}
	@Override
	public void setComponent() {
		JPanel northPanel = new JPanel();
		JPanel centerPanel = new JPanel();
		
		setLayout(new BorderLayout());
		
		northPanel.setLayout(new FlowLayout());
		setLogo(northPanel);
		
		centerPanel.setLayout(new FlowLayout());
		setLogIn(centerPanel);
	}
	public void setLogo(JPanel logoPanel) {
		ImageIcon icon = new ImageIcon("image\\logo.png");
		Image image = icon.getImage();
		Image changeImage = image.getScaledInstance(icon.getIconWidth()/4, icon.getIconHeight()/4, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);

		JLabel logoLabel = new JLabel(changeIcon);

		logoPanel.setBackground(new Color(255, 0, 0, 0));
		logoPanel.add(logoLabel, CENTER_ALIGNMENT);
		logoPanel.setPreferredSize(new Dimension(icon.getIconWidth()/4, icon.getIconHeight()/4));
		
		add(logoPanel, BorderLayout.NORTH);	
	}
	public void setLogIn(JPanel centerPanel) {
		JPanel inputPanel = new JPanel();
		JTextField idTextField = new JTextField();
		JTextField paswwordTextField = new JTextField();
		JButton logInButton = new JButton();
		JButton signUpButton = new JButton();
		
		inputPanel.setLayout(new GridLayout(4, 0, 0, 20));
		
		inputPanel.add(idTextField);
		inputPanel.add(paswwordTextField);
		inputPanel.add(logInButton);
		inputPanel.add(signUpButton);
		inputPanel.setPreferredSize(new Dimension(300, 300));
		inputPanel.setBackground(new Color(255, 0, 0, 0));
		
		centerPanel.setBackground(new Color(255, 0, 0, 0));
		centerPanel.add(inputPanel, CENTER_ALIGNMENT);
		
		add(centerPanel, BorderLayout.CENTER);	
	}
	public void paintComponent(Graphics g) {
		Dimension panelSize = getSize();
		
		ImageIcon icon = new ImageIcon("image\\StardueValley.png");
	    g.drawImage(icon.getImage(), 0, 0, panelSize.width, panelSize.height, null);
	    setOpaque(false);
	    super.paintComponent(g);
	}
}
