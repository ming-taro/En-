import java.awt.*;
import java.awt.event.*;
import java.io.IOException;
import java.net.URL;
import java.sql.SQLException;
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
	private ImageFrame imageFrame;
	private SearchRecordDAO searchRecordDAO;
	
	public SearchResult(SearchRecordDAO searchRecordDAO, PanelManager panelManager) throws IOException, ParseException {
		listener = new MyListener();
		searchField = new JTextField();   //검색어 입력창
		searchButton = new JButton();     //검색버튼
		homeButton = new JButton("뒤로가기");    //뒤로가기
		numberBox = new JComboBox();      //이미지 개수
		imageButton = new JButton[30];    //이미지 버튼
		imageFrame = new ImageFrame();    
		this.searchRecordDAO = searchRecordDAO;
		this.panelManager = panelManager;

		setLayout(null);
		setSize(800, 600);
		addSearchPanel();  
		addResultPanel();  
	}
	public void addSearchPanel() {

		Font font = new Font("SansSerif", Font.BOLD, 20);
		JPanel searchPanel = new JPanel();
		
		searchPanel.setLayout(null);
		searchPanel.setBackground(Color.white);
		searchPanel.setBounds(0,0,800,70);
		
		searchField.setBounds(150, 10, 570, 50);
		searchField.setFont(font);
		
		searchButton.setBounds(730, 10, 50, 50);
		
		homeButton.setBounds(10, 10, 130, 50);
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
		imagePanel.setLayout(new GridLayout(6, 5, 5, 5));  //사진 출력 패널
		
		for(int i=0; i<30; i++) {
			imageButton[i] = new JButton();
			imagePanel.add(imageButton[i]);
			imageButton[i].addActionListener(listener);    //사진을 버튼이미지로 담음
		}
		imagePanel.setBounds(0, 50, 800, 480);
		resultPanel.add(imagePanel);
		
		numberBox.addActionListener(listener);
		
	}
	public void setResult(String searchWord) throws IOException, ParseException, SQLException {
		Search search = new Search();
		search.searchImage(searchWord);  //검색어 입력 결과
		
		ArrayList<String> urlList = search.getUrlList();   //검색어 입력 결과 url 리스트
		
		for(int i=0; i<30; i++) {
			ImageIcon image = new ImageIcon(new URL(urlList.get(i))); //버튼에 이미지를 넣음
			imageButton[i].setIcon(image);
			imageButton[i].setVisible(true);
		}
		showResult(10);  //이미지 개수 기본값 10개
		searchRecordDAO.AddSearchRecord(searchWord);
	}
	public void showResult(int number) {  //콤보박스에서 선택한 숫자만큼 이미지를 보여줌
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
			else if((event.getSource() == searchField || event.getSource() == searchButton) && searchField.getText().equals("") == false) {   //enter or 검색버튼
				 try {
					setResult(searchField.getText());
					numberBox.setSelectedIndex(0);
				} catch (IOException | ParseException e) {
					e.printStackTrace();
				} catch (SQLException e) {
					e.printStackTrace();
				}
			}
			else if(event.getSource() == numberBox) {
				showResult(Integer.parseInt(numberBox.getSelectedItem().toString()));
			}
			else {
				//imageFrame.showImage();
			}
			
			searchField.setText("");//검색 후 입력값 지우기
		}
	}
}
