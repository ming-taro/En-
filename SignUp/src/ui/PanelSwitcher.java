package ui;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.SQLException;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JPanel;

import controller.ProfileDAO;
import model.Profile;
import utility.Constants;

public class PanelSwitcher extends JFrame implements ActionListener{
	private MainPanel mainPanel;
	private SignUpPanel signUpPanel;
	private SignUpCompletionPanel signUpCompletionPanel;
	private ProfileDAO profileDAO = ProfileDAO.GetInstance();
	
	public PanelSwitcher() {
		profileDAO.connectionDB();
		mainPanel = new MainPanel(this);
		signUpPanel = new SignUpPanel(this);
		signUpCompletionPanel = new SignUpCompletionPanel(this);
		
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
	
	private void manageSignUp() {
		Profile profile = signUpPanel.getProfile();
		String errorMessage = signUpPanel.isProfileEnteredCorrectly(profile) ;
		
		if(errorMessage.equals("") == !Constants.IS_MATCH) {
			JOptionPane.showMessageDialog(null, errorMessage + "\n양식에 맞게 정보를 다시 입력해주세요.",
					"입력 오류", JOptionPane.WARNING_MESSAGE);
			return;
		}
		
		try {
			profileDAO.connectionDB();
			profileDAO.AddToMemberList(profile);  //회원정보 저장
			System.out.println("성공");
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		switchPanel(signUpCompletionPanel);
	}
	
	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == mainPanel.getSignUpButton()) {  //메인화면에서 회원가입버튼 클릭 -> 회원가입 창으로 이동
			switchPanel(signUpPanel);
		}
		
		if(event.getSource() == signUpPanel.getBackButton()     //회원가입화면에서 뒤로가기 버튼 클릭 -> 메인화면으로 이동 
				|| event.getSource() == signUpCompletionPanel.getLogInButton()) {  //회원가입완료화면에서 로그인 버튼 클릭 -> 메인화면으로 이동
			switchPanel(mainPanel);
		}
		
		if(event.getSource() == signUpPanel.getSignUpButton()) {//회원가입화면에서 '회원가입'버튼클릭
			manageSignUp();
		}
	}
	
}
