import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class MainPage{
	private JButton searchButton;
	private JButton searchRecordButton;
	private JTextField searchField;
	private MyListener listener;
	private PanelManager panelManager;
	
	public MainPage(JFrame frame) {
		searchField = new JTextField();   //검색어 입력창
		searchButton = new JButton();
		searchRecordButton = new JButton("검색 기록");
	}
	public void setComponents() {
		Font font = new Font("SansSerif", Font.BOLD, 20);
		//setLayout(null);

		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.white);
		
		searchField.setBounds(150, 270, 450, 50); //검색창
		searchField.setFont(font);
		
		searchButton.setBounds(600, 270, 50, 50); //검색버튼
		
		searchRecordButton.setBounds(620, 480, 130, 40);  //검색기록 버튼
		searchRecordButton.setFont(font);

		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		searchPanel.add(searchRecordButton);
		
		searchField.addActionListener(listener);
		searchButton.addActionListener(listener);
		searchRecordButton.addActionListener(listener);
	}
	
	private class MyListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent event) {
			
			if(event.getSource() == searchField || event.getSource() == searchButton) {   //검색어 필드 enter or 검색버튼 클릭
				panelManager.ChangeToSearchResult();
			}
			else {
				System.out.println("기록조회버튼"); 
			}
		}
	}
}
