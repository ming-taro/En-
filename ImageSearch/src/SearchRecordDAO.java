import java.sql.*;


public class SearchRecordDAO {
	private Connection connection; 
	private Statement statement; 
	private ResultSet rs;
	private String Driver = "";
	private String url = "jdbc:mysql://localhost:3306/parkminji?&serverTimezone=Asia/Seoul&useSSL=false";
	private String userid = "root";
	private String pwd = "0000";
	
	public void connectionDB() {
	      try {
	         Class.forName("com.mysql.cj.jdbc.Driver");
	         System.out.println("드라이버 로드 성공");
	      } catch (ClassNotFoundException e) {
	         e.printStackTrace();
	      }
	      
	      try { /* 데이터베이스를 연결하는 과정 */
	          System.out.println("데이터베이스 연결 준비...");
	          connection = DriverManager.getConnection(url, userid, pwd);
	          System.out.println("데이터베이스 연결 성공");
	       } catch (SQLException e1) {
	          e1.printStackTrace();
	       }
	   }
	   

	public void getSearchRecord() throws SQLException {
		String sql; //SQL문을 저장할 String
		sql = "select * from searchrecord";
		statement = connection.createStatement();
		ResultSet rs = statement.executeQuery(sql); //SQL문을 전달하여 실행
		
		while(rs.next()){
			
			
				/*String number = rs.getString("_number");
				String name = rs.getString("name");
				String kor = rs.getString("kor");
				String math = rs.getString("math");
				String eng = rs.getString("eng");
				System.out.println("Number: "+ number + "\nName: " + name + "\nKOR: " + kor); 
				System.out.println("MATH: "+ math + "\nENG: " + eng + "\n-------------\n");
		*/}
		   
			
		rs.close();
		statement.close();
		connection.close();
	}
}
