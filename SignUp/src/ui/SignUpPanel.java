package ui;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.FocusEvent;
import java.awt.event.FocusListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.image.ImageObserver;
import java.text.AttributedCharacterIterator;

import javax.swing.*;

import controller.InputExceptionHandling;
import ui.search_box.SearchFrame;

public class SignUpPanel extends JPanel implements UICreator, ActionListener, MouseListener, FocusListener{
	private JButton backButton;
	private JTextField idTextField;
	private JTextField passwordTextField, confirmPasswordTextField;
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
	private InputExceptionHandling inputException = new InputExceptionHandling();
	
	public SignUpPanel(ActionListener actionListener) {
		searchFrame = new SearchFrame(this);
		
		setComponent();
		setFocusListener();
		
		backButton.addActionListener(actionListener);
	}
	
	public JButton getBackButton() {
		return backButton;
	}

	@Override
	public void setComponent() {
		setLayout(new BorderLayout());
		
		setBackButtonPanel();
		setProfileInputPanel();
	}
	
	private void setBackButtonPanel() {
		JPanel backButtonPanel = new JPanel();
		
		backButtonPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
		
		backButton = new JButton();
		backButton.setPreferredSize(new Dimension(50, 50));
		
		backButtonPanel.add(backButton);
		backButtonPanel.setPreferredSize(new Dimension(50, 70));
		backButtonPanel.setBackground(new Color(255, 0, 0, 0));
		
		add(backButtonPanel, BorderLayout.SOUTH);
	}
	
	private ImageIcon getLabelIcon(String imagePath) {
		ImageIcon icon = new ImageIcon(imagePath);
		Image image = icon.getImage();
		int width = icon.getIconWidth()/13;
		int hegith = icon.getIconHeight()/13;
		
		Image changeImage = image.getScaledInstance(width, hegith, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);

		return changeIcon;
	}

	private JPanel getInputFieldPanel(JLabel inputTypeLabel) {
		JPanel inputFieldPanel = new JPanel();
		
		inputFieldPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
		inputFieldPanel.add(inputTypeLabel);
		inputFieldPanel.setBackground(new Color(255, 0, 0, 0));
		inputFieldPanel.setPreferredSize(new Dimension(100, 70));
		
		return inputFieldPanel;
	}
	
	private JTextField getInputField(int width) {
		JTextField textField;
		Font font = new Font("고딕", Font.BOLD, 15);
		
		textField = new JTextField();
		textField.setFont(font);
		textField.setPreferredSize(new Dimension(width, 40));
		
		return textField;
	}
	
	private JComboBox getComboBox(String[] text) {
		JComboBox comboBox;
		Font font = new Font("고딕", Font.BOLD, 15);
		
		comboBox = new JComboBox(text);
		comboBox.setFont(font);
		comboBox.setBackground(Color.white);
		comboBox.setPreferredSize(new Dimension(60, 40));
		comboBox.addMouseListener(this);
		comboBox.addActionListener(this);
		
		return comboBox;
	}
	
	private void setBirthFiled(JPanel inputPanel) {
		JLabel inputTypeLabel = new JLabel(getLabelIcon("image\\password.png"));
		JPanel inputFieldPanel = getInputFieldPanel(inputTypeLabel);
		String[] month = {"월", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
		String[] sex = {"성별", "여", "남"};
		
		yearTextField = getInputField(60);
		
		monthcomboBox = getComboBox(month);
		
		dayTextField = getInputField(60);
		
		JLabel blankLabel = new JLabel("");
		blankLabel.setPreferredSize(new Dimension(50, 40));
		
		sexComboBox = getComboBox(sex);
		
		inputFieldPanel.add(yearTextField);
		inputFieldPanel.add(monthcomboBox);
		inputFieldPanel.add(dayTextField);
		inputFieldPanel.add(blankLabel);
		inputFieldPanel.add(sexComboBox);
	
		inputPanel.add(inputFieldPanel);
	}
	
	private void setZipCodeFiled(JPanel inputPanel) {
		JLabel inputTypeLabel = new JLabel(getLabelIcon("image\\blank.png"));
		JPanel inputFieldPanel = getInputFieldPanel(inputTypeLabel);
	
		zipCodeTextField = getInputField(100);             //우편번호
		zipCodeTextField.setEditable(false);
		
		zipCodeButton = new JButton("우편번호");            //우편번호 찾기 버튼
		zipCodeButton.setPreferredSize(new Dimension(100, 40));
		zipCodeButton.addActionListener(this);
		
		inputFieldPanel.add(zipCodeTextField);
		inputFieldPanel.add(zipCodeButton);
		
		inputPanel.add(inputFieldPanel);
	}
	
	private void setAddressFiled(JPanel inputPanel) {
		JLabel inputTypeLabel = new JLabel(getLabelIcon("image\\blank.png"));
		JPanel inputFieldPanel = getInputFieldPanel(inputTypeLabel);
	
		roadNameAddressTextField = getInputField(350);
		roadNameAddressTextField.setEditable(false);
		
		JLabel blankLabel = new JLabel("");
		blankLabel.setPreferredSize(new Dimension(20, 40));
		
		detailAddressTextField = getInputField(200);
		
		inputFieldPanel.add(roadNameAddressTextField);
		inputFieldPanel.add(blankLabel);
		inputFieldPanel.add(detailAddressTextField);
		
		inputPanel.add(inputFieldPanel);
	}
	
	private void setPhoneNumberField(JPanel inputPanel) {
		JLabel inputTypeLabel = new JLabel(getLabelIcon("image\\blank.png"));
		JPanel inputFieldPanel = getInputFieldPanel(inputTypeLabel);
		String[] phoneNumber = {"010", "011", "016", "017", "018", "019"};
		
		phoneNumberComboBox = getComboBox(phoneNumber);
		middleNumberTextField = getInputField(50);
		lastFourDigitsTextField = getInputField(50);
		
		inputFieldPanel.add(phoneNumberComboBox);
		inputFieldPanel.add(middleNumberTextField);
		inputFieldPanel.add(lastFourDigitsTextField);
		
		inputPanel.add(inputFieldPanel);
	}
	
	private void setInputFiled(JPanel inputPanel, JTextField inputField, String imagePath, JLabel regexLabel) {	
		Font font = new Font("고딕", Font.BOLD, 17);
		JLabel inputTypeLabel = new JLabel(getLabelIcon(imagePath));
		JPanel inputFieldPanel = getInputFieldPanel(inputTypeLabel);
		
		inputFieldPanel.add(inputField);    //입력필드
		
		regexLabel.setFont(font);        
		inputFieldPanel.add(regexLabel);    //입력양식 안내 문구
		
		inputPanel.add(inputFieldPanel);
	}
	
	
	private void setProfileInputPanel() {
		JPanel inputPanel = new JPanel();
		inputPanel.setLayout(new GridLayout(9, 0, 0, 0));
		inputPanel.setBackground(new Color(255, 0, 0, 0));
		
		idTextField = getInputField(250);
		setInputFiled(inputPanel, idTextField, 
				"image\\id.png", inputException.getIdRegexLabel());

		passwordTextField = getInputField(250);
		setInputFiled(inputPanel, passwordTextField, 
				"image\\password.png", inputException.getPasswordRegexLabel());
		
		confirmPasswordTextField = getInputField(250);
		setInputFiled(inputPanel, confirmPasswordTextField, 
				"image\\blank.png", inputException.getConfirmPasswordTLabel());
		
		nameTextField = getInputField(250);
		setInputFiled(inputPanel, nameTextField, 
				"image\\name.png", inputException.getNameRegexLabel());
		
		setBirthFiled(inputPanel);
		
		setZipCodeFiled(inputPanel);
		
		setAddressFiled(inputPanel);
		
		setPhoneNumberField(inputPanel);

		emailTextField = getInputField(250);
		setInputFiled(inputPanel, emailTextField, 
				"image\\password.png", inputException.getEmailRegexLabel());
		
		add(inputPanel, BorderLayout.CENTER);
	}
	
	@Override
	public void paintComponent(Graphics g) {
		Dimension panelSize = getSize();
		int width = panelSize.width;
		int height = panelSize.height;
		
		ImageIcon icon = new ImageIcon("image\\farm.jpg");
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
		JButton button = (JButton) event.getSource();
		
		if(button == zipCodeButton) {              //우편번호 -> 주소검색 프레임이 열림
			searchFrame.setVisible();
		}
		else {
			setRoadAddress(button.getText());
			searchFrame.closeFrame();              //주소검색 후 -> 검색한 주소값 중 하나를 선택한 경우
		}
		
		repaint();
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
