import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class SearchResult extends JPanel{
	private JButton searchButton;
	private JButton homeButton;
	private JTextField searchField;
	private JComboBox<String> numberBox;
	private MyListener listener;
	private PanelManager panelManager;
	
	public SearchResult(PanelManager panelManager) {
		listener = new MyListener();
		searchField = new JTextField();   //검색어 입력창
		searchButton = new JButton();
		homeButton = new JButton("H");
		numberBox = new JComboBox();
		this.panelManager = panelManager;

		setLayout(null);
		setSize(800, 600);
		addSearchPanel();   //검색창
		addResultPanel();  
	}
	public void addSearchPanel() {

		Font font = new Font("SansSerif", Font.BOLD, 20);

		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.yellow);
		searchPanel.setBounds(0,0,800,70);
		
		searchField.setBounds(70, 10, 660, 50);
		searchField.setFont(font);
		
		searchButton.setBounds(730, 10, 50, 50);
		
		homeButton.setBounds(10, 10, 50, 50);
		homeButton.setFont(font);
		
		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		searchPanel.add(homeButton);

		searchField.addActionListener(listener);
		searchButton.addActionListener(listener);
		homeButton.addActionListener(listener);

		add(searchPanel);
	}
	
	public void addResultPanel() {
		JPanel resultPanel = new JPanel();
		resultPanel.setLayout(null);
		resultPanel.setBackground(Color.white);
		resultPanel.setBounds(0,70,800,530);
		add(resultPanel);
		
		Font font = new Font("SansSerif", Font.BOLD, 20);

		JLabel listLabel = new JLabel("이미지");
		listLabel.setFont(font);
		listLabel.setBounds(10, 10, 150, 30);
		resultPanel.add(listLabel);
		
		numberBox.setBounds(730, 10, 50, 30);
		numberBox.addItem("10");
		numberBox.addItem("20");
		numberBox.addItem("30");
		resultPanel.add(numberBox);
		
		JScrollPane imageScrollPane = new JScrollPane();
		imageScrollPane.setBounds(0, 50, 800, 480);
		resultPanel.add(imageScrollPane);
		
		numberBox.addActionListener(listener);
	}
	
	private class MyListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent event) {
			
			if(event.getSource() == homeButton) {
				panelManager.ChangeToMainPage();
			}
			else if(event.getSource() == searchField || event.getSource() == searchButton) {   //검색어 필드 enter or 검색버튼 클릭
				
			}
			else if(event.getSource() == numberBox) {
				System.out.println("콤보"); 
				
			}
			else {
				System.out.println("기록조회버튼"); 
			}
		}
	}
}
