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
	private DefaultTableModel model;
	
	public SearchRecord(SearchRecordDAO searchRecordDAO, PanelManager panelManager) throws SQLException {
		homeButton = new JButton("뒤로가기");
		resetButton = new JButton("초기화");
		searchRecordPanel = new JPanel();
		searchRecordList = new ArrayList<SearchRecordDTO>();
		this.searchRecordDAO = searchRecordDAO;
		this.panelManager = panelManager;
		model = new DefaultTableModel(0,0);
		
		homeButton.setBounds(10, 10, 100, 50);
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
		setInitialSearchRecord();
	}
	public void setInitialSearchRecord() throws SQLException {
		searchRecordList = searchRecordDAO.getSearchRecord();	

		String[] header =  {"검색어", "검색 시간"};
		String[][] record = new String[searchRecordList.size()][2];
		
		for(int i=0, j=searchRecordList.size()-1; i<searchRecordList.size(); i++,j--) {
			record[i][0] = searchRecordList.get(j).getSearchWord();
			record[i][1] = searchRecordList.get(j).getDate();
		}

		model = new DefaultTableModel(record, header);
		searchRecordTable = new JTable(model);
		searchRecordTable.setRowHeight(50);
		searchRecordTable.setFont(new Font("SansSerif", Font.BOLD, 17));
		searchRecordTable.repaint();
		searchRecordTableScroll = new JScrollPane(searchRecordTable);
		searchRecordTableScroll.setBounds(0, 0, 500, 350);
		searchRecordPanel.add(searchRecordTableScroll);

	}
	public void setSearchRecord() throws SQLException {
		DefaultTableModel tableModel = (DefaultTableModel) searchRecordTable.getModel();
		tableModel.setRowCount(0);
		
		searchRecordList = searchRecordDAO.getSearchRecord();
		String[][] record = new String[searchRecordList.size()][2];
		
		for(int i=0, j=searchRecordList.size()-1; i<searchRecordList.size(); i++,j--) {  //
			record[i][0] = searchRecordList.get(j).getSearchWord();
			record[i][1] = searchRecordList.get(j).getDate();
			model.addRow(record[i]);
		}
	}

	@Override
	public void actionPerformed(ActionEvent event) {
		if(event.getSource() == homeButton) {   //뒤로가기 버튼
			panelManager.ChangeToMainPage();    //홈으로 돌아옴
		}
		else {  //�ʱ�ȭ
			int reset = JOptionPane.showConfirmDialog(this, "검색 기록을 초기화하시겠습니까?", "초기화", JOptionPane.YES_NO_OPTION, JOptionPane.PLAIN_MESSAGE);
			if(reset == 0)  //확인버튼을 누른 경우
				try {
					DefaultTableModel model = (DefaultTableModel)searchRecordTable.getModel();  //테이블 내용 지움
					model.setNumRows(0);
					searchRecordDAO.ResetSearchRecord();  //DB에서 기록 삭제
				} catch (SQLException e) {
					e.printStackTrace();
				}
		}
		
	}
}
