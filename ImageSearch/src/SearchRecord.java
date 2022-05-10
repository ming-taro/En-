import java.awt.Color;
import java.awt.Font;
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
		homeButton = new JButton("H");
		resetButton = new JButton("초기화");
		searchRecordPanel = new JPanel();
		searchRecordList = new ArrayList<SearchRecordDTO>();
		this.searchRecordDAO = searchRecordDAO;
		this.panelManager = panelManager;
		
		homeButton.setBounds(10, 10, 50, 50);
		resetButton.setBounds(350, 450, 100, 50);
		searchRecordPanel.setBounds(150, 80, 500, 350);
		
		searchRecordPanel.setLayout(null);
		
		setLayout(null);
		setBounds(0, 0, 800, 600);
		setBackground(Color.white);
		
		add(homeButton);
		add(resetButton);
		add(searchRecordPanel);
		
		homeButton.addActionListener(this);
		resetButton.addActionListener(this);
	}
	public void setSearchRecord() throws SQLException {
		searchRecordList = searchRecordDAO.getSearchRecord();
		String[] header =  {"검색어", "검색 시간"};
		String[][] record = new String[searchRecordList.size()][2];
		
		for(int i=0, j=searchRecordList.size()-1; i<searchRecordList.size(); i++,j--) {
			record[i][0] = searchRecordList.get(j).getSearchWord();
			record[i][1] = searchRecordList.get(j).getDate();
		}
		
		DefaultTableModel model = new DefaultTableModel(record, header);
		searchRecordTable = new JTable(model);
		model.fireTableDataChanged();
		searchRecordTable.setRowHeight(50);
		searchRecordTable.setFont(new Font("SansSerif", Font.BOLD, 17));
		searchRecordTableScroll = new JScrollPane(searchRecordTable);
		searchRecordTableScroll.setBounds(0, 0, 500, 350);
		searchRecordPanel.add(searchRecordTableScroll);
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == homeButton) {   //뒤로가기
			panelManager.ChangeToMainPage(); 
		}
		else {  //초기화
			int reset = JOptionPane.showConfirmDialog(this, "검색기록을 초기화하시겠습니까?", "확인", JOptionPane.YES_NO_OPTION, JOptionPane.PLAIN_MESSAGE);
			if(reset == 0)
				try {
					DefaultTableModel model = (DefaultTableModel)searchRecordTable.getModel();
					model.setNumRows(0);
					searchRecordDAO.ResetSearchRecord();
				} catch (SQLException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
		}
		
	}
}
