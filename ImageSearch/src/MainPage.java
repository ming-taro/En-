import java.awt.*;
import javax.swing.*;

public class MainPage extends JFrame {
	public MainPage() {

		setTitle("Image Search");
		setBounds(450, 150, 800, 600);
		setVisible(true);
		
		JPanel panel = new JPanel();
		panel.setLayout(null);
		add(panel);
		
		JButton button = new JButton("�˻� ���");
		button.setBounds(620, 500, 130, 50);
		button.setFont(new Font("����", Font.BOLD, 20));
		panel.add(button);
		
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}

}
