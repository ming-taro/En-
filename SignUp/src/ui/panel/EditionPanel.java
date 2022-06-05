package ui.panel;

import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.FocusEvent;
import java.sql.SQLException;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JOptionPane;
import javax.swing.JPasswordField;
import javax.swing.JTextField;

import controller.ProfileDAO;
import model.Profile;
import ui.UICreator;
import utility.Constants;

public class EditionPanel extends SignUpPanel {
	private Profile profile;
	private ProfileDAO profileDAO;
	
	public EditionPanel() {
		profileDAO = ProfileDAO.GetInstance();
	}
	
	private String getPassword(String password) {
		StringBuilder passwordBuilder = new StringBuilder();
		
		for(int count = 0; count < password.length(); count++) {
			passwordBuilder.append("*");
		}
		
		return passwordBuilder.toString();
	}
	
	private String getFirstValue(String profile, String delimiter) { 
		int endIndex = profile.indexOf(delimiter);//ex: birth -> 2000.1.2 -> 출생 연도: 첫번째 '.' 이전까지의 값 = 2000
											      //ex: phoneNumber -> 010-1234-5678 -> 첫 번재 번호 = 010
		return profile.substring(0, endIndex);
	}
	
	private String getSecondValue(String profile, String delimiter) {
		int beginIndex = profile.indexOf(delimiter) + 1;  //ex: birth -> 2000.1.2 -> 출생 월: 첫번째 '.' ~ 마지막 '.' 이전까지의 값 = 1
		int endIndex = profile.lastIndexOf(delimiter);    //ex: phoneNumber -> 010-1234-5678 -> 두 번째 번호 = 1234
		
		return profile.substring(beginIndex, endIndex);
	}
	
	private String getThirdValue(String profile, String delimiter) {  
		int beginIndex = profile.lastIndexOf(delimiter) + 1;//ex: birth -> 2000.1.2 -> 출생 일: 마지막 '.'+ 1 ~ = 2
	      										    //ex: phoneNumber -> 010-1234-5678 -> 세 번째 번호 = 5678		
		return profile.substring(beginIndex);
	}
	
	public void setMemberInformation(String memberId) {    //DB에 저장되어 있는 사용자의 정보를 불러옴
		try {
			profile = profileDAO.getMemberProfile(memberId);
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		String password = getPassword(profile.getPassword());
		String birth = profile.getBirth();
		String phoneNumber = profile.getPhoneNumber();
		
		idTextField.setText(profile.getId());                                  //아이디
		passwordTextField.setText(password);                                   //비밀번호 -> "*"표시
		confirmPasswordTextField.setText(password);
		
		nameTextField.setText(profile.getName());                              //이름
		
		yearTextField.setText(getFirstValue(birth, "."));                      //출생일
		monthcomboBox.setSelectedItem(getSecondValue(birth, "."));
		dayTextField.setText(getThirdValue(birth, "."));

		sexComboBox.setSelectedItem(profile.getSex());                         //성별
		
		zipCodeTextField.setText(profile.getZipCode());                        //주소
		roadNameAddressTextField.setText(profile.getRoadNameAddress());
		detailAddressTextField.setText(profile.getDetailAddress());
		
		phoneNumberComboBox.setSelectedItem(getFirstValue(phoneNumber, "-"));  //휴대폰 번호
		middleNumberTextField.setText(getSecondValue(phoneNumber, "-"));;
		lastFourDigitsTextField.setText(getThirdValue(phoneNumber, "-"));
		
		emailTextField.setText(profile.getEmail());                            //이메일
	}
	
	public void setEditable() {
		idTextField.setEditable(false);
		
		nameTextField.setEditable(false);
		
		yearTextField.setEditable(false);
		monthcomboBox.setEnabled(false);
		dayTextField.setEditable(false);
		sexComboBox.setEnabled(false);
		
		signUpButton.setText("정보수정");
		signUpButton.addActionListener(this);
	}
	
	public void start(String memberId) {
		setMemberInformation(memberId);
		setEditable();
	}
	
	private void setEditionChanged() {
		String password = ((JTextField)passwordTextField).getText();
		String zipCode = zipCodeTextField.getText();
		String roadNameaddress = roadNameAddressTextField.getText();
		String detailAddress = detailAddressTextField.getText();
		String phoneNumber = 
				String.format("%s-%s-%s", phoneNumberComboBox.getSelectedItem().toString(),
				middleNumberTextField.getText(), lastFourDigitsTextField.getText());;
		String email = emailTextField.getText();;
		
		if(inputException.isPasswordEnteredCorrectly(password)
				&& inputException.isConfirmPasswordEnteredCorrectly()) {
			
			profile.setPassword(password);
		}
		
		if(inputException.isAddressEnteredCorrectly(roadNameaddress, detailAddress)) {
			
			profile.setZipCode(zipCode);
			profile.setRoadNameAddress(roadNameaddress);
			profile.setDetailAddress(detailAddress);
		}
		
		if(inputException.isPhoneNumberEnteredCorrectly(phoneNumber)) {
			profile.setPhoneNumber(phoneNumber);
		}
		
		if(inputException.isEmailEnteredCorrectly(email)) {
			profile.setEmail(email);
		}
	}
	
	private void manageEditionChange() {
		setEditionChanged();
		
		int memberWithdrawal = JOptionPane.showConfirmDialog(null, "입력하신 내용으로 회원정보를 수정하시겠습니까?", 
				"회원정보 수정", JOptionPane.YES_NO_OPTION, JOptionPane.QUESTION_MESSAGE);
		
		if(memberWithdrawal == Constants.YES_OPTION) {   //회원 수정 버튼 클릭 -> 팝업창에서 yes or no 선택
			JOptionPane.showMessageDialog(null, "회원정보가 수정되었습니다.\n"
					+ "변경된 정보를 확인하세요.", "회원정보 수정 완료", JOptionPane.INFORMATION_MESSAGE);
			profileDAO.updateMemberInformation(profile);
			setMemberInformation(profile.getId());  //회원정보창 업데이트
		}
	}
	
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == monthcomboBox            //comboBox클릭 후 배경이 지워지는 오류가 생겨서 추가한 코드
				|| event.getSource() == sexComboBox          //->이벤트 후 패널의 배경을 다시 그려줌
				|| event.getSource() == phoneNumberComboBox) {
				repaint();
				return;
			}
		
		JButton button = (JButton) event.getSource();
		
		if(button == signUpButton) {                    //정보 수정
			manageEditionChange();
		}
		else if(button == zipCodeButton) {              //우편번호 -> 주소검색 프레임이 열림
			searchFrame.setVisible();
		}
		else {
			setRoadAddress(button.getText());
			searchFrame.closeFrame();              //주소검색 후 -> 검색한 주소값 중 하나를 선택한 경우
		}
		
	}
	
	public void focusLost(FocusEvent event) {
		if(event.getSource() == passwordTextField) {      //비밀번호 입력 필드 검사
			inputException.setPasswordTextField(passwordTextField);
		}
		
		if(event.getSource() == confirmPasswordTextField) {//비밀번호 재입력 입력 필드 검사
			inputException.setConfirmPasswordTextField(
					passwordTextField, confirmPasswordTextField);
		}
		
		if(event.getSource() == emailTextField) {      //이메일 입력 필드 검사
			inputException.setEmailTextField(emailTextField);
		}
		
		repaint();
	}
}
