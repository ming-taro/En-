package ui;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Image;
import java.awt.event.ActionListener;
import java.awt.event.MouseListener;

import javax.swing.ImageIcon;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JTextField;

public class UIComponent {
	public ImageIcon getLabelIcon(String imagePath) {
		ImageIcon icon = new ImageIcon(imagePath);
		Image image = icon.getImage();
		int width = icon.getIconWidth()/13;
		int hegith = icon.getIconHeight()/13;
		
		Image changeImage = image.getScaledInstance(width, hegith, Image.SCALE_SMOOTH);
		ImageIcon changeIcon = new ImageIcon(changeImage);

		return changeIcon;
	}

	public JPanel getInputFieldPanel(JLabel inputTypeLabel) {
		JPanel inputFieldPanel = new JPanel();
		
		inputFieldPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
		inputFieldPanel.add(inputTypeLabel);
		inputFieldPanel.setBackground(new Color(255, 0, 0, 0));
		inputFieldPanel.setPreferredSize(new Dimension(100, 70));
		
		return inputFieldPanel;
	}
	
	public JTextField getInputField(int width) {
		JTextField textField;
		Font font = new Font("고딕", Font.BOLD, 15);
		
		textField = new JTextField();
		textField.setFont(font);
		textField.setPreferredSize(new Dimension(width, 40));
		
		return textField;
	}
	
	public JPasswordField getPasswordField(int width) {
		JPasswordField textField;
		Font font = new Font("고딕", Font.BOLD, 15);
		
		textField = new JPasswordField();
		textField.setFont(font);
		textField.setPreferredSize(new Dimension(width, 40));
		
		return textField;
	}
	
	public JComboBox getComboBox(String[] text, MouseListener mouseListener, ActionListener actionListener) {
		JComboBox comboBox;
		Font font = new Font("고딕", Font.BOLD, 15);
		
		comboBox = new JComboBox(text);
		comboBox.setFont(font);
		comboBox.setBackground(Color.white);
		comboBox.setPreferredSize(new Dimension(60, 40));
		comboBox.addMouseListener(mouseListener);
		comboBox.addActionListener(actionListener);
		
		return comboBox;
	}
	
}
