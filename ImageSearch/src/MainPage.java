import java.awt.*;
import javax.swing.*;

public class MainPage extends JFrame {
	public MainPage() {

		setTitle("Image Search");
		setBounds(450, 150, 800, 600);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		Font font = new Font("����", Font.BOLD, 20);
		
		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		add(searchPanel);
		
		JTextField searchField = new JTextField("�˻��� �Է�");   //�˻��� �Է�â
		searchField.setBounds(150, 270, 450, 50);
		searchField.setFont(font);
		
		JButton searchButton = new JButton();
		searchButton.setBounds(600, 270, 50, 50);
		
		JButton button = new JButton("�˻� ���");
		button.setBounds(620, 480, 130, 40);
		button.setFont(font);
		
		searchPanel.add(button);
		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		
		setVisible(true);
	}

}
