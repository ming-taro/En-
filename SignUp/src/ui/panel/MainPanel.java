package ui.panel;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.*;

import controller.ProfileDAO;
import ui.UICreator;
import utility.Constants;

public class MainPanel extends JPanel implements UICreator {
	private JTextField idTextField;          //아이디 입력 필드
	private JPasswordField paswwordTextField;    //비밀번호 입력 필드
	private JButton loginButton;             //로그인 버튼
	private JButton signUpButton;            //회원가입버튼
	
	public MainPanel() {
		idTextField = new JTextField();            
		paswwordTextField = new JPasswordField();
		loginButton = new JButton("LogIn");
		signUpButton = new JButton("SignUp");
		
		setComponent();
	}
	
	public JButton getSignUpButton() {
		return signUpButton;
	}
	
	public JButton getLoginButton() {
		return loginButton;
	}
	
	public JTextField getIdTextField() {
		return idTextField;
	}
	
	public JTextField getPaswwordTextField() {
		return paswwordTextField;
	}
	
	public void resetPaswwordTextField() {
		paswwordTextField.setText("");
	}
	
	@Override
	public void setActionListener(ActionListener actionListener) {
		idTextField.addActionListener(actionListener);
		paswwordTextField.addActionListener(actionListener);
		loginButton.addActionListener(actionListener);
		signUpButton.addActionListener(actionListener);
	}
	
	@Override
	public void setComponent() {
		JPanel northPanel = new JPanel();
		JPanel centerPanel = new JPanel();
		
		setLayout(new BorderLayout());
		
		northPanel.setLayout(new FlowLayout());
		setLogoPanel(northPanel);
		
		centerPanel.setLayout(new FlowLayout());
		setLogInPanel(centerPanel);
	}
	
	private void setLogoPanel(JPanel northPanel) {
		ImageIcon icon = new ImageIcon("image\\logo.png");
		Image image = icon.getImage();
		int width = icon.getIconWidth()/4;
		int hegith = icon.getIconHeight()/4;
		
		Image changeImage = image.getScaledInstance(width, hegith, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);

		JLabel logoLabel = new JLabel(changeIcon);

		northPanel.setBackground(new Color(255, 0, 0, 0));
		northPanel.add(logoLabel, CENTER_ALIGNMENT);
		northPanel.setPreferredSize(new Dimension(width, hegith));
		
		add(northPanel, BorderLayout.NORTH);	
	}
	
	private void setLogInPanel(JPanel centerPanel) {
		JPanel loginPanel = new JPanel();
		Font font = new Font("돋움", Font.PLAIN, 20);
		
		loginPanel.setLayout(new GridLayout(4, 0, 0, 20));
		
		idTextField.setFont(font);        //아이디, 비밀번호 입력필드 크기 조절
 		paswwordTextField.setFont(font);
		
 		loginPanel.add(idTextField);    
 		loginPanel.add(paswwordTextField);
 		loginPanel.add(loginButton);
 		loginPanel.add(signUpButton);
		
 		loginPanel.setPreferredSize(new Dimension(250, 250));
 		loginPanel.setBackground(new Color(255, 0, 0, 0));
		
		centerPanel.setBackground(new Color(255, 0, 0, 0));
		centerPanel.add(loginPanel, CENTER_ALIGNMENT);
		
		add(centerPanel, BorderLayout.CENTER);
	}
	
	@Override
	public void paintComponent(Graphics g) {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		int height = panelSize.height;
		
		ImageIcon icon = new ImageIcon("image\\StardueValley.png");
	    g.drawImage(icon.getImage(), 0, 0, width, height, null);
	    setOpaque(false);
	    super.paintComponent(g);
	}
}
