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

public class UserModePanel extends JPanel implements UICreator{
	private JButton editionButton;
	private JButton logoutButton;
	private JButton memberWithdrawalButton;
	
	public UserModePanel() {
		setLayout(new BorderLayout());
		setComponent();
	}
	
	public JButton getLogoutButton() {
		return logoutButton;
	}
	
	public JButton getEditionButton() {
		return editionButton;
	}
	
	private JPanel getJPanel(int width, int height) {
		JPanel panel = new JPanel();
		
		panel.setLayout(new FlowLayout(FlowLayout.CENTER));    //버튼을 담을 패널(버튼의 크기 변경을 방지하기 위함)
		panel.setOpaque(false);
		panel.setPreferredSize(new Dimension(width, height));
		
		return panel;
	}
	
	private JButton getJButton(String text) {
		JButton button = new JButton(text);
		Font font = new Font("돋움", Font.PLAIN, 20);
		
		button.setFont(font);
		button.setPreferredSize(new Dimension(200, 70));
		
		return button;
	}
	
	@Override
	public void setComponent() {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		JPanel buttonPanel;
		JPanel centerPanel = new JPanel();
		
		centerPanel.setLayout(new GridLayout(3, 0));
		centerPanel.setPreferredSize(new Dimension(width, 140));     //로그인 버튼이 밑으로 가도록 중간에 넣어줄 빈 패널
		centerPanel.setOpaque(false);
		
		editionButton = getJButton("정보 수정");
		buttonPanel = getJPanel(width, 70);
		buttonPanel.add(editionButton);
		centerPanel.add(buttonPanel);
		
		logoutButton = getJButton("로그아웃");
		buttonPanel = getJPanel(width, 70);
		buttonPanel.add(logoutButton);
		centerPanel.add(buttonPanel);
		
		memberWithdrawalButton = getJButton("회원탈퇴");
		buttonPanel = getJPanel(width, 70);
		buttonPanel.add(memberWithdrawalButton);
		centerPanel.add(buttonPanel);

		add(centerPanel, BorderLayout.CENTER);
		add(getJPanel(width, 250), BorderLayout.NORTH);
		add(getJPanel(width, 100), BorderLayout.SOUTH);
		
	}

	@Override
	public void setActionListener(ActionListener actionListener) {
		logoutButton.addActionListener(actionListener);
		editionButton.addActionListener(actionListener);
		memberWithdrawalButton.addActionListener(actionListener);
	}

	@Override
	public void paintComponent(Graphics g) {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		int height = panelSize.height;
		
		ImageIcon icon = new ImageIcon("image\\userMode.jpg");
	    g.drawImage(icon.getImage(), 0, 0, width, height, null);
	    setOpaque(false);
	    super.paintComponent(g);
	}
}
