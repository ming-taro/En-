import java.awt.Color;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.SQLException;
import java.util.ArrayList;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;

public class SearchRecord extends JPanel implements ActionListener{
	private JButton homeButton;
	private JButton resetButton;
	private JPanel searchRecordPanel;
	private JTable searchRecordTable;
	private JScrollPane searchRecordTableScroll;
	private ArrayList<SearchRecordDTO> searchRecordList;
	private PanelManager panelManager;
	private SearchRecordDAO searchRecordDAO;
	
	public SearchRecord(SearchRecordDAO searchRecordDAO, PanelManager panelManager) throws SQLException {
		homeButton = new JButton();
		resetButton = new JButton("초기화");
		searchRecordPanel = new JPanel();
		searchRecordList = new ArrayList<SearchRecordDTO>();
		this.searchRecordDAO = searchRecordDAO;
		this.panelManager = panelManager;
		
		homeButton.setBounds(10, 10, 50, 50);
		resetButton.setBounds(350, 450, 100, 50);
		searchRecordPanel.setBounds(150, 80, 500, 350);
		
		//searchRecordPanel.setLayout(null);
		
		setLayout(null);
		setBounds(0, 0, 800, 600);
		setBackground(Color.white);
		
		add(homeButton);
		add(resetButton);
		add(searchRecordPanel);
		
		homeButton.addActionListener(this);
		resetButton.addActionListener(this);
		setSearchRecord();
	}
	public void setSearchRecord() throws SQLException {
		searchRecordList = searchRecordDAO.getSearchRecord();
		String[] header =  {"검색어", "검색 시간"};
		String[][] record = new String[searchRecordList.size()][2];
		
		for(int i=searchRecordList.size()-1; i>=0; i--) {
			record[i][0] = searchRecordList.get(i).getSearchWord();
			record[i][1] = searchRecordList.get(i).getDate();
		}
		
		DefaultTableModel model = new DefaultTableModel(record, header);
		searchRecordTable = new JTable(model);
		searchRecordTableScroll = new JScrollPane(searchRecordTable);
		searchRecordPanel.add(searchRecordTableScroll);
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == homeButton) {   //뒤로가기
			panelManager.ChangeToMainPage(); 
		}
		else {  //초기화
			
		}
		
	}
}
