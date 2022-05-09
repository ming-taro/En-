import java.awt.*;
import java.awt.event.*;
import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;

import javax.swing.*;

import org.json.simple.parser.ParseException;

public class SearchResult extends JPanel{
	private JButton searchButton;
	private JButton homeButton;
	private JTextField searchField;
	private JComboBox<String> numberBox;
	private JPanel resultPanel, imagePanel;
	private JButton[] imageButton;
	private MyListener listener;
	private PanelManager panelManager;
	
	public SearchResult(PanelManager panelManager) throws IOException, ParseException {
		listener = new MyListener();
		searchField = new JTextField();   //검색어 입력창
		searchButton = new JButton();
		homeButton = new JButton("H");
		numberBox = new JComboBox();
		imageButton = new JButton[30];
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
		searchPanel.setBackground(Color.white);
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
	
	public void addResultPanel() throws IOException, ParseException {
		resultPanel = new JPanel();
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
		
		imagePanel = new JPanel();
		imagePanel.setLayout(new GridLayout(6, 5, 5, 5));
		for(int i=0; i<30; i++) {
			imageButton[i] = new JButton();
			imagePanel.add(imageButton[i]);
		}
		imagePanel.setBounds(0, 50, 800, 480);
		resultPanel.add(imagePanel);
		
		numberBox.addActionListener(listener);
	}
	public void setResult(String searchWord) throws IOException, ParseException {
		Search search = new Search();
		search.searchImage(searchWord);  //이미지 검색
		ArrayList<String> urlList = search.getUrlList();   //검색결과 이미지 url 리스트
		
		for(int i=0; i<30; i++) {
			ImageIcon image = new ImageIcon(new URL(urlList.get(i)));
			imageButton[i].setIcon(image);
			imageButton[i].setVisible(true);
		}
		showResult(10);
	}
	public void showResult(int number) {
		for(int i=0; i<30; i++) {
			if(i<number) imageButton[i].setVisible(true);
			else imageButton[i].setVisible(false);
		}
	}
	private class MyListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent event) {
			
			if(event.getSource() == homeButton) {
				panelManager.ChangeToMainPage();
			}
			else if(event.getSource() == searchField || event.getSource() == searchButton) {   //검색어 필드 enter or 검색버튼 클릭
				 try {
					setResult(searchField.getText());
					numberBox.setSelectedIndex(0);
				} catch (IOException | ParseException e) {
					e.printStackTrace();
				}
			}
			else if(event.getSource() == numberBox) {
				showResult(Integer.parseInt(numberBox.getSelectedItem().toString()));
			}
			else {
				System.out.println("기록조회버튼"); 
			}
			
			searchField.setText("");
		}
	}
}
