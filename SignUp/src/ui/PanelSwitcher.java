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

import controller.PanelFactory;
import controller.ProfileDAO;
import model.Profile;
import utility.Constants;

public class PanelSwitcher extends JFrame implements ActionListener{
	private PanelFactory panelFactory;
	private MainPanel mainPanel;
	private SignUpPanel signUpPanel;
	private SignUpCompletionPanel signUpCompletionPanel;
	private ProfileDAO profileDAO;
	
	public PanelSwitcher() {
		panelFactory = new PanelFactory();
		
		profileDAO = ProfileDAO.GetInstance();
		profileDAO.connectionDB();
		
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
		
		mainPanel = (MainPanel) panelFactory.getPanel("MainPanel");
		mainPanel.setActionListener(this);
		switchPanel(mainPanel);
	}
	private void switchPanel(JPanel panel) {
		getContentPane().removeAll();
		getContentPane().add(panel, BorderLayout.CENTER);
		getContentPane().revalidate();
		getContentPane().repaint();
	}
	
	private void addSignUpPanel() {
		signUpPanel = (SignUpPanel) panelFactory.getPanel("SignUpPanel");
		signUpPanel.setActionListener(this);
		switchPanel(signUpPanel);
	}
	
	private void addMainPanel() {
		mainPanel = (MainPanel) panelFactory.getPanel("MainPanel");
		mainPanel.setActionListener(this);
		switchPanel(mainPanel);
	}
	
	private void AddsignUpCompletionPanel() {
		signUpCompletionPanel = (SignUpCompletionPanel) panelFactory.getPanel("SignUpCompletionPanel");
		signUpCompletionPanel.setActionListener(this);
		switchPanel(signUpCompletionPanel);
	}
	
	private void setSignUp() {
		Profile profile = signUpPanel.getProfile();   //회원가입 화면에서 입력한 회원정보를 가져옴
		String errorMessage = signUpPanel.isProfileEnteredCorrectly(profile) ;   //가져온 데이터 중 비어있거나 양식에 맞지 않은 입력 검사
																			     //해당 항목에 대한 오류 메세지를 리턴받음
		if(errorMessage.equals("") == !Constants.IS_MATCH) {
			JOptionPane.showMessageDialog(null, errorMessage + "\n양식에 맞게 정보를 다시 입력해주세요.",
					"입력 오류", JOptionPane.WARNING_MESSAGE);
			return;
		}
		
		try {
			profileDAO.AddToMemberList(profile);  //회원정보 저장
			System.out.println("성공");
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		AddsignUpCompletionPanel();
	}
	
	private void setLogIn() {
		String id = mainPanel.getIdTextField().getText();
		String password = mainPanel.getPaswwordTextField().getText();
		
		if(id.equals("")) {
			JOptionPane.showMessageDialog(null, "아이디를 입력해주세요",  //아이디를 입력하지 않은 경우 -> 팝업 메세지
					"입력 오류", JOptionPane.WARNING_MESSAGE);
			return;
		}
		else if(password.equals("")) {     
			JOptionPane.showMessageDialog(null, "비밀번호를 입력해주세요",  //비밀번호를 입력하지 않은 경우 -> 팝업 메세지
					"입력 오류", JOptionPane.WARNING_MESSAGE);
			return;
		}
		
		if(profileDAO.isMemberInList(id, password)) {  //입력한 회원정보가 존재하는 경우
			System.out.println("오예아");
		}
		else {										   //입력한 회원정보가 존재하지 않음 -> 팝업 메세지
			JOptionPane.showMessageDialog(null, "아이디 또는 비밀번호를 잘못 입력했습니다.\n입력하신 내용을 다시 확인해주세요.",  
					"로그인 오류", JOptionPane.WARNING_MESSAGE);
		}
		
	}
	
	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == mainPanel.getSignUpButton()) {        //메인화면에서 회원가입버튼 클릭 -> 회원가입 창으로 이동
			addSignUpPanel();
		}
		else if(event.getSource() == mainPanel.getIdTextField()       //메인화면에서 로그인/비밀번호 필드에서 엔터 or 로그인 버튼 클릭
				|| event.getSource() == mainPanel.getPaswwordTextField()
				|| event.getSource() == mainPanel.getLogInButton()){
			setLogIn();
		}
		else if(event.getSource() == signUpPanel.getBackButton()){    //회원가입화면에서 뒤로가기 버튼 클릭 -> 메인화면으로 이동 
			addMainPanel();
		}
		else if(event.getSource() == signUpCompletionPanel.getLogInButton()) {	//회원가입 완료화면에서 로그인 버튼 클릭 -> 메인화면으로 이동
			addMainPanel();
		}
		else if(event.getSource() == signUpPanel.getSignUpButton()) {//회원가입화면에서 '회원가입'버튼클릭
			setSignUp();
		}

		
	}
	
}
