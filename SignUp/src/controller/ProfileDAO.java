package controller;

import java.sql.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

import model.Profile;
import utility.Constants;

public class ProfileDAO {
	private static ProfileDAO profileDAO;
	private Connection connection; 
	private Statement statement; 
	private ResultSet result;
	private String Driver = "";
	private String url = "jdbc:mysql://localhost:3306/parkminji?&serverTimezone=Asia/Seoul&useSSL=false";
	private String userid = "root";
	private String pwd = "0000";
	
	public static ProfileDAO GetInstance()
    {
        if (profileDAO == null)
        {
        	profileDAO = new ProfileDAO();
        }
        return profileDAO;
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

	public Profile getMemberProfile(String id) throws SQLException {
		Profile profile;
		String 	query; 
		
		setConnection();
		query = String.format(Constants.QUERY_FOR_MEMBER_PROFILE, id);
		statement = connection.createStatement();
		result = statement.executeQuery(query); //SQL쿼리문

		profile = new Profile(result.getString("id"), result.getString("password"), result.getString("name"),
				result.getString("birth"), result.getString("sex"), result.getString("zipCode"),
				result.getString("roadNameAddress"), result.getString("detailAddress"),
				result.getString("phoneNumber"), result.getString("email"));
		
		result.close();
		statement.close();
		connection.close();
		
		return profile;
	}
	
	public void AddToMemberList(Profile profile) throws SQLException {   //DB에 회원가입한 회원의 정보 저장
		String query; 
		
		setConnection();
		
		query = String.format(Constants.QUERY_TO_ADD_MEMBER,
				profile.getId(), profile.getPassword(), profile.getName(), profile.getBirth(),
				profile.getSex(), profile.getZipCode(), profile.getRoadNameAddress(),
				profile.getDetailAddress(), profile.getPhoneNumber(), profile.getEmail());
				
		statement = connection.createStatement();
		statement.execute(query);

		statement.close();
		connection.close();
	}
	
	public boolean isMemberInList(String id, String password) {  //로그인시 회원의 정보가 존재하는지 검사
		String query; 
		boolean isMemberInList = !Constants.IS_MEMBER_IN_LIST;  
		
		setConnection();
		
		query = String.format(Constants.QUERY_TO_CHECK_IF_MEMBER_IS_IN_LIST, id, password);
		
		try {
			statement = connection.createStatement();
			result = statement.executeQuery(query); //SQL쿼리문
			
			if(result.next()) {
				isMemberInList = Constants.IS_MEMBER_IN_LIST;   //입력한 아이디와 비밀번호와 일치하는 회원정보가 있음
			}
			
			result.close();
			statement.close();
			connection.close();
			
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		return isMemberInList;
	}
}
