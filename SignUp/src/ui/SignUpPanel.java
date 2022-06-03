package ui;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

import controller.InputExceptionHandling;
import model.Profile;
import ui.search_box.SearchFrame;

public class SignUpPanel extends JPanel implements UICreator, ActionListener, MouseListener, FocusListener{
	private JButton backButton, signUpButton;
	private JTextField idTextField;
	private JPasswordField passwordTextField, confirmPasswordTextField;
	private JTextField nameTextField;
	private JTextField yearTextField, dayTextField;
	private JTextField zipCodeTextField;
	private JTextField roadNameAddressTextField, detailAddressTextField;
	private JTextField middleNumberTextField, lastFourDigitsTextField;
	private JTextField emailTextField;
	private JComboBox monthcomboBox, sexComboBox;
	private JComboBox phoneNumberComboBox;
	private JButton zipCodeButton;
	
	private SearchFrame searchFrame;
	private UIComponent uiComponent;
	private InputExceptionHandling inputException;
	
	public SignUpPanel(ActionListener actionListener) {
		searchFrame = new SearchFrame(this);
		uiComponent = new UIComponent();
		inputException = new InputExceptionHandling();
		
		setComponent();
		setFocusListener();
		
		backButton.addActionListener(actionListener);
		signUpButton.addActionListener(actionListener);
	}
	
	public JButton getBackButton() {
		return backButton;
	}
	
	public JButton getSignUpButton() {
		return signUpButton;
	}
	
	public Profile getProfile() {
		String id = idTextField.getText();
		String password = ((JTextField)passwordTextField).getText();
		String name = nameTextField.getText();
		String birth = String.format("%s.%s.%s",
				yearTextField.getText(), 
				monthcomboBox.getSelectedItem().toString(), 
				dayTextField.getText());
		String sex = sexComboBox.getSelectedItem().toString();
		String zipCode = zipCodeTextField.getText();
		String roadNameAddress = roadNameAddressTextField.getText();
		String detailAddress = detailAddressTextField.getText();
		String phoneNumber = 
				String.format("%s-%s-%s", phoneNumberComboBox.getSelectedItem().toString(),
				middleNumberTextField.getText(), lastFourDigitsTextField.getText());
		String email = emailTextField.getText();
		
		Profile profile =
				new Profile(id, password, name, birth, sex, zipCode,
						roadNameAddress, detailAddress, phoneNumber, email);
		
		return profile;
	}
	
	public String isProfileEnteredCorrectly(Profile profile) {
		return inputException.isProfileEnteredCorrectly(profile);
	}

	@Override
	public void setComponent() {
		setLayout(new BorderLayout());
		
		setButtonPanel();
		setProfileInputPanel();
	}
	
	private void setButtonPanel() {
		JPanel buttonPanel = new JPanel();
		
		buttonPanel.setLayout(new FlowLayout(FlowLayout.CENTER));
		
		backButton = new JButton("뒤로가기");
		backButton.setPreferredSize(new Dimension(100, 50));
		
		signUpButton = new JButton("회원가입");
		signUpButton.setPreferredSize(new Dimension(100, 50));
		
		buttonPanel.add(backButton);
		buttonPanel.add(signUpButton);
		buttonPanel.setPreferredSize(new Dimension(300, 70));
		buttonPanel.setBackground(new Color(255, 0, 0, 0));
		
		add(buttonPanel, BorderLayout.SOUTH);
	}
	
	
	private void setBirthFiled(JPanel inputPanel) {
		JLabel inputTypeLabel =
				new JLabel(uiComponent.getLabelIcon("image\\password.png"));
		JPanel inputFieldPanel = 
				uiComponent.getInputFieldPanel(inputTypeLabel);
		String[] month = {"월", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
		String[] sex = {"성별", "여", "남"};
		
		yearTextField = uiComponent.getInputField(60);
		
		monthcomboBox = uiComponent.getComboBox(month, this, this);
		
		dayTextField = uiComponent.getInputField(60);
		
		JLabel blankLabel = new JLabel("");
		blankLabel.setPreferredSize(new Dimension(50, 40));
		
		sexComboBox = uiComponent.getComboBox(sex, this, this);
		
		inputFieldPanel.add(yearTextField);
		inputFieldPanel.add(monthcomboBox);
		inputFieldPanel.add(dayTextField);
		inputFieldPanel.add(blankLabel);
		inputFieldPanel.add(sexComboBox);
	
		inputPanel.add(inputFieldPanel);
	}
	
	private void setZipCodeFiled(JPanel inputPanel) {
		JLabel inputTypeLabel = 
				new JLabel(uiComponent.getLabelIcon("image\\blank.png"));
		JPanel inputFieldPanel =
				uiComponent.getInputFieldPanel(inputTypeLabel);
	
		zipCodeTextField = uiComponent.getInputField(100);             //우편번호
		zipCodeTextField.setEditable(false);
		
		zipCodeButton = new JButton("우편번호");            //우편번호 찾기 버튼
		zipCodeButton.setPreferredSize(new Dimension(100, 40));
		zipCodeButton.addActionListener(this);
		
		inputFieldPanel.add(zipCodeTextField);
		inputFieldPanel.add(zipCodeButton);
		
		inputPanel.add(inputFieldPanel);
	}
	
	private void setAddressFiled(JPanel inputPanel) {
		JLabel inputTypeLabel =
				new JLabel(uiComponent.getLabelIcon("image\\blank.png"));
		JPanel inputFieldPanel = 
				uiComponent.getInputFieldPanel(inputTypeLabel);
	
		roadNameAddressTextField = uiComponent.getInputField(350);
		roadNameAddressTextField.setEditable(false);
		
		JLabel blankLabel = new JLabel("");
		blankLabel.setPreferredSize(new Dimension(20, 40));
		
		detailAddressTextField = uiComponent.getInputField(200);
		
		inputFieldPanel.add(roadNameAddressTextField);
		inputFieldPanel.add(blankLabel);
		inputFieldPanel.add(detailAddressTextField);
		
		inputPanel.add(inputFieldPanel);
	}
	
	private void setPhoneNumberField(JPanel inputPanel) {
		JLabel inputTypeLabel = 
				new JLabel(uiComponent.getLabelIcon("image\\blank.png"));
		JPanel inputFieldPanel = 
				uiComponent.getInputFieldPanel(inputTypeLabel);
		String[] phoneNumber = {"010", "011", "016", "017", "018", "019"};
		
		phoneNumberComboBox = uiComponent.getComboBox(phoneNumber, this, this);
		middleNumberTextField = uiComponent.getInputField(50);
		lastFourDigitsTextField = uiComponent.getInputField(50);
		
		inputFieldPanel.add(phoneNumberComboBox);
		inputFieldPanel.add(middleNumberTextField);
		inputFieldPanel.add(lastFourDigitsTextField);
		
		inputPanel.add(inputFieldPanel);
	}
	
	private void setInputFiled(JPanel inputPanel, JTextField inputField, String imagePath, JLabel regexLabel) {	
		Font font = new Font("고딕", Font.BOLD, 17);
		JLabel inputTypeLabel = 
				new JLabel(uiComponent.getLabelIcon(imagePath));
		JPanel inputFieldPanel = 
				uiComponent.getInputFieldPanel(inputTypeLabel);
		
		inputFieldPanel.add(inputField);    //입력필드
		
		regexLabel.setFont(font);        
		inputFieldPanel.add(regexLabel);    //입력양식 안내 문구
		
		inputPanel.add(inputFieldPanel);
	}
	
	
	private void setProfileInputPanel() {
		JPanel inputPanel = new JPanel();
		inputPanel.setLayout(new GridLayout(9, 0, 0, 0));
		inputPanel.setBackground(new Color(255, 0, 0, 0));
		
		idTextField = uiComponent.getInputField(250);
		setInputFiled(inputPanel, idTextField, 
				"image\\id.png", inputException.getIdRegexLabel());

		passwordTextField = uiComponent.getPasswordField(250);
		setInputFiled(inputPanel, passwordTextField, 
				"image\\password.png", inputException.getPasswordRegexLabel());
		
		confirmPasswordTextField = uiComponent.getPasswordField(250);
		setInputFiled(inputPanel, confirmPasswordTextField, 
				"image\\blank.png", inputException.getConfirmPasswordTLabel());
		
		nameTextField = uiComponent.getInputField(250);
		setInputFiled(inputPanel, nameTextField, 
				"image\\name.png", inputException.getNameRegexLabel());
		
		setBirthFiled(inputPanel);
		
		setZipCodeFiled(inputPanel);
		
		setAddressFiled(inputPanel);
		
		setPhoneNumberField(inputPanel);

		emailTextField = uiComponent.getInputField(250);
		setInputFiled(inputPanel, emailTextField, 
				"image\\password.png", inputException.getEmailRegexLabel());
		
		add(inputPanel, BorderLayout.CENTER);
	}
	
	@Override
	public void paintComponent(Graphics g) {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		int height = panelSize.height;
		
		ImageIcon icon = new ImageIcon("image\\sky.jpg");
	    g.drawImage(icon.getImage(), 0, 0, width, height, null);
	    setOpaque(false);
	    super.paintComponent(g);
	}

	public void setRoadAddress(String roadAddress) {
		int beginIndex = roadAddress.indexOf("[") + 1;   //우편번호 정보가 '[]'로 묶여있음
		int endIndex =  roadAddress.indexOf("]");
		
		String zipCode = roadAddress.substring(beginIndex, endIndex);
		roadAddress = roadAddress.substring(endIndex + 	1);   //우편정보 다음에 이어지는 정보 -> 도로명 주소
		
		zipCodeTextField.setText(zipCode);
		roadNameAddressTextField.setText(roadAddress);
	}
	
	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == monthcomboBox            //comboBox클릭 후 배경이 지워지는 오류가 생겨서 추가한 코드
			|| event.getSource() == sexComboBox          //->이벤트 후 패널의 배경을 다시 그려줌
			|| event.getSource() == phoneNumberComboBox) {
			repaint();
			return;
		}
		
		JButton button = (JButton) event.getSource();
		
		if(button == zipCodeButton) {              //우편번호 -> 주소검색 프레임이 열림
			searchFrame.setVisible();
		}
		else {
			setRoadAddress(button.getText());
			searchFrame.closeFrame();              //주소검색 후 -> 검색한 주소값 중 하나를 선택한 경우
		}
		
	}

	@Override
	public void mouseClicked(MouseEvent e) {
		repaint();
	}

	@Override
	public void mousePressed(MouseEvent e) {
		repaint();
	}

	@Override
	public void mouseReleased(MouseEvent e) {
		repaint();
	}

	@Override
	public void mouseEntered(MouseEvent e) {
		repaint();
	}

	@Override
	public void mouseExited(MouseEvent e) {
		repaint();
	}
	
	private void setFocusListener() {
		idTextField.addFocusListener(this);
		passwordTextField.addFocusListener(this);
		confirmPasswordTextField.addFocusListener(this);
		nameTextField.addFocusListener(this);
		emailTextField.addFocusListener(this);
	}
	
	@Override
	public void focusGained(FocusEvent event) {
		repaint();
	}

	@Override
	public void focusLost(FocusEvent event) {
		if(event.getSource() == idTextField) {            //아이디 입력 필드 검사
			inputException.setIdTextField(idTextField);
		}
		
		if(event.getSource() == passwordTextField) {      //비밀번호 입력 필드 검사
			inputException.setPasswordTextField(passwordTextField);
		}
		
		if(event.getSource() == confirmPasswordTextField) {//비밀번호 재입력 입력 필드 검사
			inputException.setConfirmPasswordTextField(
					passwordTextField, confirmPasswordTextField);
		}
		
		if(event.getSource() == nameTextField) {      //비밀번호 입력 필드 검사
			inputException.setNameTextField(nameTextField);
		}
		
		if(event.getSource() == emailTextField) {      //비밀번호 입력 필드 검사
			inputException.setEmailTextField(emailTextField);
		}
		
		repaint();
	}
}
