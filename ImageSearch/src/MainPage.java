import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class MainPage extends JFrame {
	private JButton searchButton;
	private JButton searchRecordButton;
	private JTextField searchField;
	private MyListener listener;
	
	public MainPage() {
		Font font = new Font("SansSerif", Font.BOLD, 20);
		listener = new MyListener();

		setTitle("Image Search");
		setBounds(450, 150, 800, 600);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.white);
		add(searchPanel);
		
		searchField = new JTextField();   //검색어 입력창
		searchField.setBounds(150, 270, 450, 50);
		searchField.setFont(font);
		
		searchButton = new JButton();
		searchButton.setBounds(600, 270, 50, 50);
		
		searchRecordButton = new JButton("검색 기록");
		searchRecordButton.setBounds(620, 480, 130, 40);
		searchRecordButton.setFont(font);
		
		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		searchPanel.add(searchRecordButton);

		searchField.addActionListener(listener);
		searchButton.addActionListener(listener);
		searchRecordButton.addActionListener(listener);
		
		setVisible(true);
	}
	
	private class MyListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent event) {
			
			if(event.getSource() == searchField || event.getSource() == searchButton) {   //검색어 필드 enter or 검색버튼 클릭
				
			}
			else {
				System.out.println("기록조회버튼"); 
			}
		}
	}
}
