import java.awt.BorderLayout;
import java.awt.Color;
import java.io.IOException;
import java.net.URL;
import java.sql.SQLException;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

import javax.swing.*;

import org.json.simple.parser.ParseException;

public class ImageSearch {

	public static void main(String[] args) throws IOException, ParseException, SQLException {
		//SearchRecordDAO s = new SearchRecordDAO();
		//s.createSearchRecord();
		PanelManager panelManager = new PanelManager();
		panelManager.ChangeToMainPage();
	}

}
