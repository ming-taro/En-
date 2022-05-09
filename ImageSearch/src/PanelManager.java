
import javax.swing.JFrame;

public class PanelManager extends JFrame {
	private MainPage mainPage;
	private SearchResult searchResult;
	public PanelManager() {
		setTitle("Image Search");
		setBounds(450, 150, 800, 600);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		//mainPage = new MainPage();
		//searchResult = new SearchResult();
		
		mainPage.setComponents();
		//add(mainPage);
		setVisible(true);
	}
	
	public void ChangeToMainPage() {
		getContentPane().removeAll();
		//getContentPane().add(mainPage);
		revalidate();
		repaint();
	}
	public void ChangeToSearchResult() {
		getContentPane().removeAll();
		//getContentPane().add(searchResult);
		revalidate();
		repaint();
	}
	
}
