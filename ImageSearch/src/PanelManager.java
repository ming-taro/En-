
import javax.swing.JFrame;

public class PanelManager extends JFrame {
	private MainPage mainPage;
	private SearchResult searchResult;
	public PanelManager() {
		mainPage = new MainPage(this);
		searchResult = new SearchResult(this);
		
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
		mainPage = new MainPage(this);
		getContentPane().removeAll();
		getContentPane().add(mainPage);
		revalidate();
		repaint();
	}
	public void ChangeToSearchResult() {
		searchResult = new SearchResult(this);
		getContentPane().removeAll();
		getContentPane().add(searchResult);
		revalidate();
		repaint();
	}
	
}
