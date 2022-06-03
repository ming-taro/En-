package controller;

import java.sql.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

import model.Profile;

public class ProfileDAO {
	private static ProfileDAO profileDAO;
	private Connection connection; 
	private Statement statement; 
	private ResultSet rs;
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
		query = String.format("select * from memberList where id=%s", id);
		statement = connection.createStatement();
		ResultSet result = statement.executeQuery(query); //SQL쿼리문

		profile = new Profile(rs.getString("id"), rs.getString("password"), rs.getString("name"),
				rs.getString("birth"), rs.getString("sex"), rs.getString("zipCode"),
				rs.getString("roadNameAddress"), rs.getString("detailAddress"),
				rs.getString("phoneNumber"), rs.getString("email"));
		
		rs.close();
		statement.close();
		connection.close();
		
		return profile;
	}
	
	public void AddToMemberList(Profile profile) throws SQLException {
		String query; 
		
		setConnection();
		
		query = String.format("insert into memberlist(id, password, name, birth, sex, "
				+ "zipCode, roadNameAddress, detailAddress, phoneNumber, email) "
				+ "values('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s');",
				profile.getId(), profile.getPassword(), profile.getName(), profile.getBirth(),
				profile.getSex(), profile.getZipCode(), profile.getRoadNameAddress(),
				profile.getDetailAddress(), profile.getPhoneNumber(), profile.getEmail());
				
		statement = connection.createStatement();
		statement.execute(query);

		statement.close();
		connection.close();
	}
}
