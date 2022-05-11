import java.awt.*;
import java.awt.event.*;
import java.io.IOException;
import java.sql.SQLException;

import javax.swing.*;

import org.json.simple.parser.ParseException;

public class MainPage extends JPanel{
	private JButton searchButton;
	private JButton searchRecordButton;
	private JTextField searchField;
	private MyListener listener;
	private PanelManager panelManager;
	private JButton logo;
	
	public MainPage(PanelManager panelManager) {
		searchField = new JTextField("");   //입력값 초기화
		searchButton = new JButton();
		searchRecordButton = new JButton("검색기록");
		listener = new MyListener();
		this.panelManager = panelManager;
		
		setLayout(null);
		setSize(800, 600);
		setComponents();
	}
	public void setComponents() {
		Font font = new Font("SansSerif", Font.BOLD, 20);

		JPanel searchPanel = new JPanel();
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.white);
		searchPanel.setBounds(0, 0, 800, 600);
		
		searchField.setBounds(150, 270, 450, 50); //검색어 입력
		searchField.setFont(font);
		
		searchButton.setBounds(600, 270, 50, 50); //검색버튼
		ImageIcon imageIcon = new ImageIcon("image\\magnifyingGlass.png"); //버튼에 이미지를 넣음
		Image image = imageIcon.getImage();
		imageIcon = new ImageIcon(image.getScaledInstance(50, 50, Image.SCALE_SMOOTH));
		searchButton.setIcon(imageIcon);
		searchButton.setBorderPainted(false);
		searchButton.setFocusPainted(false);
		searchButton.setContentAreaFilled(false);
		
		searchRecordButton.setBounds(620, 480, 130, 40);  //검색기록버튼
		searchRecordButton.setFont(font);

		logo = new JButton();
		logo.setBounds(250, 100, 300,200);
		imageIcon = new ImageIcon("image\\daum.jpg"); //버튼에 이미지를 넣음
		image = imageIcon.getImage();
		imageIcon = new ImageIcon(image.getScaledInstance(300, 100, Image.SCALE_SMOOTH));
		logo.setIcon(imageIcon);
		logo.setBorderPainted(false);
		logo.setFocusPainted(false);
		logo.setContentAreaFilled(false);
		
		add(searchPanel);
		searchPanel.add(searchField);
		searchPanel.add(searchButton);
		searchPanel.add(searchRecordButton);
		searchPanel.add(logo);
		
		searchField.addActionListener(listener);
		searchButton.addActionListener(listener);
		searchRecordButton.addActionListener(listener);
	}
	
	private class MyListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent event){
			if((event.getSource() == searchField || event.getSource() == searchButton) && searchField.getText().equals("")) {
				
			}
			else if(event.getSource() == searchField || event.getSource() == searchButton) {   //검색어 입력 후 enter or 검색버튼 클릭
				System.out.print(searchField.getText());
				try {
					panelManager.ChangeToSearchResult(searchField.getText());  //검색결과창
				} catch (IOException | ParseException e) {
					e.printStackTrace();
				} catch (SQLException e) {
					e.printStackTrace();
				}
			}
			else {
				try {
					panelManager.ChangeToSearchRecord();
				} catch (SQLException e) {
					e.printStackTrace();
				}
			}
			searchField.setText("");
		}
	}
}
