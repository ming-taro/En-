package ui;

import java.awt.*;
import java.awt.event.ActionListener;

import javax.swing.*;

public class SignUpPanel extends JPanel implements UICreator{
	private JButton backButton;
	private JTextField idTextField, passwordTextField;
	
	public SignUpPanel(ActionListener actionListener) {
		setComponent();
		
		backButton.addActionListener(actionListener);
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
		backButtonPanel.setPreferredSize(new Dimension(50, 100));
		
		add(backButtonPanel, BorderLayout.NORTH);
	}
	
	private ImageIcon getLabelIcon(String imagePath) {
		ImageIcon icon = new ImageIcon(imagePath);
		Image image = icon.getImage();
		int width = icon.getIconWidth()/14;
		int hegith = icon.getIconHeight()/14;
		
		Image changeImage = image.getScaledInstance(width, hegith, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);

		return changeIcon;
	}

	private void setProfileInputPanel() {
		JPanel inputPanel = new JPanel();
		
		JLabel idLabel = new JLabel(getLabelIcon("image\\id.png"));
		JLabel password = new JLabel("password");
		JLabel reconfirmPasswod = new JLabel("re");
		JLabel farmName = new JLabel("farmname");
		JLabel name = new JLabel("name");
		JLabel birth = new JLabel("birth"); 
		JLabel sex = new JLabel("sec");
		JLabel address = new JLabel("address");
		JLabel phone = new JLabel("phone");
		JLabel email = new JLabel("email");
		
		inputPanel.setLayout(new GridLayout(10, 0, 0, 0));

		JPanel idPanel = new JPanel();
		idPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
		idPanel.add(idLabel);
		idPanel.setBackground(new Color(255, 0, 0, 0));
		idPanel.setPreferredSize(new Dimension(100, 70));
		idTextField = new JTextField();
		idTextField.setPreferredSize(new Dimension(200, 40));
		idPanel.add(idTextField);
		
		inputPanel.add(idPanel);
		inputPanel.add(password);
		inputPanel.add(reconfirmPasswod);
		inputPanel.add(farmName);
		inputPanel.add(name);
		inputPanel.add(birth);
		inputPanel.add(sex);
		inputPanel.add(address);
		inputPanel.add(phone);
		inputPanel.add(email);
		
		add(inputPanel, BorderLayout.CENTER);
	}
	
	@Override
	public void paintComponent(Graphics g) {
		// TODO Auto-generated method stub
		
	}
}
