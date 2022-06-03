package ui;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
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
import ui.panel.MainPanel;
import ui.panel.SignUpCompletionPanel;
import ui.panel.SignUpPanel;
import ui.panel.UserModePanel;
import utility.Constants;

public class PanelSwitcher extends JFrame implements ActionListener{
	private ProfileDAO profileDAO;
	private PanelFactory panelFactory;
	private UICreator panelCurrentlyInUse;
	
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
		
		addPanel(Constants.MAIN_PANEL);  //frame 첫 실행시 메인화면으로 시작
	}
	
	private void switchPanel(JPanel panel) {
		getContentPane().removeAll();
		getContentPane().add(panel, BorderLayout.CENTER);
		getContentPane().revalidate();
		getContentPane().repaint();
	}
	
	private void addPanel(String panelName) {
		panelCurrentlyInUse = panelFactory.getPanel(panelName);
		panelCurrentlyInUse.setActionListener(this);
		
		getContentPane().removeAll();
		getContentPane().add((Component) panelCurrentlyInUse, BorderLayout.CENTER);
		getContentPane().revalidate();
		getContentPane().repaint();
	}

	private void setSignUp() {
		Profile profile = 
				((SignUpPanel) panelCurrentlyInUse).getProfile();   //회원가입 화면에서 입력한 회원정보를 가져옴
		String errorMessage = 
				((SignUpPanel) panelCurrentlyInUse).isProfileEnteredCorrectly(profile) ;   //가져온 데이터 중 비어있거나 양식에 맞지 않은 입력 검사
																			     //해당 항목에 대한 오류 메세지를 리턴받음
		if(errorMessage.equals("") == !Constants.IS_MATCH) {
			JOptionPane.showMessageDialog(null, errorMessage, 
					"입력 오류", JOptionPane.WARNING_MESSAGE);
			return;
		}
		
		try {
			profileDAO.AddToMemberList(profile);  //회원정보 저장
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		addPanel(Constants.SIGN_UP_COMPLETION_PANEL);
	}
	
	private void setLogIn() {
		String id = 
				((MainPanel) panelCurrentlyInUse).getIdTextField().getText();
		String password = 
				((MainPanel) panelCurrentlyInUse).getPaswwordTextField().getText();
		
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
			addPanel(Constants.USER_MODE_PANEL);
		}
		else {										   //입력한 회원정보가 존재하지 않음 -> 팝업 메세지
			JOptionPane.showMessageDialog(null, "아이디 또는 비밀번호를 잘못 입력했습니다.\n입력하신 내용을 다시 확인해주세요.",  
					"로그인 오류", JOptionPane.WARNING_MESSAGE);
		}
	}
	
	private void manageMainPanel(ActionEvent event) {
		if(event.getSource() == 
				((MainPanel) panelCurrentlyInUse).getSignUpButton()) {        //메인화면에서 회원가입버튼 클릭 -> 회원가입 창으로 이동
			addPanel(Constants.SIGN_UP_PANEL);
		}
		else {
			setLogIn();        //메인화면에서 로그인/비밀번호 필드에서 엔터 or 로그인 버튼 클릭
		} 
	}
	
	private void manageSignUpPanel(ActionEvent event) {
		if(event.getSource() == 
				((SignUpPanel) panelCurrentlyInUse).getBackButton()){    //회원가입화면에서 뒤로가기 버튼 클릭 -> 메인화면으로 이동 
			addPanel(Constants.MAIN_PANEL);
		}
		else { 
			setSignUp();  //회원가입화면에서 '회원가입'버튼클릭
		}
	}
	
	private void manageUserModePanel(ActionEvent event) {
		int logout = JOptionPane.showConfirmDialog(null, "로그아웃 하시겠습니까?", 
				"로그아웃", JOptionPane.YES_NO_OPTION, JOptionPane.QUESTION_MESSAGE);
		
		if(logout == Constants.YES_OPTION) {   //회원모드에서 로그아웃 클릭 -> 팝업창에서 yes or no 선택
			addPanel(Constants.MAIN_PANEL);
		}
		
	}
	
	@Override
	public void actionPerformed(ActionEvent event) {
		String className = panelCurrentlyInUse.getClass().getName();
		className = className.substring(className.lastIndexOf(".") + 1);   //class이름으로 현재 fram에 올려진 panel을 구별
		
		switch(className) {                       //추출한 클래스 이름으로 해당 클래스의 이벤트 실행(클래스가 null인지 체크하지 않아도 됨)
		
		case Constants.MAIN_PANEL:
			manageMainPanel(event);
			break;
		case Constants.SIGN_UP_PANEL:
			manageSignUpPanel(event);
			break;
		case Constants.SIGN_UP_COMPLETION_PANEL:  //회원가입 완료화면에서 로그인 버튼 클릭 -> 메인화면으로 이동
			addPanel(Constants.MAIN_PANEL);
			break;
		case Constants.USER_MODE_PANEL:           //회원모드에서 로그아웃 버튼 클릭 -> 메인화면으로
			manageUserModePanel(event);
			break;
		}
	}
	
}
