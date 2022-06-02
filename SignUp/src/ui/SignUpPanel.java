package ui;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.ImageObserver;
import java.text.AttributedCharacterIterator;

import javax.swing.*;

public class SignUpPanel extends JPanel implements UICreator{
	private JButton backButton;
	private JTextField idTextField;
	private JTextField passwordTextField, confirmPasswordTextField;
	private JTextField nameTextField;
	private JTextField yearTextField, dayTextField;
	private JComboBox monthcomboBox, sexComboBox;
	
	private JTextField roadNameAddressTextField, detailAddressTextField;
	private JTextField phoneNumberTextField;
	private JTextField emailTextField;
	
	public SignUpPanel(ActionListener actionListener) {
		setComponent();
		
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
		
		add(backButtonPanel, BorderLayout.NORTH);
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
	
	private void setInputFiled(JPanel inputPanel, JTextField inputField, String imagePath, String regexText) {	
		Font font = new Font("고딕", Font.BOLD, 15);
		JLabel inputTypeLabel = new JLabel(getLabelIcon(imagePath));
		JLabel regexLabel;
		JPanel inputFieldPanel = getInputFieldPanel(inputTypeLabel);
		
		inputField = new JTextField();
		inputField.setFont(font);
		inputField.setPreferredSize(new Dimension(250, 40));
		inputFieldPanel.add(inputField);
		
		regexLabel = new JLabel(regexText);
		regexLabel.setFont(font);
		inputFieldPanel.add(regexLabel);
		
		inputPanel.add(inputFieldPanel);
	}
	
	private void setBirthFiled(JPanel inputPanel) {
		JLabel inputTypeLabel = new JLabel(getLabelIcon("image\\password.png"));
		JPanel inputFieldPanel = getInputFieldPanel(inputTypeLabel);
		Font font = new Font("고딕", Font.BOLD, 15);
		String[] month = {"월", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"};
		String[] sex = {"성별", "여", "남"};
		
		yearTextField = new JTextField();
		yearTextField.setFont(font);
		yearTextField.setPreferredSize(new Dimension(50, 40));
		
		monthcomboBox = new JComboBox(month);
		monthcomboBox.setFont(font);
		monthcomboBox.setPreferredSize(new Dimension(50, 40));
		
		dayTextField = new JTextField();
		dayTextField.setFont(font);
		dayTextField.setPreferredSize(new Dimension(50, 40));
		
		JLabel blankLabel = new JLabel("");
		blankLabel.setPreferredSize(new Dimension(50, 40));
		
		sexComboBox = new JComboBox(sex);
		sexComboBox.setFont(font);
		sexComboBox.setPreferredSize(new Dimension(70, 40));
		
		inputFieldPanel.add(yearTextField);
		inputFieldPanel.add(monthcomboBox);
		inputFieldPanel.add(dayTextField);
		inputFieldPanel.add(blankLabel);
		inputFieldPanel.add(sexComboBox);
	
		inputPanel.add(inputFieldPanel);
	}
	
	private void setProfileInputPanel() {
		JPanel inputPanel = new JPanel();
		inputPanel.setLayout(new GridLayout(9, 0, 0, 0));
		inputPanel.setBackground(new Color(255, 0, 0, 0));

		setInputFiled(inputPanel, idTextField, "image\\id.png", "(영문 대소문자/숫자, 5~20자)");
		setInputFiled(inputPanel, passwordTextField, "image\\password.png", "(영문 대소문자/숫자, 8~16자)");
		setInputFiled(inputPanel, confirmPasswordTextField, "image\\blank.png", "(비밀번호를 한번 더 입력해주세요)");
		setInputFiled(inputPanel, nameTextField, "image\\name.png", "(한글/영문 대소문자, 40자 이내)");
		setBirthFiled(inputPanel);
		setInputFiled(inputPanel, roadNameAddressTextField, "image\\password.png", "기본주소");
		setInputFiled(inputPanel, detailAddressTextField, "image\\password.png", "상세 주소");
		setInputFiled(inputPanel, phoneNumberTextField, "image\\password.png", "");
		setInputFiled(inputPanel, emailTextField, "image\\password.png", "");
		
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
}
