package ui;

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

public class SignUpCompletionPanel extends JPanel implements UICreator{
	private JButton loginButton;

	public SignUpCompletionPanel(ActionListener actionListener) {
		setComponent();
		
		loginButton.addActionListener(actionListener);
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
		
		centerPanel.setPreferredSize(new Dimension(width, 140));
		centerPanel.setOpaque(false);
		
		buttonPanel.setLayout(new FlowLayout(FlowLayout.CENTER));
		buttonPanel.setOpaque(false);
		buttonPanel.setPreferredSize(new Dimension(width, 70));
		buttonPanel.add(loginButton);

		southPanel.setLayout(new GridLayout(2, 0));
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
