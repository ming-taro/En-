import java.awt.BorderLayout;
import java.awt.Color;
import java.io.IOException;
import java.net.URL;
import java.sql.SQLException;

import javax.swing.*;

import org.json.simple.parser.ParseException;

public class ImageSearch {

	public static void main(String[] args) throws IOException, ParseException, SQLException {
		SearchRecordDAO s = new SearchRecordDAO();
		s.connectionDB();
		s.getSearchRecord();
		//PanelManager panelManager = new PanelManager();
		//panelManager.ChangeToMainPage();
	}

}
