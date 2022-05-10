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
	         System.out.println("드라이버 로드 성공");
	      } catch (ClassNotFoundException e) {
	         e.printStackTrace();
	      }
	      
	      setConnection();
	   }
	public void setConnection() {
		try { 
	          connection = DriverManager.getConnection(url, userid, pwd);
	          System.out.println("데이터베이스 연결 성공");
	    } catch (SQLException e1) {
	          e1.printStackTrace();
	    }
	}

	public void createSearchRecord(ArrayList<SearchRecordDTO> searchRecordList) throws SQLException {
		String 	qeury; 
		
		setConnection();
		qeury = "select * from searchrecord";
		statement = connection.createStatement();
		ResultSet rs = statement.executeQuery(qeury); //SQL문을 전달하여 실행

		while(rs.next()){
			searchRecordList.add(new SearchRecordDTO(rs.getString("searchWord"), rs.getString("date")));
		}
		   
		rs.close();
		statement.close();
		connection.close();
	}
	public void AddSearchRecord(String searchWord) throws SQLException {
		String sql; 
		LocalDateTime now = LocalDateTime.now();
		String formatedNow = now.format(DateTimeFormatter.ofPattern("yyyy.MM.dd HH:mm:ss"));

		setConnection();
		sql = "insert into searchrecord(searchWord,date) values('" + searchWord + "', '" + formatedNow + "');";
		System.out.print(sql);
		statement = connection.createStatement();
		statement.execute(sql); 

		statement.close();
		connection.close();
	}
}
