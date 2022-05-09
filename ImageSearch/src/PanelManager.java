
import java.io.IOException;
import javax.swing.JFrame;
import org.json.simple.parser.ParseException;

public class PanelManager extends JFrame {
	private MainPage mainPage;
	private SearchResult searchResult;
	private SearchRecord searchRecord;
	
	public PanelManager() throws IOException, ParseException {
		mainPage = new MainPage(this);
		searchResult = new SearchResult(this);
		searchRecord = new SearchRecord(this);
		
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
	public void ChangeToSearchResult(String searchWord) throws IOException, ParseException {
		getContentPane().removeAll();
		getContentPane().add(searchResult);
		revalidate();
		repaint();
		searchResult.setResult(searchWord);
	}
	public void ChangeToSearchRecord() {
		getContentPane().removeAll();
		getContentPane().add(searchRecord);
		revalidate();
		repaint();
	}
	
}
