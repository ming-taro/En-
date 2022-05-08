import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class SearchResult extends JFrame {
	private JButton searchButton;
	private JButton homeButton;
	private JTextField searchField;
	private MyListener listener;
	
	public SearchResult() {
		setTitle("Image Search");
		setBounds(450, 150, 800, 600);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		listener = new MyListener();
		addSearchPanel();
		setVisible(true);
	}
	
	public void addSearchPanel() {

		Font font = new Font("SansSerif", Font.BOLD, 20);

		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.white);
		add(searchPanel);
		
		searchField = new JTextField();   //검색어 입력창
		searchField.setBounds(70, 10, 650, 50);
		searchField.setFont(font);
		
		searchButton = new JButton();
		searchButton.setBounds(730, 10, 50, 50);
		
		homeButton = new JButton("H");
		homeButton.setBounds(10, 10, 50, 50);
		homeButton.setFont(font);
		
		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		searchPanel.add(homeButton);

		searchField.addActionListener(listener);
		searchButton.addActionListener(listener);
		homeButton.addActionListener(listener);
		
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
