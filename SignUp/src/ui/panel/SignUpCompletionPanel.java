package ui.panel;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.GridLayout;
import java.awt.event.ActionListener;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JPanel;

import ui.UICreator;

public class SignUpCompletionPanel extends JPanel implements UICreator{
	private JButton loginButton;

	public SignUpCompletionPanel() {
		setComponent();
	}
	
	public JButton getLoginButton() {
		return loginButton;
	}
	
	public void setActionListener(ActionListener actionListener) {
		loginButton.addActionListener(actionListener);   //회원가입완료 후 로그인 버튼을 누르면 로그인화면으로 돌아가도록 연결
	}
	
	@Override
	public void setComponent() {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		Font font = new Font("돋움", Font.PLAIN, 20);
		JPanel centerPanel = new JPanel();
		JPanel southPanel = new JPanel();
		JPanel buttonPanel = new JPanel();
		
		setLayout(new BorderLayout());   //회원가입 완료 화면 레이아웃
		
		loginButton = new JButton("로그인");
		loginButton.setFont(font);
		loginButton.setPreferredSize(new Dimension(200, 70));
		
		centerPanel.setPreferredSize(new Dimension(width, 140));     //로그인 버튼이 밑으로 가도록 중간에 넣어줄 빈 패널
		centerPanel.setOpaque(false);
		
		buttonPanel.setLayout(new FlowLayout(FlowLayout.CENTER));    //로그인 버튼을 담을 패널(로그인 버튼의 크기 변경을 방지하기 위함)
		buttonPanel.setOpaque(false);
		buttonPanel.setPreferredSize(new Dimension(width, 70));
		buttonPanel.add(loginButton);

		southPanel.setLayout(new GridLayout(2, 0));                  //로그인버튼 밑에 버튼 크기만큼의 공간을 띄우기 위해 그리드로 설정 
		southPanel.setOpaque(false);
		southPanel.add(buttonPanel);
		
		add(centerPanel, BorderLayout.CENTER);
		add(southPanel, BorderLayout.SOUTH);
	}
	
	@Override
	public void paintComponent(Graphics g) {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		int height = panelSize.height;
		
		ImageIcon icon = new ImageIcon("image\\nightSky.png");
	    g.drawImage(icon.getImage(), 0, 0, width, height, null);
	    setOpaque(false);
	    super.paintComponent(g);
	}

}
