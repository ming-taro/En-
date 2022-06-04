package controller;

import java.sql.*;
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
	      	} catch (ClassNotFoundException e) {
	         e.printStackTrace();
	      	}
		setConnection();
	}
	
	public void setConnection() {
		try { 
	          connection = DriverManager.getConnection(url, userid, pwd);
	    } catch (SQLException e) {
	          e.printStackTrace();
	    }
	}

	public Profile getMemberProfile(String id) throws SQLException {
		Profile profile;
		String 	query; 

		query = String.format(Constants.QUERY_FOR_MEMBER_PROFILE, id);
		
		setConnection();
		statement = connection.createStatement();
		result = statement.executeQuery(query); //SQL쿼리문
		result.next();
		
		profile = new Profile(result.getString("id"), result.getString("password"), result.getString("name"),
				result.getString("birth"), result.getString("sex"), result.getString("zipCode"),
				result.getString("roadNameAddress"), result.getString("detailAddress"),
				result.getString("phoneNumber"), result.getString("email"));
		
		result.close();
		statement.close();
		connection.close();
		
		return profile;
	}
	
	public void addToMemberList(Profile profile) {   //DB에 회원가입한 회원의 정보 저장
		String query; 
		
		setConnection();
		
		query = String.format(Constants.QUERY_TO_ADD_MEMBER,
				profile.getId(), profile.getPassword(), profile.getName(), profile.getBirth(),
				profile.getSex(), profile.getZipCode(), profile.getRoadNameAddress(),
				profile.getDetailAddress(), profile.getPhoneNumber(), profile.getEmail());
				
		try {
			statement = connection.createStatement();
			statement.execute(query);

			statement.close();
			connection.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public boolean isDuplicateId(String id) {
		String query; 
		boolean isDuplicateId = !Constants.IS_DUPLICATE_ID;
		
		query = String.format(Constants.QUERY_TO_CHECK_IF_DUPLICATE_ID, id);

		setConnection();
		
		try {
			statement = connection.createStatement();
			result = statement.executeQuery(query); //SQL쿼리문
			
			if(result.next()) {
				isDuplicateId = Constants.IS_DUPLICATE_ID;   //입력한 아이디와 일치하는 회원정보가 있음
			}
			
			result.close();
			statement.close();
			connection.close();
			
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		return isDuplicateId;
	}
	
	public boolean isDuplicatePhoneNumber(String phoneNumber) {
		String query; 
		boolean isDuplicatePhoneNumber = !Constants.IS_DUPLICATE_PHONE_NUMBER;
		
		query = String.format(Constants.QUERY_TO_CHECK_IF_DUPLICATE_PHONE_NUMBER, phoneNumber);

		setConnection();
		
		try {
			statement = connection.createStatement();
			result = statement.executeQuery(query); //SQL쿼리문
			
			if(result.next()) {  //입력한 전화번호와 일치하는 회원정보가 있음
				isDuplicatePhoneNumber = Constants.IS_DUPLICATE_PHONE_NUMBER;   
			}
			
			result.close();
			statement.close();
			connection.close();
			
		} catch (SQLException e) {
			e.printStackTrace();
		}
		
		return isDuplicatePhoneNumber;
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
	
	public void removeFromMemberList(String userId) {   //DB에 회원가입한 회원의 정보 저장
		String query; 
		
		setConnection();
		
		query = String.format(Constants.QUERY_TO_DELETE_MEMBER, userId);
		
		try {
			statement = connection.createStatement();
			statement.execute(query);

			statement.close();
			connection.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}
}
