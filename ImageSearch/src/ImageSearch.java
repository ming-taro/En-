

import java.io.IOException;
import java.sql.SQLException;

import org.json.simple.parser.ParseException;

public class ImageSearch {

	public static void main(String[] args) throws IOException, ParseException, SQLException {
		//SearchRecordDAO s = new SearchRecordDAO();
		//s.createSearchRecord();
		PanelManager panelManager = new PanelManager();
		panelManager.ChangeToMainPage();
	}

}
