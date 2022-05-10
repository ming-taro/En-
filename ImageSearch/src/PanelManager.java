
import java.io.IOException;
import java.sql.SQLException;

import javax.swing.JFrame;
import org.json.simple.parser.ParseException;

public class PanelManager extends JFrame {
	private MainPage mainPage;
	private SearchResult searchResult;
	private SearchRecord searchRecord;
	private SearchRecordDAO searchRecordDAO;
	
	public PanelManager() throws IOException, ParseException, SQLException {
		searchRecordDAO = new SearchRecordDAO();
		mainPage = new MainPage(this);
		searchResult = new SearchResult(searchRecordDAO, this);
		searchRecord = new SearchRecord(searchRecordDAO, this);
		
		setTitle("Image Search");
		setBounds(450, 150, 800, 600);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setTitle("Image Search");
		setBounds(450, 150, 800, 600);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setVisible(true);
		setLayout(null);
	}
	
	public void ChangeToMainPage() {
		getContentPane().removeAll();
		getContentPane().add(mainPage);
		revalidate();
		repaint();
	}
	public void ChangeToSearchResult(String searchWord) throws IOException, ParseException, SQLException {
		getContentPane().removeAll();
		getContentPane().add(searchResult);
		revalidate();
		repaint();
		searchResult.setResult(searchWord);
	}
	public void ChangeToSearchRecord() throws SQLException {
		getContentPane().removeAll();
		getContentPane().add(searchRecord);
		revalidate();
		repaint();
		searchRecord.setSearchRecord();
	}
	
}
