import java.sql.*;
import java.text.MessageFormat;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;


public class SearchRecordDAO {
	private Connection connection; 
	private Statement statement; 
	private ResultSet rs;
	private String Driver = "";
	private String url = "jdbc:mysql://localhost:3306/parkminji?&serverTimezone=Asia/Seoul&useSSL=false";
	private String userid = "root";
	private String pwd = "0000";
	
	public SearchRecordDAO() {
		connectionDB();
	}
	
	public void connectionDB() {
	      try {
	         Class.forName("com.mysql.cj.jdbc.Driver");
	         System.out.println("드라이브 연결 성공...");
	      } catch (ClassNotFoundException e) {
	         e.printStackTrace();
	      }
	      
	      setConnection();
	   }
	public void setConnection() {
		try { 
	          connection = DriverManager.getConnection(url, userid, pwd);
	          System.out.println("데이터 베이스 연결 성공...");
	    } catch (SQLException e1) {
	          e1.printStackTrace();
	    }
	}

	public ArrayList<SearchRecordDTO> getSearchRecord() throws SQLException {
		String 	qeury; 
		ArrayList<SearchRecordDTO> searchRecordList = new ArrayList<SearchRecordDTO>();
		
		setConnection();
		qeury = "select * from searchrecord";
		statement = connection.createStatement();
		ResultSet rs = statement.executeQuery(qeury); //SQL쿼리문

		while(rs.next()){
			searchRecordList.add(new SearchRecordDTO(rs.getString("searchWord"), rs.getString("date")));
		}
		   
		rs.close();
		statement.close();
		connection.close();
		
		return searchRecordList;
	}
	public void AddSearchRecord(String searchWord) throws SQLException {
		String sql; 
		LocalDateTime now = LocalDateTime.now();
		String formatedNow = now.format(DateTimeFormatter.ofPattern("yyyy.MM.dd HH:mm:ss"));

		setConnection();
		sql = "delete from searchrecord where searchword='"+ searchWord +"';";
		statement = connection.createStatement();
		statement.execute(sql);
		
		sql = "insert into searchrecord(searchWord,date) values('" + searchWord + "', '" + formatedNow + "');";
		statement = connection.createStatement();
		statement.execute(sql);

		statement.close();
		connection.close();
	}
	public void ResetSearchRecord() throws SQLException {
		String sql;
		
		setConnection();
		sql = "delete from searchrecord;";
		statement = connection.createStatement();
		statement.execute(sql); 
		statement.close();
		connection.close();
	}
}
