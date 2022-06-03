package controller;

import java.awt.Color;
import java.util.regex.Pattern;

import javax.swing.JLabel;
import javax.swing.JTextField;
import utility.Constants;

public class InputExceptionHandling {
	private JLabel idRegexLabel;
	private JLabel passwordRegexLabel;
	private JLabel confirmPasswordTLabel;
	private JLabel nameRegexLabel;
	private JLabel emailRegexLabel;
	
	public InputExceptionHandling() {
		idRegexLabel = new JLabel("(영문 소문자/숫자, 5~20자)");
		idRegexLabel.setBackground(Color.white);
		passwordRegexLabel = new JLabel("(영문 대소문자/숫자, 8~16자)");
		confirmPasswordTLabel = new JLabel("(비밀번호를 한번 더 입력해주세요)");
		nameRegexLabel = new JLabel("(한글/영문 대소문자, 40자 이내)");
		emailRegexLabel = new JLabel("(이메일 주소를 다시 확인해주세요.)");
	}
	
	public JLabel getIdRegexLabel() {
		return idRegexLabel;
	}
	
	public JLabel getPasswordRegexLabel() {
		return passwordRegexLabel;
	}
	
	public JLabel getConfirmPasswordTLabel() {
		return confirmPasswordTLabel;
	}
	
	public JLabel getNameRegexLabel() {
		return nameRegexLabel;
	}
	
	public JLabel getEmailRegexLabel() {
		return emailRegexLabel;
	}
	
	public void setIdTextField(JTextField idTextField) {
		String id = idTextField.getText();
		
		if(id.equals("")) {
			return;
		}
		
		if(Pattern.matches(Constants.ID_REGEX, id)) {           //아이디 입력 양식에 맞음
			idRegexLabel.setText("(사용 가능한 아이디 입니다.)");
			idRegexLabel.setForeground(Color.green);
		}
		else {
			idRegexLabel.setText("(5~20자의 영문 소문자, 숫자만 입력 가능합니다.)"); //아이디 입력 양식과 다름
			idRegexLabel.setForeground(Color.red);
		}
	}
	
	public void setPasswordTextField(JTextField passwordTextField) {
		String password = passwordTextField.getText();
		
		if(password.equals("")) {
			return;
		}
		
		if(Pattern.matches(Constants.PASSWORD_REGEX, password)) {//비밀번호 입력 양식에 맞음
			passwordRegexLabel.setText("");
		}
		else {
			passwordRegexLabel.setText("(8~16자의 영문 대소문자, 숫자만 입력 가능합니다.)");//비밀번호 입력 양식과 다름
			passwordRegexLabel.setForeground(Color.red);
		}
	}
	
	public void setConfirmPasswordTextField(JTextField passwordTextField, JTextField confirmPasswordTextField) {
		String password = passwordTextField.getText();
		String confirmPassword = confirmPasswordTextField.getText();
		
		if(confirmPassword.equals("")) {             //비밀번호 재확인란을 입력하지 않은 경우
			confirmPasswordTLabel.setText("(필수 입력 항목입니다.)");
		}
		else if(confirmPassword.equals(password)) {  //비밀번호와 일치함
			confirmPasswordTLabel.setText("");
		}
		else {
			confirmPasswordTLabel.setText("(비밀번호가 일치하지 않습니다.)");//비밀번호와 일치하지 않음
			confirmPasswordTLabel.setForeground(Color.red);
		}
	}
	
	public void setNameTextField(JTextField nameTextField) {
		String name = nameTextField.getText();
		
		if(name.equals("")) {
			return;
		}
		
		if(Pattern.matches(Constants.NAME_REGEX, name)) {//이름 입력 양식에 맞음
			nameRegexLabel.setText("");
		}
		else {
			nameRegexLabel.setText("(40자 이내의 한글, 영문 대소문자만 입력 가능합니다.)");//비밀번호와 일치하지 않음
			nameRegexLabel.setForeground(Color.red);
		}
	}
	
	public void setEmailTextField(JTextField emailTextField) {
		String email = emailTextField.getText();
		
		if(email.equals("")) {
			return;
		}
	}
}
